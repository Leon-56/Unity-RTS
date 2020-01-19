using System.Collections.Generic;

namespace RTS.GameEvent
{
    public class IGameEventSubject
    {
        private List<IGameEventObserver> m_Observers = new List<IGameEventObserver>();
        private System.Object m_Param = null;	// parameter

        // Add Observer
        public void Attach(IGameEventObserver theObserver)
        {
            m_Observers.Add( theObserver );
        }

        // Remove Observer
        public void Detach(IGameEventObserver theObserver)
        {
            m_Observers.Remove( theObserver );
        }

        // Notify
        public void Notify()
        {
            foreach( IGameEventObserver theObserver  in m_Observers)
                theObserver.Update();
        }

        // Set parameter
        public virtual void SetParam( System.Object Param )
        {
            m_Param = Param;
        }
    }
}