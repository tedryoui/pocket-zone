using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player) : base(player)
        {
        }

        public override void DoStart()
        {
            
        }

        public override void DoAction()
        {
            // TODO - Compute random actions
            
            // Switch to the move state
            if(InputHandler.GetJoystickValue != Vector2.zero)
                _player.GetController.SetState(_player.GetController.MoveState);
        }
    }
}