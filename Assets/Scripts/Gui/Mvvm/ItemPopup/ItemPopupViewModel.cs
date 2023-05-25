using System;
using DefaultNamespace.Items;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Gui.Mvvm.ItemPopup
{
    [Serializable]
    public class ItemPopupViewModel
    {
        [SerializeField] private ItemPopupView _view;

        public UnityEvent onRemoveClicked;
        
        public void Bind()
        {
            _view.onRemoveClicked += onRemoveClicked.Invoke;
            _view.onBackClicked += Hide;
        }
        
        public void Show()
        {
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        public void SetItem(ItemStack stack)
        {
            SetIcon(stack.item.icon);
            BindRemoveAction(stack);
        }

        public void SetIcon(Sprite icon)
        {
            _view.icon.sprite = icon;
        }
        
        public void BindRemoveAction(ItemStack stack)
        {
            onRemoveClicked.RemoveAllListeners();
            onRemoveClicked.AddListener(() =>
            {
                GuiHandler.Instance.player.stash.RemoveItemFromStack(stack);
                Hide();
            });
        }
    }
}