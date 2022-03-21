using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class RectangleAdapter : ContainerAdapter, IAdapter
    {
        [Header("Border Settings")] [Tooltip("Gets or sets the corner radius angle")]
        public float cornerRadius;

        [Tooltip("Gets or sets the corner radius angle")]
        public float thickness;

        public new void UpdateUI()
        {
            base.UpdateUI();
        }

        public new GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}