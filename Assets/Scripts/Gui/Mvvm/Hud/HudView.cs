using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gui.Mvvm
{
    public class HudView : MonoBehaviour
    {
        public Button removeButton;
        public Button shootButton;
        public Button inventoryButton;

        public event Action onRemoveClicked;
        public event Action onShootClicked;
        public event Action onInventoryClicked;

        private HudViewModel ViewModel => GuiHandler.Instance?.hud;
        
        private void Start() => ViewModel.Bind();
        private void OnDestroy() => ViewModel?.Unbind();

        public void OnRemoveClicked() => onRemoveClicked?.Invoke();
        public void OnShootClicked() => onShootClicked?.Invoke();
        public void OnInventoryClicked() => onInventoryClicked?.Invoke();
    }
}