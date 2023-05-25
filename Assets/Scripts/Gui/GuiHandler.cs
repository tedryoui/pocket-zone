using System;
using DefaultNamespace.Gui.Mvvm;
using DefaultNamespace.Gui.Mvvm.HealthBars;
using DefaultNamespace.Gui.Mvvm.Inventory;
using DefaultNamespace.Gui.Mvvm.ItemPopup;
using UnityEngine;

namespace DefaultNamespace.Gui
{
    public class GuiHandler : MonoBehaviour
    {
        private static GuiHandler _instance;
        public Player player;
        public Camera cachedCamera;
        public Canvas overlayCanvas;
        public Canvas cameraCanvas;

        public static GuiHandler Instance => _instance;
        public HudViewModel hud;
        public InventoryViewModel inventory;
        public ItemPopupViewModel itemPopup;
        public HealthBarsViewModel healthBars;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                _instance.player = FindObjectOfType<Player>();
                _instance.cachedCamera = Camera.main;
                _instance.cameraCanvas.worldCamera = _instance.cachedCamera;
                
                Destroy(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}