using System;
using System.Linq;
using DefaultNamespace.Spawner;
using UnityEngine;

namespace DefaultNamespace.Weapons
{
    public class AkGun : Weapon
    {
        [SerializeField] private float _attackDamage;
        [SerializeField] private Animator _animator;
        
        private Player _playerReference;
        [SerializeField] private LayerMask _entitiesLayer;

        public override void Equip(Player player)
        {
            _playerReference = player;
        }
        
        public override void Shoot()
        {
            // Get overlap point
            var point = _playerReference.GetObject.position;
            
            // Overlap for entity colliders around
            var colliders = Physics2D.OverlapCircleAll(point, _playerReference.GetStats.ShootDistance, _entitiesLayer);
            if(colliders.Length == 0) return;

            // Get closest entity
            GameObject closest = colliders
                .OrderBy(x => Vector2.Distance(x.transform.position, _playerReference.GetObject.position))
                .First().gameObject;
            
            // Shoot
            closest.GetComponent<Spawnable>().TakeDamage(_attackDamage);
            
            // Align toward shoot direction
            var direction = ((Vector2)closest.transform.position - (Vector2)transform.position).normalized;
            _playerReference.GetController.AlignObject(direction);
            _playerReference.GetController.AlignWeapon(direction);
            
            // Run animations
            _playerReference.GetAnimator.SetTrigger("isKickback");
            _animator.SetTrigger("isKickback");
        }
    }
}