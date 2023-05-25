using System;
using UnityEngine;

namespace DefaultNamespace.Spawner
{
    [Serializable]
    public class SpawnerFabric
    {
        [SerializeField] private GameObject _entityPrefab;
        [SerializeField] private int _entitiesAmount;
        
        public GameObject Prefab => _entityPrefab;
        public int Amount => _entitiesAmount;

        public GameObject Create(Transform parent, ObjectPool pool, Vector2 position)
        {
            var obj = pool.Get(_entityPrefab.tag);
            obj.transform.position = position;
            obj.transform.parent = parent;
            
            return obj;
        }
    }
}