using DefaultNamespace.State;

namespace DefaultNamespace
{
    public abstract class PlayerState : IState
    {
        protected Player _player;

        public PlayerState(Player player)
        {
            _player = player;
        }

        public virtual void DoStart()
        {
            
        }
        
        public abstract void DoAction();
    }
}