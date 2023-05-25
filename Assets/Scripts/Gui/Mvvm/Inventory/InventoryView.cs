using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gui.Mvvm.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        public Button backButton;
        public Button[] cells;
        
        public event Action onBackClicked;

        private InventoryViewModel ViewModel => GuiHandler.Instance.inventory;
        
        public void Start() => ViewModel.Bind();

        public void OnBackClicked() => onBackClicked?.Invoke();
    }
}