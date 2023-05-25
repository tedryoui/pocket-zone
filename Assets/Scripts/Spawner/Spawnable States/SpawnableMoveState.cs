using UnityEngine;

namespace DefaultNamespace.Spawner.Spawnable_States
{
    public class SpawnableMoveState : SpawnableState
    {
        public SpawnableMoveState(Spawnable processor) : base(processor)
        {
        }

        public override void DoStart()
        {
            _spawnable.animator.SetBool("isRunning", true);

            _spawnable.dstSetter.target = _spawnable.GetPlayer.transform;
        }

        public override void DoAction()
        {
            if (_spawnable.DstToPlayer > _spawnable.triggerDistance || 
                _spawnable.DstToPlayer < _spawnable.attackDistance - 0.25f)
            {
                _spawnable.animator.SetBool("isRunning", false);
                _spawnable.dstSetter.target = null;
                _spawnable.SetState(_spawnable.IdleState);
            }
            
            _spawnable.AlignObject(_spawnable.GetPlayer.transform.position - _spawnable.transform.position);
        }
    }
}