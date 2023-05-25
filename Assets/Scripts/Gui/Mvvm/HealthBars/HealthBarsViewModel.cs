using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DefaultNamespace.Gui.Mvvm.HealthBars
{
    [Serializable]
    public class HealthBarsViewModel
    {
        [SerializeField] private HealthBarsView _view;

        public void UpdateHealthBar(GameObject parent, Vector3 offset, float value)
        {
            var pointInViewport = PointsInView(parent.transform.position);
            if (pointInViewport == false)
            {
                RemoveHealthBar(parent);
                return;
            }
            
            if (_view.healthBars.ContainsKey(parent) == false) CreateHealthBar(parent);

            var obj = _view.healthBars[parent];
            obj.gameObject.transform.position = GuiHandler.Instance.cachedCamera.WorldToScreenPoint(parent.transform.position + offset);
            obj.filler.fillAmount = value;    
        }

        private bool PointsInView(Vector3 transformPosition)
        {
            Vector3 viewport = GuiHandler.Instance.cachedCamera.WorldToViewportPoint(transformPosition);
            return viewport.x is > 0 and < 1 &&
                   viewport.y is > 0 and < 1;
        }

        public void RemoveHealthBar(GameObject parent)
        {
            if(_view.healthBars.ContainsKey(parent))
            {
                var hb = _view.healthBars[parent];
                Object.Destroy(hb.gameObject);

                _view.healthBars.Remove(parent);
            }
        }

        private void CreateHealthBar(GameObject parent)
        {
            var healthObject = UnityEngine.Object.Instantiate(_view.healthBarPrefab, _view.transform);
            var filler = healthObject.transform.Find("Health Filler").GetComponent<Image>();

            HealthBar hb = new HealthBar()
            {
                gameObject = healthObject,
                filler = filler
            };
            
            _view.healthBars.Add(parent, hb);
        }
    }
}