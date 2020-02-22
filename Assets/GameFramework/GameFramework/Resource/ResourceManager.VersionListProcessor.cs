﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Download;
using System;
using System.IO;
using System.Text;

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        /// <summary>
        /// 版本资源列表处理器。
        /// </summary>
        private sealed class VersionListProcessor
        {
            private readonly ResourceManager m_ResourceManager;
            private IDownloadManager m_DownloadManager;
            private int m_VersionListLength;
            private int m_VersionListHashCode;
            private int m_VersionListZipLength;
            private int m_VersionListZipHashCode;

            public GameFrameworkAction<string, string> VersionListUpdateSuccess;
            public GameFrameworkAction<string, string> VersionListUpdateFailure;

            /// <summary>
            /// 初始化版本资源列表处理器的新实例。
            /// </summary>
            /// <param name="resourceManager">资源管理器。</param>
            public VersionListProcessor(ResourceManager resourceManager)
            {
                m_ResourceManager = resourceManager;
                m_DownloadManager = null;
                m_VersionListLength = 0;
                m_VersionListHashCode = 0;
                m_VersionListZipLength = 0;
                m_VersionListZipHashCode = 0;

                VersionListUpdateSuccess = null;
                VersionListUpdateFailure = null;
            }

            /// <summary>
            /// 关闭并清理版本资源列表处理器。
            /// </summary>
            public void Shutdown()
            {
                if (m_DownloadManager != null)
                {
                    m_DownloadManager.DownloadSuccess -= OnDownloadSuccess;
                    m_DownloadManager.DownloadFailure -= OnDownloadFailure;
                }
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
                m_DownloadManager.DownloadSuccess += OnDownloadSuccess;
                m_DownloadManager.DownloadFailure += OnDownloadFailure;
            }

            /// <summary>
            /// 检查版本资源列表。
            /// </summary>
            /// <param name="latestInternalResourceVersion">最新的内部资源版本号。</param>
            /// <returns>检查版本资源列表结果。</returns>
            public CheckVersionListResult CheckVersionList(int latestInternalResourceVersion)
            {
                if (string.IsNullOrEmpty(m_ResourceManager.m_ReadWritePath))
                {
                    throw new GameFrameworkException("Read-write path is invalid.");
                }

                string applicableGameVersion = null;
                int internalResourceVersion = 0;

                string versionListFileName = Utility.Path.GetRegularPath(Path.Combine(m_ResourceManager.m_ReadWritePath, Utility.Path.GetResourceNameWithSuffix(VersionListFileName)));
                if (!File.Exists(versionListFileName))
                {
                    return CheckVersionListResult.NeedUpdate;
                }

                FileStream fileStream = null;
                try
                {
                    fileStream = new FileStream(versionListFileName, FileMode.Open, FileAccess.Read);
                    using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
                    {
                        fileStream = null;
                        if (binaryReader.ReadChar() != VersionListHeader[0] || binaryReader.ReadChar() != VersionListHeader[1] || binaryReader.ReadChar() != VersionListHeader[2])
                        {
                            return CheckVersionListResult.NeedUpdate;
                        }

                        byte listVersion = binaryReader.ReadByte();

                        if (listVersion == 0)
                        {
                            byte[] encryptBytes = binaryReader.ReadBytes(4);

                            applicableGameVersion = m_ResourceManager.GetEncryptedString(binaryReader, encryptBytes);
                            internalResourceVersion = binaryReader.ReadInt32();
                        }
                        else
                        {
                            throw new GameFrameworkException("Version list version is invalid.");
                        }
                    }
                }
                catch
                {
                    return CheckVersionListResult.NeedUpdate;
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }

                if (internalResourceVersion != latestInternalResourceVersion)
                {
                    return CheckVersionListResult.NeedUpdate;
                }

                return CheckVersionListResult.Updated;
            }

            /// <summary>
            /// 更新版本资源列表。
            /// </summary>
            /// <param name="versionListLength">版本资源列表大小。</param>
            /// <param name="versionListHashCode">版本资源列表哈希值。</param>
            /// <param name="versionListZipLength">版本资源列表压缩后大小。</param>
            /// <param name="versionListZipHashCode">版本资源列表压缩后哈希值。</param>
            public void UpdateVersionList(int versionListLength, int versionListHashCode, int versionListZipLength, int versionListZipHashCode)
            {
                if (m_DownloadManager == null)
                {
                    throw new GameFrameworkException("You must set download manager first.");
                }

                m_VersionListLength = versionListLength;
                m_VersionListHashCode = versionListHashCode;
                m_VersionListZipLength = versionListZipLength;
                m_VersionListZipHashCode = versionListZipHashCode;
                string versionListFileName = Utility.Path.GetResourceNameWithSuffix(VersionListFileName);
                string latestVersionListFileName = Utility.Path.GetResourceNameWithCrc32AndSuffix(VersionListFileName, m_VersionListHashCode);
                string localVersionListFilePath = Utility.Path.GetRegularPath(Path.Combine(m_ResourceManager.m_ReadWritePath, versionListFileName));
                string latestVersionListFileUri = Utility.Path.GetRemotePath(Path.Combine(m_ResourceManager.m_UpdatePrefixUri, latestVersionListFileName));
                m_DownloadManager.AddDownload(localVersionListFilePath, latestVersionListFileUri, this);
            }

            private void OnDownloadSuccess(object sender, DownloadSuccessEventArgs e)
            {
                VersionListProcessor versionListProcessor = e.UserData as VersionListProcessor;
                if (versionListProcessor == null || versionListProcessor != this)
                {
                    return;
                }

                using (FileStream fileStream = new FileStream(e.DownloadPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    int length = (int)fileStream.Length;
                    if (length != m_VersionListZipLength)
                    {
                        fileStream.Close();
                        string errorMessage = Utility.Text.Format("Latest version list zip length error, need '{0}', downloaded '{1}'.", m_VersionListZipLength.ToString(), length.ToString());
                        DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                        OnDownloadFailure(this, downloadFailureEventArgs);
                        ReferencePool.Release(downloadFailureEventArgs);
                        return;
                    }

                    fileStream.Position = 0L;
                    int hashCode = Utility.Verifier.GetCrc32(fileStream);
                    if (hashCode != m_VersionListZipHashCode)
                    {
                        fileStream.Close();
                        string errorMessage = Utility.Text.Format("Latest version list zip hash code error, need '{0}', downloaded '{1}'.", m_VersionListZipHashCode.ToString(), hashCode.ToString());
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
                            string errorMessage = Utility.Text.Format("Unable to decompress latest version list '{0}'.", e.DownloadPath);
                            DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                            OnDownloadFailure(this, downloadFailureEventArgs);
                            ReferencePool.Release(downloadFailureEventArgs);
                            return;
                        }

                        if (m_ResourceManager.m_DecompressCachedStream.Length != m_VersionListLength)
                        {
                            fileStream.Close();
                            string errorMessage = Utility.Text.Format("Latest version list length error, need '{0}', downloaded '{1}'.", m_VersionListLength.ToString(), m_ResourceManager.m_DecompressCachedStream.Length.ToString());
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
                        string errorMessage = Utility.Text.Format("Unable to decompress latest version list '{0}' with error message '{1}'.", e.DownloadPath, exception.ToString());
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

                if (VersionListUpdateSuccess != null)
                {
                    VersionListUpdateSuccess(e.DownloadPath, e.DownloadUri);
                }
            }

            private void OnDownloadFailure(object sender, DownloadFailureEventArgs e)
            {
                VersionListProcessor versionListProcessor = e.UserData as VersionListProcessor;
                if (versionListProcessor == null || versionListProcessor != this)
                {
                    return;
                }

                if (File.Exists(e.DownloadPath))
                {
                    File.Delete(e.DownloadPath);
                }

                if (VersionListUpdateFailure != null)
                {
                    VersionListUpdateFailure(e.DownloadUri, e.ErrorMessage);
                }
            }
        }
    }
}
