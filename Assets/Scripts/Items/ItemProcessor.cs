using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Items
{
    public class ItemProcessor
    {
        private Player _player;
        private Item _item;
        private GameObject _obj;

        public GameObject GetObject => _obj;
        public event Action<ItemProcessor> OnDestroy;

        public ItemProcessor(Player player, Item item, GameObject obj, Vector2 position)
        {
            _player = player;
            _item = item;
            _obj = obj;

            _obj.transform.position = position;
            
            var image = _obj.GetComponent<SpriteRenderer>();
            image.sprite = _item.icon;
        }

        public void Update()
        {
            if (Vector3.Distance(_player.GetObject.position, _obj.transform.position) <= 1.0f)
            {
                _player.stash.AddItem(_item);
                
                OnDestroy?.Invoke(this);
            }
        }
    }
}