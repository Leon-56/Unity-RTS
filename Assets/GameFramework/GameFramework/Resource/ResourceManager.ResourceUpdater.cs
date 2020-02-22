﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Download;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        /// <summary>
        /// 资源更新器。
        /// </summary>
        private sealed partial class ResourceUpdater
        {
            private const int CachedHashBytesLength = 4;

            private readonly ResourceManager m_ResourceManager;
            private readonly List<UpdateInfo> m_UpdateWaitingInfo;
            private readonly List<UpdateInfo> m_UpdateCandidateInfo;
            private readonly byte[] m_CachedHashBytes;
            private IDownloadManager m_DownloadManager;
            private bool m_CheckResourcesComplete;
            private ResourceGroup m_UpdatingResourceGroup;
            private int m_GenerateReadWriteListLength;
            private int m_CurrentGenerateReadWriteListLength;
            private int m_UpdateRetryCount;
            private int m_UpdatingCount;
            private bool m_FailureFlag;
            private string m_ReadWriteListFileName;
            private string m_ReadWriteListBackupFileName;

            public GameFrameworkAction<ResourceName, string, string, int, int, int> ResourceUpdateStart;
            public GameFrameworkAction<ResourceName, string, string, int, int> ResourceUpdateChanged;
            public GameFrameworkAction<ResourceName, string, string, int, int> ResourceUpdateSuccess;
            public GameFrameworkAction<ResourceName, string, int, int, string> ResourceUpdateFailure;
            public GameFrameworkAction<ResourceGroup, bool, bool> ResourceUpdateAllComplete;

            /// <summary>
            /// 初始化资源更新器的新实例。
            /// </summary>
            /// <param name="resourceManager">资源管理器。</param>
            public ResourceUpdater(ResourceManager resourceManager)
            {
                m_ResourceManager = resourceManager;
                m_UpdateWaitingInfo = new List<UpdateInfo>();
                m_UpdateCandidateInfo = new List<UpdateInfo>();
                m_CachedHashBytes = new byte[CachedHashBytesLength];
                m_DownloadManager = null;
                m_CheckResourcesComplete = false;
                m_UpdatingResourceGroup = null;
                m_GenerateReadWriteListLength = 0;
                m_CurrentGenerateReadWriteListLength = 0;
                m_UpdateRetryCount = 3;
                m_UpdatingCount = 0;
                m_FailureFlag = false;
                m_ReadWriteListFileName = Utility.Path.GetRegularPath(Path.Combine(m_ResourceManager.m_ReadWritePath, Utility.Path.GetResourceNameWithSuffix(ResourceListFileName)));
                m_ReadWriteListBackupFileName = m_ReadWriteListFileName + BackupFileSuffixName;

                ResourceUpdateStart = null;
                ResourceUpdateChanged = null;
                ResourceUpdateSuccess = null;
                ResourceUpdateFailure = null;
                ResourceUpdateAllComplete = null;
            }

            /// <summary>
            /// 获取或设置每下载多少字节的资源，刷新一次资源列表。
            /// </summary>
            public int GenerateReadWriteListLength
            {
                get
                {
                    return m_GenerateReadWriteListLength;
                }
                set
                {
                    m_GenerateReadWriteListLength = value;
                }
            }

            /// <summary>
            /// 获取或设置资源更新重试次数。
            /// </summary>
            public int UpdateRetryCount
            {
                get
                {
                    return m_UpdateRetryCount;
                }
                set
                {
                    m_UpdateRetryCount = value;
                }
            }

            /// <summary>
            /// 获取正在更新的资源组。
            /// </summary>
            public IResourceGroup UpdatingResourceGroup
            {
                get
                {
                    return m_UpdatingResourceGroup;
                }
            }

            /// <summary>
            /// 获取等待更新资源数量。
            /// </summary>
            public int UpdateWaitingCount
            {
                get
                {
                    return m_UpdateWaitingInfo.Count;
                }
            }

            /// <summary>
            /// 获取候选更新资源数量。
            /// </summary>
            public int UpdateCandidateCount
            {
                get
                {
                    return m_UpdateCandidateInfo.Count;
                }
            }

            /// <summary>
            /// 获取正在更新资源数量。
            /// </summary>
            public int UpdatingCount
            {
                get
                {
                    return m_UpdatingCount;
                }
            }

            /// <summary>
            /// 资源更新器轮询。
            /// </summary>
            /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
            /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                if (m_UpdatingResourceGroup == null)
                {
                    return;
                }

                if (m_UpdateWaitingInfo.Count > 0)
                {
                    if (m_DownloadManager.FreeAgentCount > 0)
                    {
                        UpdateInfo updateInfo = m_UpdateWaitingInfo[0];
                        m_UpdateWaitingInfo.RemoveAt(0);
                        m_DownloadManager.AddDownload(updateInfo.ResourcePath, Utility.Path.GetRemotePath(Path.Combine(m_ResourceManager.m_UpdatePrefixUri, Utility.Path.GetResourceNameWithCrc32AndSuffix(updateInfo.ResourceName.FullName, updateInfo.HashCode))), updateInfo);
                        m_UpdatingCount++;
                    }
                }
                else if (m_UpdatingCount <= 0)
                {
                    ResourceGroup updatingResourceGroup = m_UpdatingResourceGroup;
                    m_UpdatingResourceGroup = null;

                    Utility.Path.RemoveEmptyDirectory(m_ResourceManager.m_ReadWritePath);
                    if (ResourceUpdateAllComplete != null)
                    {
                        ResourceUpdateAllComplete(updatingResourceGroup, !m_FailureFlag, m_UpdateCandidateInfo.Count <= 0);
                    }
                }
            }

            /// <summary>
            /// 关闭并清理资源更新器。
            /// </summary>
            public void Shutdown()
            {
                if (m_DownloadManager != null)
                {
                    m_DownloadManager.DownloadStart -= OnDownloadStart;
                    m_DownloadManager.DownloadUpdate -= OnDownloadUpdate;
                    m_DownloadManager.DownloadSuccess -= OnDownloadSuccess;
                    m_DownloadManager.DownloadFailure -= OnDownloadFailure;
                }

                m_UpdateWaitingInfo.Clear();
                m_UpdateCandidateInfo.Clear();
            }

            /// <summary>
            /// 设置下载管理器。
            /// </summary>
            /// <param name="downloadManager">下载管理器。</param>
            public void SetDownloadManager(IDownloadManager downloadManager)
            {
                if (downloadManager == null)
                {
                    throw new GameFrameworkException("Download manager is invalid.");
                }

                m_DownloadManager = downloadManager;
                m_DownloadManager.DownloadStart += OnDownloadStart;
                m_DownloadManager.DownloadUpdate += OnDownloadUpdate;
                m_DownloadManager.DownloadSuccess += OnDownloadSuccess;
                m_DownloadManager.DownloadFailure += OnDownloadFailure;
            }

            /// <summary>
            /// 增加资源更新。
            /// </summary>
            /// <param name="resourceName">资源名称。</param>
            /// <param name="loadType">资源加载方式。</param>
            /// <param name="length">资源大小。</param>
            /// <param name="hashCode">资源哈希值。</param>
            /// <param name="zipLength">压缩包大小。</param>
            /// <param name="zipHashCode">压缩包哈希值。</param>
            /// <param name="resourcePath">资源路径。</param>
            public void AddResourceUpdate(ResourceName resourceName, LoadType loadType, int length, int hashCode, int zipLength, int zipHashCode, string resourcePath)
            {
                m_UpdateCandidateInfo.Add(new UpdateInfo(resourceName, loadType, length, hashCode, zipLength, zipHashCode, resourcePath));
            }

            /// <summary>
            /// 检查资源完成。
            /// </summary>
            /// <param name="needGenerateReadWriteList">是否需要生成读写区资源列表。</param>
            public void CheckResourceComplete(bool needGenerateReadWriteList)
            {
                m_CheckResourcesComplete = true;
                if (needGenerateReadWriteList)
                {
                    GenerateReadWriteList();
                }
            }

            /// <summary>
            /// 更新指定资源组的资源。
            /// </summary>
            /// <param name="resourceGroup">要更新的资源组。</param>
            public void UpdateResources(ResourceGroup resourceGroup)
            {
                if (m_DownloadManager == null)
                {
                    throw new GameFrameworkException("You must set download manager first.");
                }

                if (!m_CheckResourcesComplete)
                {
                    throw new GameFrameworkException("You must check resources complete first.");
                }

                if (m_UpdatingResourceGroup != null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("There is already a resource group '{0}' being updated.", m_UpdatingResourceGroup.Name));
                }

                if (string.IsNullOrEmpty(resourceGroup.Name))
                {
                    m_UpdateWaitingInfo.AddRange(m_UpdateCandidateInfo);
                    m_UpdateCandidateInfo.Clear();
                }
                else
                {
                    int index = 0;
                    while (index < m_UpdateCandidateInfo.Count)
                    {
                        if (!resourceGroup.HasResource(m_UpdateCandidateInfo[index].ResourceName))
                        {
                            index++;
                            continue;
                        }

                        m_UpdateWaitingInfo.Add(m_UpdateCandidateInfo[index]);
                        m_UpdateCandidateInfo.RemoveAt(index);
                    }
                }

                m_UpdatingResourceGroup = resourceGroup;
                m_FailureFlag = false;
            }

            private void GenerateReadWriteList()
            {
                if (File.Exists(m_ReadWriteListFileName))
                {
                    if (File.Exists(m_ReadWriteListBackupFileName))
                    {
                        File.Delete(m_ReadWriteListBackupFileName);
                    }

                    File.Move(m_ReadWriteListFileName, m_ReadWriteListBackupFileName);
                }

                FileStream fileStream = null;
                try
                {
                    fileStream = new FileStream(m_ReadWriteListFileName, FileMode.CreateNew, FileAccess.Write);
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8))
                    {
                        fileStream = null;
                        Utility.Random.GetRandomBytes(m_CachedHashBytes);

                        binaryWriter.Write(ReadWriteListHeader);
                        binaryWriter.Write(ReadWriteListVersionHeader);
                        binaryWriter.Write(m_CachedHashBytes);
                        binaryWriter.Write(m_ResourceManager.m_ReadWriteResourceInfos.Count);
                        foreach (KeyValuePair<ResourceName, ReadWriteResourceInfo> i in m_ResourceManager.m_ReadWriteResourceInfos)
                        {
                            byte[] nameBytes = Utility.Converter.GetBytes(i.Key.Name);
                            Utility.Encryption.GetSelfXorBytes(nameBytes, m_CachedHashBytes);
                            binaryWriter.Write((byte)nameBytes.Length);
                            binaryWriter.Write(nameBytes);

                            if (i.Key.Variant == null)
                            {
                                binaryWriter.Write((byte)0);
                            }
                            else
                            {
                                byte[] variantBytes = Utility.Converter.GetBytes(i.Key.Variant);
                                Utility.Encryption.GetSelfXorBytes(variantBytes, m_CachedHashBytes);
                                binaryWriter.Write((byte)variantBytes.Length);
                                binaryWriter.Write(variantBytes);
                            }

                            binaryWriter.Write((byte)i.Value.LoadType);
                            binaryWriter.Write(i.Value.Length);
                            binaryWriter.Write(i.Value.HashCode);
                        }

                        Array.Clear(m_CachedHashBytes, 0, CachedHashBytesLength);
                    }

                    if (!string.IsNullOrEmpty(m_ReadWriteListBackupFileName))
                    {
                        File.Delete(m_ReadWriteListBackupFileName);
                    }
                }
                catch (Exception exception)
                {
                    if (File.Exists(m_ReadWriteListFileName))
                    {
                        File.Delete(m_ReadWriteListFileName);
                    }

                    if (!string.IsNullOrEmpty(m_ReadWriteListBackupFileName))
                    {
                        File.Move(m_ReadWriteListBackupFileName, m_ReadWriteListFileName);
                    }

                    throw new GameFrameworkException(Utility.Text.Format("Pack save exception '{0}'.", exception.ToString()), exception);
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }
            }

            private void OnDownloadStart(object sender, DownloadStartEventArgs e)
            {
                UpdateInfo updateInfo = e.UserData as UpdateInfo;
                if (updateInfo == null)
                {
                    return;
                }

                if (m_DownloadManager == null)
                {
                    throw new GameFrameworkException("You must set download manager first.");
                }

                if (e.CurrentLength > updateInfo.ZipLength)
                {
                    m_DownloadManager.RemoveDownload(e.SerialId);
                    string downloadFile = Utility.Text.Format("{0}.download", e.DownloadPath);
                    if (File.Exists(downloadFile))
                    {
                        File.Delete(downloadFile);
                    }

                    string errorMessage = Utility.Text.Format("When download start, downloaded length is larger than zip length, need '{0}', current '{1}'.", updateInfo.ZipLength.ToString(), e.CurrentLength.ToString());
                    DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                    OnDownloadFailure(this, downloadFailureEventArgs);
                    ReferencePool.Release(downloadFailureEventArgs);
                    return;
                }

                if (ResourceUpdateStart != null)
                {
                    ResourceUpdateStart(updateInfo.ResourceName, e.DownloadPath, e.DownloadUri, e.CurrentLength, updateInfo.ZipLength, updateInfo.RetryCount);
                }
            }

            private void OnDownloadUpdate(object sender, DownloadUpdateEventArgs e)
            {
                UpdateInfo updateInfo = e.UserData as UpdateInfo;
                if (updateInfo == null)
                {
                    return;
                }

                if (m_DownloadManager == null)
                {
                    throw new GameFrameworkException("You must set download manager first.");
                }

                if (e.CurrentLength > updateInfo.ZipLength)
                {
                    m_DownloadManager.RemoveDownload(e.SerialId);
                    string downloadFile = Utility.Text.Format("{0}.download", e.DownloadPath);
                    if (File.Exists(downloadFile))
                    {
                        File.Delete(downloadFile);
                    }

                    string errorMessage = Utility.Text.Format("When download update, downloaded length is larger than zip length, need '{0}', current '{1}'.", updateInfo.ZipLength.ToString(), e.CurrentLength.ToString());
                    DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                    OnDownloadFailure(this, downloadFailureEventArgs);
                    ReferencePool.Release(downloadFailureEventArgs);
                    return;
                }

                if (ResourceUpdateChanged != null)
                {
                    ResourceUpdateChanged(updateInfo.ResourceName, e.DownloadPath, e.DownloadUri, e.CurrentLength, updateInfo.ZipLength);
                }
            }

            private void OnDownloadSuccess(object sender, DownloadSuccessEventArgs e)
            {
                UpdateInfo updateInfo = e.UserData as UpdateInfo;
                if (updateInfo == null)
                {
                    return;
                }

                using (FileStream fileStream = new FileStream(e.DownloadPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    bool zip = updateInfo.Length != updateInfo.ZipLength || updateInfo.HashCode != updateInfo.ZipHashCode;

                    int length = (int)fileStream.Length;
                    if (length != updateInfo.ZipLength)
                    {
                        fileStream.Close();
                        string errorMessage = Utility.Text.Format("Resource zip length error, need '{0}', downloaded '{1}'.", updateInfo.ZipLength.ToString(), length.ToString());
                        DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                        OnDownloadFailure(this, downloadFailureEventArgs);
                        ReferencePool.Release(downloadFailureEventArgs);
                        return;
                    }

                    if (zip)
                    {
                        fileStream.Position = 0L;
                        int hashCode = Utility.Verifier.GetCrc32(fileStream);
                        if (hashCode != updateInfo.ZipHashCode)
                        {
                            fileStream.Close();
                            string errorMessage = Utility.Text.Format("Resource zip hash code error, need '{0}', downloaded '{1}'.", updateInfo.ZipHashCode.ToString(), hashCode.ToString());
                            DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                            OnDownloadFailure(this, downloadFailureEventArgs);
                            ReferencePool.Release(downloadFailureEventArgs);
                            return;
                        }

                        if (m_ResourceManager.m_DecompressCachedStream == null)
                        {
                            m_ResourceManager.m_DecompressCachedStream = new MemoryStream();
                        }

                        try
                        {
                            fileStream.Position = 0L;
                            m_ResourceManager.m_DecompressCachedStream.Position = 0L;
                            m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
                            if (!Utility.Zip.Decompress(fileStream, m_ResourceManager.m_DecompressCachedStream))
                            {
                                fileStream.Close();
                                string errorMessage = Utility.Text.Format("Unable to decompress resource '{0}'.", e.DownloadPath);
                                DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                                OnDownloadFailure(this, downloadFailureEventArgs);
                                ReferencePool.Release(downloadFailureEventArgs);
                                return;
                            }

                            if (m_ResourceManager.m_DecompressCachedStream.Length != updateInfo.Length)
                            {
                                fileStream.Close();
                                string errorMessage = Utility.Text.Format("Resource length error, need '{0}', downloaded '{1}'.", updateInfo.Length.ToString(), m_ResourceManager.m_DecompressCachedStream.Length.ToString());
                                DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                                OnDownloadFailure(this, downloadFailureEventArgs);
                                ReferencePool.Release(downloadFailureEventArgs);
                                return;
                            }

                            fileStream.Position = 0L;
                            fileStream.SetLength(0L);
                            fileStream.Write(m_ResourceManager.m_DecompressCachedStream.GetBuffer(), 0, (int)m_ResourceManager.m_DecompressCachedStream.Length);
                        }
                        catch (Exception exception)
                        {
                            fileStream.Close();
                            string errorMessage = Utility.Text.Format("Unable to decompress resource '{0}' with error message '{1}'.", e.DownloadPath, exception.ToString());
                            DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                            OnDownloadFailure(this, downloadFailureEventArgs);
                            ReferencePool.Release(downloadFailureEventArgs);
                            return;
                        }
                        finally
                        {
                            m_ResourceManager.m_DecompressCachedStream.Position = 0L;
                            m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
                        }
                    }
                    else
                    {
                        int hashCode = 0;
                        fileStream.Position = 0L;
                        if (updateInfo.LoadType == LoadType.LoadFromMemoryAndQuickDecrypt || updateInfo.LoadType == LoadType.LoadFromMemoryAndDecrypt)
                        {
                            Utility.Converter.GetBytes(updateInfo.HashCode, m_CachedHashBytes);
                            if (updateInfo.LoadType == LoadType.LoadFromMemoryAndQuickDecrypt)
                            {
                                hashCode = Utility.Verifier.GetCrc32(fileStream, m_CachedHashBytes, Utility.Encryption.QuickEncryptLength);
                            }
                            else if (updateInfo.LoadType == LoadType.LoadFromMemoryAndDecrypt)
                            {
                                hashCode = Utility.Verifier.GetCrc32(fileStream, m_CachedHashBytes, length);
                            }

                            Array.Clear(m_CachedHashBytes, 0, CachedHashBytesLength);
                        }
                        else
                        {
                            hashCode = Utility.Verifier.GetCrc32(fileStream);
                        }

                        if (hashCode != updateInfo.HashCode)
                        {
                            fileStream.Close();
                            string errorMessage = Utility.Text.Format("Zip hash code error, need '{0}', downloaded '{1}'.", updateInfo.ZipHashCode.ToString(), hashCode.ToString());
                            DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                            OnDownloadFailure(this, downloadFailureEventArgs);
                            ReferencePool.Release(downloadFailureEventArgs);
                            return;
                        }
                    }
                }

                m_UpdatingCount--;

                if (m_ResourceManager.m_ResourceInfos.ContainsKey(updateInfo.ResourceName))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Resource info '{0}' is already exist.", updateInfo.ResourceName.FullName));
                }

                m_ResourceManager.m_ResourceInfos.Add(updateInfo.ResourceName, new ResourceInfo(updateInfo.ResourceName, updateInfo.LoadType, updateInfo.Length, updateInfo.HashCode, false));

                if (m_ResourceManager.m_ReadWriteResourceInfos.ContainsKey(updateInfo.ResourceName))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Read-write resource info '{0}' is already exist.", updateInfo.ResourceName.FullName));
                }

                m_ResourceManager.m_ReadWriteResourceInfos.Add(updateInfo.ResourceName, new ReadWriteResourceInfo(updateInfo.LoadType, updateInfo.Length, updateInfo.HashCode));

                m_CurrentGenerateReadWriteListLength += updateInfo.ZipLength;
                if (m_UpdatingCount <= 0 || m_CurrentGenerateReadWriteListLength >= m_GenerateReadWriteListLength)
                {
                    m_CurrentGenerateReadWriteListLength = 0;
                    GenerateReadWriteList();
                }

                if (ResourceUpdateSuccess != null)
                {
                    ResourceUpdateSuccess(updateInfo.ResourceName, e.DownloadPath, e.DownloadUri, updateInfo.Length, updateInfo.ZipLength);
                }
            }

            private void OnDownloadFailure(object sender, DownloadFailureEventArgs e)
            {
                UpdateInfo updateInfo = e.UserData as UpdateInfo;
                if (updateInfo == null)
                {
                    return;
                }

                if (File.Exists(e.DownloadPath))
                {
                    File.Delete(e.DownloadPath);
                }

                m_UpdatingCount--;

                if (ResourceUpdateFailure != null)
                {
                    ResourceUpdateFailure(updateInfo.ResourceName, e.DownloadUri, updateInfo.RetryCount, m_UpdateRetryCount, e.ErrorMessage);
                }

                if (updateInfo.RetryCount < m_UpdateRetryCount)
                {
                    updateInfo.RetryCount++;
                    m_UpdateWaitingInfo.Add(updateInfo);
                }
                else
                {
                    m_FailureFlag = true;
                    updateInfo.RetryCount = 0;
                    m_UpdateCandidateInfo.Add(updateInfo);
                }
            }
        }
    }
}
