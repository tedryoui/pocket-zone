using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gui.Mvvm.HealthBars
{
    public class HealthBarsView : MonoBehaviour
    {
        public GameObject healthBarPrefab;

        public Dictionary<GameObject, HealthBar> healthBars;

        private void Start() => healthBars = new Dictionary<GameObject, HealthBar>();
    }

    public struct HealthBar
    {
        public GameObject gameObject;
        public Image filler;
    }
}