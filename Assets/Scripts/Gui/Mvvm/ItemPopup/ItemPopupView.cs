using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gui.Mvvm.ItemPopup
{
    public class ItemPopupView : MonoBehaviour
    {
        public Image icon;
        public Button removeButton;
        public Button backButton;

        public event Action onRemoveClicked;
        public event Action onBackClicked;

        private ItemPopupViewModel ViewModel => GuiHandler.Instance.itemPopup;
        
        private void Start() => ViewModel.Bind();

        public void OnRemoveClicked() => onRemoveClicked?.Invoke();
        public void OnBackClicked() => onBackClicked?.Invoke();
    }
}