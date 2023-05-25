using DefaultNamespace.State;

namespace DefaultNamespace.Spawner.Spawnable_States
{
    public class SpawnableState : IState
    {
        protected Spawnable _spawnable;

        public SpawnableState(Spawnable spawnable)
        {
            _spawnable = spawnable;
        }
        
        public virtual void DoStart()
        {
            
        }

        public virtual void DoAction()
        {
            
        }
    }
}