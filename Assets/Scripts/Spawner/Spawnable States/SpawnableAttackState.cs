using UnityEngine;

namespace DefaultNamespace.Spawner.Spawnable_States
{
    public class SpawnableAttackState : SpawnableState
    {
        private float crrDelay;
        
        public SpawnableAttackState(Spawnable spawnable) : base(spawnable)
        {
        }

        public override void DoStart()
        {
            crrDelay = _spawnable.attackDelay;
        }

        public override void DoAction()
        {
            crrDelay += Time.deltaTime;

            if (crrDelay > _spawnable.attackDelay)
            {
                _spawnable.animator.SetTrigger("isAttacking");
                crrDelay = 0.0f;
            }
            
            if(_spawnable.DstToPlayer > _spawnable.attackDistance)
                _spawnable.SetState(_spawnable.IdleState);
            
            _spawnable.AlignObject(_spawnable.GetPlayer.transform.position - _spawnable.transform.position);
        }
    }
}