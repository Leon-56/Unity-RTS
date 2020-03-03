using UnityEngine;
using System;
using System.Collections;
using System.Net.NetworkInformation;
using RTS;
using RTS.Factory;
using RTS.GameSystem;
using RTS.Tool;
using UnityEngine.UI;

namespace RTS.UI
{
    public class CampInfoUI : IUserInterface
    {
        private ICamp m_Camp = null;  // 显示的兵营
        
        // 组件界面
        private Text m_AliveCountTxt = null;
        private Text m_CampLvTxt = null;
        private Text m_WeaponLvTxt = null;
        private Text m_TrainCostTxt = null;
        private Text m_TrainTimerTxt = null;
        private Text m_OnTrainCountTxt = null;
        private Text m_CampNameTxt = null;
        private Image m_CampImage = null;
        
        public CampInfoUI(RTSGame RTS) : base(RTS)
        {
            Initialize();
        }
        
        public override void Initialize()
        {
            m_RootUI = UITool.FindUIGameObject("CampInfoUI");
            
            // 兵营名称
            m_CampNameTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampNameText");
            // 兵营图
            m_CampImage = UITool.GetUIComponent<Image>(m_RootUI, "CameIcon");
            // 存活单位
            m_AliveCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "AliveCountText");
            // 等级
            m_CampLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampLevelText");
            // 武器等级
            m_WeaponLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "WeaponLevelText");
            // 训练中的数量
            m_OnTrainCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "OnTrainCountText");
            // 训练花费
            m_TrainCostTxt = UITool.GetUIComponent<Text>(m_RootUI, "TrainCostText");
            // 训练时间
            m_TrainTimerTxt = UITool.GetUIComponent<Text>(m_RootUI, "TrainTimerText");
            
            Hide();
        }

        public void ShowInfo(ICamp Camp)
        {
            Show();
            m_Camp = Camp;
            
            // 名称
            m_CampNameTxt.text = m_Camp.GetName();
            // 训练花费
            m_TrainCostTxt.text = string.Format("AP:{0}", m_Camp.GetTrainCost());
            
            // 训练中信息
            ShowOnTrainInfo();
            // Icon
            IAssetFactory Factory = RTSFactory.GetAssetFactory();
            m_CampImage.sprite = Factory.LoadSprite(m_Camp.GetIconSpriteName());
            
            // 升级功能
            if(m_Camp.GetLevel() <= 0)
                EnableLevelInfo(false);
            else
            {
                EnableLevelInfo(true);
                m_CampLvTxt.text = string.Format("等级:" + m_Camp.GetLevel());
                m_WeaponLvTxt.text = string.Format("武器等级:" + m_Camp.GetWeaponLevel());
            }
        }

        public void EnableLevelInfo(bool Value)
        {
            
        }

        public void ShowOnTrainInfo()
        {
            
        }
    }
}