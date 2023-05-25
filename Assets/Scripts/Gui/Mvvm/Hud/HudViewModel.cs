using System;
using UnityEngine;

namespace DefaultNamespace.Gui.Mvvm
{
    [Serializable]
    public class HudViewModel
    {
        [SerializeField] private HudView _view;

        public void Bind()
        {
            _view.onRemoveClicked += ChangeRemoveState;
            _view.onShootClicked += PerformShoot;
            _view.onInventoryClicked += OpenInventory;
            
            ChangeRemoveState();
        }

        public void Unbind()
        {
            _view.onRemoveClicked -= ChangeRemoveState;
            _view.onShootClicked -= PerformShoot;
            _view.onInventoryClicked -= OpenInventory;
        }
        
        public void ChangeRemoveState()
        {
            bool nxtState = !GuiHandler.Instance.inventory.isRemoveState;

            _view.removeButton.image.color = (nxtState) ? new Color(1, 1, 1, 1) : new Color(1,1,1,0.5f);
            
            GuiHandler.Instance.inventory.isRemoveState = nxtState;
        }

        public void PerformShoot()
        {
            GuiHandler.Instance.player.weapon.Shoot();
        }

        public void OpenInventory()
        {
            GuiHandler.Instance.inventory.Show();
        }
    }
}