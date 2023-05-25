using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private ItemEmitter _itemEmitter;
        private ObjectPool _pool;
        
        [SerializeField] private Transform _entityParent;
        [SerializeField] private List<SpawnerFabric> _fabrics;

        [SerializeField] private List<Spawnable> _processors;
        [SerializeField] private HashSet<Vector2> _fixedPositions;

        [SerializeField] private Vector2 _gridMinPoint;
        [SerializeField] private Vector2 _gridMaxPoint;
        [SerializeField] private float _gridStep;

        private event Action OnUpdate;
        
        private void Start()
        {
            _pool = new ObjectPool();
            _fixedPositions = new HashSet<Vector2>();
            _processors = new List<Spawnable>();
            
            foreach (var fabric in _fabrics)
                _pool.RegisterPool(fabric.Prefab.tag, fabric.Prefab, fabric.Amount, _entityParent);

            SpawnEntities();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            _pool.ReleasePools();
        }

        private void SpawnEntities()
        {
            var horPoints = (_gridMaxPoint.x - _gridMinPoint.x) / _gridStep;
            var verPoints = (_gridMaxPoint.y - _gridMinPoint.y) / _gridStep;

            foreach (var fabric in _fabrics)
            {
                var amount = 0;
                while (amount != fabric.Amount)
                {
                    var position = GetRandomInGrid(horPoints, verPoints);

                    if (!CanSpawn(position)) continue;
                    
                    _fixedPositions.Add(position);
                    var obj = fabric.Create(_entityParent, _pool, position);
                    var spawnable = obj.GetComponent<Spawnable>();
                    spawnable.Initialize(_player, _itemEmitter);

                    amount++;
                }
            }
        }

        private Vector2 GetRandomInGrid(float rows, float cols)
        {
            var rndX = UnityEngine.Random.Range(0, (int) rows);
            var rndY = UnityEngine.Random.Range(0, (int) cols);

            var finalX = _gridMinPoint.x + _gridStep * rndX;
            var finalY = _gridMinPoint.y + _gridStep * rndY;

            var position = new Vector2(finalX + 0.5f, finalY + 0.5f);
            return position + (Vector2)transform.position;
        }

        private bool CanSpawn(Vector2 position)
        {
            return Vector2.Distance(position, _player.transform.position) >= _gridStep
                && !_fixedPositions.Contains(position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            if(_gridStep == 0) return;

            var center = (Vector2)transform.position + (_gridMaxPoint - _gridMinPoint) / 2.0f + _gridMinPoint;
            Gizmos.DrawWireCube(center, (_gridMaxPoint - _gridMinPoint));
        }
    }
}