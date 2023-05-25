using System;
using UnityEngine;

namespace DefaultNamespace.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public float visualOffset;
        
        public abstract void Equip(Player player);
        public abstract void Shoot();
    }
}