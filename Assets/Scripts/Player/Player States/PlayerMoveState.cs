using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerMoveState : PlayerState
    {
        private const string isRunningAnimator = "isRunning";
        
        public PlayerMoveState(Player player) : base(player)
        {
        }

        public override void DoStart()
        {
            _player.GetAnimator.SetBool(isRunningAnimator, true);
        }

        public override void DoAction()
        {
            var moveDelta = InputHandler.GetJoystickValue.normalized;
            
            if(moveDelta == Vector2.zero)
            {
                ExitState();
                return;
            }
            
            // Set move velocity
            var moveSpeed = _player.GetStats.Speed;
            _player.GetRigidbody2D.velocity = moveDelta * moveSpeed;
            
            // Align visual direction of player and weapon
            _player.GetController.AlignObject(moveDelta);
            _player.GetController.AlignWeapon(moveDelta);
        }

        private void ExitState()
        {
            _player.GetController.SetState(_player.GetController.IdleState);
            _player.GetRigidbody2D.velocity = Vector2.zero;
            _player.GetAnimator.SetBool(isRunningAnimator, false);
        }
    }
}