namespace RTS.GameEvent
{
    public abstract class IGameEventObserver
    {
        public abstract void Update();
        public abstract	void SetSubject( IGameEventSubject Subject );
    }
}