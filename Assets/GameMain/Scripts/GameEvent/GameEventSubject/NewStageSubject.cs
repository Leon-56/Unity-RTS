namespace RTS.GameEvent
{
    public class NewStageSubject : IGameEventSubject
    {
        private int m_StageCount = 1;

        public NewStageSubject()
        {}
        
        public int GetStageCount()
        {
            return m_StageCount;
        }
        
        public override void SetParam( System.Object Param )
        {
            base.SetParam( Param);
            m_StageCount = (int)Param;
            
            Notify();
        }
    }
}