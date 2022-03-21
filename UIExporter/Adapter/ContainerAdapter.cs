using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class ContainerAdapter : ControlAdapter, IAdapter
    {
        [Header("Children Settings")]
        [Tooltip("Gets or sets a boolean indicating if the container should try to adapt to its children height")]
        public bool adaptHeightToChildren;

        [Tooltip("Gets or sets a boolean indicating if the container should try to adapt to its children width")]
        public bool adaptWidthToChildren;

        [ReadOnly] protected new float alpha;

        [ReadOnly] protected Color background = new Color(1, 1, 1, 1);

        public new void UpdateUI()
        {
            base.UpdateUI();
            var imageComponent = GetComponent<Image>();
            imageComponent.sprite = null;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}