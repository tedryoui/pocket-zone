using System;
using DefaultNamespace.State;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerStateController
    {
        private Player _playerReference;
        private IState _crrState;

        public IState IdleState;
        public IState MoveState;

        public PlayerStateController(Player player)
        {
            _playerReference = player;

            IdleState = new PlayerIdleState(_playerReference);
            MoveState = new PlayerMoveState(_playerReference);
            
            _crrState = IdleState;
        }

        public void SetState(IState state)
        {
            _crrState = state;
            _crrState.DoStart();
        }

        public void Update()
        {
            _crrState?.DoAction();
        }
        
        public void AlignObject(Vector2 delta)
        {
            var initialScale = _playerReference.GetObject.localScale;

            if (delta.x >= 0 && initialScale.x < 0 ||
                delta.x < 0 && initialScale.x >= 0) initialScale.x *= -1;

            _playerReference.GetObject.localScale = initialScale;
        }

        public void AlignWeapon(Vector2 delta)
        {
            Vector3 normalDelta = delta.normalized;
            Vector3 weaponScale = _playerReference.weapon.transform.localScale; 
            
            // Compute weapon sprite direction
            if (normalDelta.x >= 0 && weaponScale.x < 0 ||
                normalDelta.x < 0 && weaponScale.x >= 0) weaponScale.x *= -1;
            
            // Compute weapon rotation
            Vector3 lookDirection = normalDelta;
            if (weaponScale.x < 0) lookDirection *= -1;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            Quaternion alignedRotation = Quaternion.Euler(0, 0, angle);
            
            // Compute position offset
            Vector3 offset = normalDelta * _playerReference.weapon.visualOffset;
            Vector3 alignedPosition = _playerReference.weaponPivot.transform.position + offset;
            
            // Set values
            _playerReference.weapon.transform.rotation = alignedRotation;
            _playerReference.weapon.transform.position = alignedPosition;
            _playerReference.weapon.transform.localScale = weaponScale;
        }
    }
}