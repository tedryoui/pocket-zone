using System;
using DefaultNamespace.Gui;
using DefaultNamespace.Items;
using DefaultNamespace.Spawner.Spawnable_States;
using DefaultNamespace.State;
using Pathfinding;
using UnityEngine;

namespace DefaultNamespace.Spawner
{
    public class Spawnable : MonoBehaviour
    {
        private ItemEmitter _itemEmitter;
        private Player _player;
        [SerializeField] private Vector3 _healthBarOffset;
        [SerializeField] private HealthPoints _healthPoints;
        [SerializeField] private Stash _stash;
        
        public AIPath ai;
        public AIDestinationSetter dstSetter;
        public Animator animator;

        public Player GetPlayer => _player;

        public float triggerDistance;
        public float attackDistance;
        public float attackDelay;
        public float attackDamage;
        
        private IState _crrState;
        public SpawnableIdleState IdleState;
        public SpawnableMoveState MoveState;
        public SpawnableAttackState AttackState;
        
        public float DstToPlayer => 
            Vector2.Distance(_player.GetObject.position, transform.position);

        public void Start()
        {
            IdleState = new SpawnableIdleState(this);
            MoveState = new SpawnableMoveState(this);
            AttackState = new SpawnableAttackState(this);
            _crrState = IdleState;
            
            _healthPoints.Initialize();
            _healthPoints.OnZero += Kill;
        }

        private void OnDestroy()
        {
            GuiHandler.Instance?.healthBars.RemoveHealthBar(gameObject);
        }

        public void Initialize(Player player, ItemEmitter emitter)
        {
            _player = player;
            _itemEmitter = emitter;
        }

        private void Kill()
        {
            DropItems();
            Destroy(gameObject);        
        }

        private void DropItems()
        {
            foreach (var stashItem in _stash.items)
            {
                for (int i = 0; i < stashItem.amount; i++)
                {
                    _itemEmitter.Emit(stashItem.item, gameObject.transform.position);
                }
            }
        }

        public void Update()
        {
            _crrState.DoAction();
            
            GuiHandler.Instance.healthBars.UpdateHealthBar(gameObject, _healthBarOffset, _healthPoints.GetHealth / _healthPoints.GetMaxHealthPoints);
        }

        public void SetState(IState state)
        {
            _crrState = state;
            _crrState.DoStart();
        }
        
        public void TakeDamage(float amount)
        {
            _healthPoints.Increase(-amount);
        }

        public void Attack()
        {
            GetPlayer.TakeDamage(attackDamage);
        }
        
        public void AlignObject(Vector2 delta)
        {
            var initialScale = transform.localScale;

            if (delta.x >= 0 && initialScale.x < 0 ||
                delta.x < 0 && initialScale.x >= 0) initialScale.x *= -1;

            transform.localScale = initialScale;
        }
    }
}