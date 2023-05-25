using UnityEngine;

namespace DefaultNamespace.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
    public class Item : ScriptableObject
    {
        public Sprite icon;
    }
}