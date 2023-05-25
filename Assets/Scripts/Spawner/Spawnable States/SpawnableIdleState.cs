namespace DefaultNamespace.Spawner.Spawnable_States
{
    public class SpawnableIdleState : SpawnableState
    {
        public SpawnableIdleState(Spawnable processor) : base(processor)
        {
        }

        public override void DoStart()
        {
            _spawnable.ai.pickNextWaypointDist = _spawnable.triggerDistance;
            _spawnable.ai.endReachedDistance = _spawnable.attackDistance;
        }

        public override void DoAction()
        {
            if(_spawnable.DstToPlayer < _spawnable.triggerDistance &&
               _spawnable.DstToPlayer > _spawnable.attackDistance - 0.25f)
                _spawnable.SetState(_spawnable.MoveState);
            
            if(_spawnable.DstToPlayer < _spawnable.attackDistance)
                _spawnable.SetState(_spawnable.AttackState);
        }
    }
}