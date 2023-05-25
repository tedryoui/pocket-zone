using System.Collections.Generic;
using DefaultNamespace.Items;
using DefaultNamespace.SaveSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerSavable : ISavable
    {
        private Player _player;

        public PlayerSavable(Player player)
        {
            _player = player;
        }
        
        public void SaveIntoObject(SaveObject saveObject)
        {
            saveObject.playerSave.position = _player.transform.position;
            saveObject.playerSave.healthPoints = _player.GetHealthPoints.GetHealth;
            saveObject.playerSave.stacks = _player.stash.items.ToArray();
        }

        public void LoadFromObject(SaveObject saveObject)
        {
            _player.transform.position = saveObject.playerSave.position;
            _player.GetHealthPoints.Initialize(saveObject.playerSave.healthPoints);
            _player.stash.items = new List<ItemStack>(saveObject.playerSave.stacks);
        }

        public void LoadDefaults()
        {
            _player.transform.position = Vector3.zero;
            _player.GetHealthPoints.Initialize();
            _player.stash.items = new List<ItemStack>();
        }
    }
}