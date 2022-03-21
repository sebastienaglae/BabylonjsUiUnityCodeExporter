using System;
using UnityEngine;

namespace PROJECT
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(RectTransform))]
    public class ControlAdapter : MonoBehaviour, IComparable
    {
        [Header("ReadOnly Debug Settings")] [ReadOnly]
        public ControlAdapter parent;

        [ReadOnly] public int hierchicalLevel;
        [ReadOnly] public CanvasExporterUtils.AlignmentHorizontal alignmentHorizontal;
        [ReadOnly] public CanvasExporterUtils.AlignmentVertical alignmentVertical;
        
        [Header("ID Settings")] 
        public string uniqueID;

        [Header("Cursor Settings")] [Tooltip("Gets or sets the cursor to use when the control is hovered")]
        public BabylonControl.CursorType hoverCursor;

        [Header("Focus Settings")] [Tooltip("Gets or sets a boolean indicating if the control can be focusable")]
        public bool isFocusInvisible;

        [Header("Main color Settings")] [Tooltip("Gets or sets foreground color")]
        public Color color = new Color(1, 1, 1, 1);

        [Tooltip("Gets or sets alpha value for the control (1 means opaque and 0 means entirely transparent)")]
        [Range(0, 1)]
        public float alpha = 1;

        [Header("Disable color Settings")] [Tooltip("Gets or sets background color of control if it's disabled")]
        public Color disabledColor = new Color(1, 1, 1, 1);

        [Tooltip("Gets or sets front color of control if it's disabled")]
        public Color disabledColorItem = new Color(1, 1, 1, 1);

        [Header("Highlight Settings")]
        [Tooltip("Gets or sets a string defining the color to use for highlighting this control")]
        public Color highlightColor = new Color(1, 1, 1, 1);

        [Tooltip(
            "Gets or sets a number indicating size of stroke we want to highlight the control with (mostly for debugging purpose)")]
        public float highlightLineWidth;

        [Tooltip(
            "Gets or sets a boolean indicating that we want to highlight the control (mostly for debugging purpose)")]
        public bool isHighlighted;

        [Header("Link Offset Settings")]
        [Tooltip("Gets or sets a value indicating the offset on X axis to the linked mesh")]
        public float linkOffsetX;

        [Tooltip("Gets or sets a value indicating the offset on Y axis to the linked mesh")]
        public float linkOffsetY;

        [Header("Padding Settings")]
        [Tooltip("Gets or sets a value indicating the padding to use on the top of the control")]
        public float paddingTop;

        [Tooltip("Gets or sets a value indicating the padding to use on the bottom of the control")]
        public float paddingBottom;

        [Tooltip("Gets or sets a value indicating the padding to use on the left of the control")]
        public float paddingLeft;

        [Tooltip("Gets or sets a value indicating the padding to use on the right of the control")]
        public float paddingRight;

        [Header("Shadow Settings")]
        [Tooltip("Gets or sets a value indicating the amount of blur to use to render the shadow")]
        public float shadowBlur;

        [Tooltip("Gets or sets a value indicating the color of the shadow (black by default ie. #000)")]
        public Color shadowColor = new Color(1, 1, 1, 1);

        [Tooltip("Gets or sets a value indicating the offset to apply on X axis to render the shadow")]
        public float shadowOffsetX;

        [Tooltip("Gets or sets a value indicating the offset to apply on Y axis to render the shadow")]
        public float shadowOffsetY;

        public int CompareTo(object obj)
        {
            return obj != null ? hierchicalLevel.CompareTo(((ControlAdapter) obj).hierchicalLevel) : 0;
        }

        protected void UpdateUI()
        {
            hierchicalLevel = CanvasExporterUtils.GetHierarchicalLevel(gameObject);
            parent = CanvasExporterUtils.GetParent(gameObject)?.GetGameObject().GetComponent<ControlAdapter>();
            var (horizontal, vertical) = CanvasExporterUtils.GetAlignment(gameObject.GetComponent<RectTransform>());
            alignmentHorizontal = horizontal;
            alignmentVertical = vertical;
        }
    }
}