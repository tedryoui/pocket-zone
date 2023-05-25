using System;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class ItemBaker : MonoBehaviour
    {
        public ItemEmitter emitter;
        public Item item;
        
        private void Start()
        {
            emitter.Emit(item, transform.position);
            
            Destroy(gameObject);
        }
    }
}