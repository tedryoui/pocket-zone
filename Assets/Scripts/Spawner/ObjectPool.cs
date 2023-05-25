using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Spawner
{
    public class ObjectPool
    {
        public ObjectPool()
        {
            _pools = new Dictionary<string, List<GameObject>>();
        }
        
        public Dictionary<string, List<GameObject>> _pools;

        public void RegisterPool(string name, GameObject obj, int amount, Transform parent = null)
        {
            var list = new List<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                var position = new Vector3(-100, -100, -100);
                var gameObject = Object.Instantiate(obj, position, Quaternion.identity, parent);
                gameObject.SetActive(false);

                list.Add(gameObject);
            }

            _pools.Add(name, list);
        }

        public void ReleasePools()
        {
            foreach (var pool in _pools)
                pool.Value.Clear();
            _pools.Clear();
        }

        public GameObject Get(string name)
        {
            if (_pools.TryGetValue(name, out var pool))
            {
                foreach (var element in pool)
                {
                    if (element.activeInHierarchy) continue;

                    element.SetActive(true);
                    return element;
                }
            }

            return null;
        }

        public void Return(GameObject gameObject, string name)
        {
            if (_pools.TryGetValue(name, out var pool))
            {
                if (pool.Contains(gameObject))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}