using System;
using DefaultNamespace.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.Gui.Mvvm.Inventory
{
    [Serializable]
    public class InventoryViewModel
    {
        [SerializeField] private InventoryView _view;

        public bool isRemoveState = true;

        public void Bind()
        {
            _view.onBackClicked += Hide;
        }

        public void Show()
        {
            UpdateCells();
            
            _view.gameObject.SetActive(true);
        }

        public void UpdateCells()
        {
            for (var i = 0; i < _view.cells.Length; i++)
            {
                var cell = _view.cells[i];

                var cellStack = i < GuiHandler.Instance.player.stash.items.Count ? 
                    GuiHandler.Instance.player.stash.items[i] : null;

                UpdateCellInfo(cell, cellStack);
                UpdateCellBinding(cell, cellStack);
            }
        }

        private void UpdateCellBinding(Button cell, ItemStack cellStack)
        {
            if (cellStack == null)
            {
                cell.interactable = false;
            } else {
                cell.interactable = true;
                cell.onClick.RemoveAllListeners();                
                
                if(isRemoveState)
                {
                    cell.onClick.AddListener(() =>
                    {
                        GuiHandler.Instance.itemPopup.SetItem(cellStack);
                        GuiHandler.Instance.itemPopup.Show();
                    });
                }
            }
        }

        private void UpdateCellInfo(Button cell, ItemStack cellStack)
        {
            var cellIcon = cell.transform.Find("Icon").GetComponent<Image>();
            var cellAmount = cell.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            
            if (cellIcon != null)
            {
                if(cellStack == null)
                {
                    cellIcon.gameObject.SetActive(false);
                    cellAmount.gameObject.SetActive(false);
                }
                else
                {
                    cellIcon.gameObject.SetActive(true);
                    cellAmount.gameObject.SetActive(true);

                    cellIcon.sprite = cellStack.item.icon;
                    
                    if(cellStack.amount == 1)
                        cellAmount.SetText("");
                    else cellAmount.SetText($"{cellStack.amount:### ### ###}");
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Inventory Cell Execution Failure");
#endif
            }
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }
    }
}