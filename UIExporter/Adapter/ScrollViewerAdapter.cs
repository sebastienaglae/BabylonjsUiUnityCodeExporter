using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewerAdapter : RectangleAdapter, IAdapter
    {
        [Header("Bar Settings")] [Tooltip("Gets or sets the bar background")]
        public Color barBackground = new Color(1, 1, 1, 1);

        [Tooltip("Gets or sets the bar color")]
        public Color barColor = new Color(1, 1, 1, 1);

        [Tooltip("Gets or sets the bar background image")]
        public string barImageUrl;

        [Tooltip("Gets or sets the height of the bar image")]
        public float barImageHeight;

        [Header("Bucket Settings")] [Tooltip("Gets the bucket height")]
        public float bucketHeight;

        [Tooltip("Gets the bucket width")] public float bucketWidth;

        [Header("Horizontal Settings")] [Tooltip("Gets the horizontal scrollbar")] [ReadOnly]
        public string horizontalBar;

        [Tooltip("Gets or sets the horizontal bar background image")]
        public string horizontalBarImageUrl;

        [Tooltip("Gets or sets the height of the horizontal bar image")]
        public float horizontalBarImageHeight;

        [Tooltip("Gets or sets the horizontal bar image")]
        public string horizontalThumbImageUrl;

        [Tooltip("Forces the horizontal scroll bar to be displayed")]
        public bool forceHorizontalBar;

        [Header("Vertical Settings")] [ReadOnly] [Tooltip("Gets the vertical scrollbar")]
        public string verticalBar;

        [Tooltip("Gets or sets the vertical bar background image")]
        public string verticalBarImageUrl;

        [Tooltip("Gets or sets the height of the vertical bar image")]
        public float verticalBarImageHeight;

        [Tooltip("Gets or sets the vertical bar image")]
        public string verticalThumbImageUrl;

        [Tooltip("Forces the vertical scroll bar to be displayed")]
        public bool forceVerticalBar;


        [Header("Thumb Settings")] [Tooltip("Gets or sets the height of the thumb")]
        public float thumbHeight;

        [Tooltip("Gets or sets the bar image")]
        public string thumbImageUrl;

        [Tooltip("Gets or sets the length of the thumb")]
        public float thumbLength;

        [Header("Other Settings")]
        [Tooltip(
            "Freezes or unfreezes the controls in the window. When controls are frozen, the scroll viewer can render a lot more quickly but updates to positions/sizes of controls are not taken into account. If you want to change positions/sizes, unfreeze, perform the changes then freeze again")]
        public bool freezeControls;

        [Tooltip("Gets or sets the scroll bar container background color")]
        public string scrollBackground;

        [Tooltip("Gets or sets the mouse wheel precision from 0 to 1 with a default value of 0.05")]
        public float wheelPrecision;

        public void UpdateUI()
        {
            base.UpdateUI();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}