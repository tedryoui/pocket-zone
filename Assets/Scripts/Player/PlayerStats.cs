using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _shootDistance;

        public float Speed => _speed;
        public float ShootDistance => _shootDistance;
    }
}