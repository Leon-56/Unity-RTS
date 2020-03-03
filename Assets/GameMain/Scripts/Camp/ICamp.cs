namespace RTS.GameSystem
{
    public class ICamp
    {
        private string m_Name;
        private string m_IconSpriteName;
        private int m_TrainCost;
        private int m_Lv;
        
        public ICamp() { }

        public int GetTrainCost()
        {
            return m_TrainCost;
        }

        public int GetLevel()
        {
            return m_Lv;
        }

        public int GetWeaponLevel()
        {
            return 0;
        }
        
        public string GetName()
        {
            return m_Name;
        }

        public string GetIconSpriteName()
        {
            return m_IconSpriteName;
        }
        
    }
}