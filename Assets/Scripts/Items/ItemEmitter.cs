using System;
using DefaultNamespace.Spawner;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class ItemEmitter : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        [SerializeField] private GameObject _itemPrefab;        
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private int _maxItemsAmount;
        
        private ObjectPool _objectPool;
        private event Action OnUpdate;

        private void Awake()
        {
            _objectPool = new ObjectPool();
            _objectPool.RegisterPool("item", _itemPrefab, _maxItemsAmount, _itemsParent);
        }

        private void OnDestroy()
        {
            _objectPool.ReleasePools();
        }

        public void Emit(Item item, Vector2 pos)
        {
            var obj = _objectPool.Get("item");
            if (obj == null)
            {
#if UNITY_EDITOR
                Debug.Log($"Maximum items per scene limit reached! [{_maxItemsAmount}]");
#endif
                return;
            }

            var processor = new ItemProcessor(_player, item, obj, pos);
            OnUpdate += processor.Update;
            processor.OnDestroy += Kill;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void Kill(ItemProcessor processor)
        {
            OnUpdate -= processor.Update;
            _objectPool.Return(processor.GetObject, "item");
        }
    }
}