using System;
using DefaultNamespace.Items;
using UnityEngine;

namespace DefaultNamespace.SaveSystem
{
    [Serializable]
    public class SaveObject
    {
        [Serializable]
        public struct PlayerSave
        {
            public Vector3 position;
            public float healthPoints;
            public ItemStack[] stacks;
        }

        public PlayerSave playerSave;
    }
}