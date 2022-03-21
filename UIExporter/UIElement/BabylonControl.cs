using UnityEngine;

namespace PROJECT
{
    public abstract class BabylonControl
    {
        public enum CursorType
        {
            AUTO,
            DEFAULT,
            NONE,
            CONTEXT_MENU,
            HELP,
            POINTER,
            PROGRESS,
            WAIT,
            CELL,
            CROSSHAIR,
            TEXT,
            VERTICAL_TEXT,
            ALIAS,
            COPY,
            MOVE,
            NO_DROP,
            GRAB,
            GRABBING,
            NOT_ALLOWED,
            ALL_SCROLL,
            COL_RESIZE,
            ROW_RESIZE,
            N_RESIZE,
            E_RESIZE,
            S_RESIZE,
            W_RESIZE,
            NE_RESIZE,
            NW_RESIZE,
            SE_RESIZE,
            SW_RESIZE,
            EW_RESIZE,
            NS_RESIZE,
            NESW_RESIZE,
            NWSE_RESIZE,
            ZOOM_IN,
            ZOOM_OUT
        }

        protected readonly Canvas canvas;
        protected readonly ControlAdapter controlAdapter;
        protected readonly GameObject gameObject;
        protected readonly RectTransform rectTransform;
        protected readonly string uiName;
        protected readonly int zIndex;
        protected readonly string varName;

        protected BabylonControl(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas)
        {
            this.uiName = uiName;
            this.gameObject = gameObject;
            this.varName = varName;
            this.zIndex = zIndex;
            this.canvas = canvas;
            rectTransform = gameObject.GetComponent<RectTransform>();
            controlAdapter = gameObject.GetComponent<ControlAdapter>();
        }

        protected virtual void GenerateDefault(ref string n)
        {
            var (horizontal, vertical) = CanvasExporterUtils.GetAlignment(gameObject.GetComponent<RectTransform>());
            GenerateIsVisible(ref n);
            GenerateSize(horizontal, vertical, ref n);
            GeneratePosition(horizontal, vertical, ref n);
            GenerateRotation(ref n);
            GenerateScale(ref n);
            GenerateIsFocusInvisible(ref n);
            GenerateAlpha(ref n);
            GenerateColor(ref n);
            GenerateDisable(ref n);
            GenerateHighlight(ref n);
            GenerateLinkOffset(ref n);
            GeneratePadding(ref n);
            GenerateZIndex(ref n);
            GenerateShadow(ref n);
            GenerateHoverCursor(ref n);
        }

        protected virtual void GenerateAddControl(ref string n)
        {
            BabylonUtils.CreateCodeMethod(uiName, "addControl", "this." + varName, ref n);
        }

        protected virtual void GenerateIsVisible(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "isVisible", gameObject.activeInHierarchy, ref n);
        }

        protected virtual void GenerateIsFocusInvisible(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "isFocusInvisible", controlAdapter.isFocusInvisible, ref n);
        }

        protected virtual void GenerateAlpha(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "alpha", controlAdapter.alpha, ref n);
        }

        protected virtual void GenerateColor(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "color", controlAdapter.color, ref n);
        }

        protected virtual void GenerateDisable(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "disabledColor", controlAdapter.disabledColor, ref n);
            BabylonUtils.CreateCodeProperty(varName, "disabledColorItem", controlAdapter.disabledColorItem, ref n);
        }

        protected virtual void GenerateHighlight(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "highlightColor", controlAdapter.highlightColor, ref n);
            BabylonUtils.CreateCodeProperty(varName, "highlightLineWidth", controlAdapter.highlightLineWidth, ref n);
            BabylonUtils.CreateCodeProperty(varName, "isHighlighted", controlAdapter.isHighlighted, ref n);
        }

        protected virtual void GenerateLinkOffset(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "linkOffsetX", controlAdapter.linkOffsetX, ref n);
            BabylonUtils.CreateCodeProperty(varName, "linkOffsetY", controlAdapter.linkOffsetY, ref n);
        }

        protected virtual void GeneratePadding(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "paddingBottom", controlAdapter.paddingBottom, ref n);
            BabylonUtils.CreateCodeProperty(varName, "paddingLeft", controlAdapter.paddingLeft, ref n);
            BabylonUtils.CreateCodeProperty(varName, "paddingRight", controlAdapter.paddingRight, ref n);
            BabylonUtils.CreateCodeProperty(varName, "paddingTop", controlAdapter.paddingTop, ref n);
        }

        protected virtual void GenerateZIndex(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "zIndex", zIndex, ref n);
        }

        protected virtual void GenerateShadow(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "shadowBlur", controlAdapter.shadowBlur, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowColor", controlAdapter.shadowColor, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", -controlAdapter.shadowOffsetX, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", controlAdapter.shadowOffsetY, ref n);
        }

        protected virtual void GenerateRotation(ref string n)
        {
            var val = gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z * Mathf.Deg2Rad;

            BabylonUtils.CreateCodeProperty(varName, "rotation", -val, ref n);
        }

        protected virtual void GenerateSize(CanvasExporterUtils.AlignmentHorizontal horizontal,
            CanvasExporterUtils.AlignmentVertical vertical, ref string n)
        {
            float valw;
            float valh;
            var target = rectTransform;

            if (horizontal == CanvasExporterUtils.AlignmentHorizontal.STRETCH)
            {
                valw = target.rect.width / canvas.pixelRect.size.x * 100f;
                BabylonUtils.CreateCodeProperty(varName, "width", valw, BabylonUtils.Unit.PERCENT, ref n);
            }
            else
            {
                valw = target.rect.width;
                BabylonUtils.CreateCodeProperty(varName, "width", valw, BabylonUtils.Unit.PIXEL, ref n);
            }

            if (vertical == CanvasExporterUtils.AlignmentVertical.STRETCH)
            {
                valh = target.rect.height / canvas.pixelRect.size.y * 100f;
                BabylonUtils.CreateCodeProperty(varName, "height", valh, BabylonUtils.Unit.PERCENT, ref n);
            }
            else
            {
                valh = target.rect.height;
                BabylonUtils.CreateCodeProperty(varName, "height", valh, BabylonUtils.Unit.PIXEL, ref n);
            }
        }

        protected virtual void GenerateScale(ref string n)
        {
            var localScale = rectTransform.localScale;
            BabylonUtils.CreateCodeProperty(varName, "scaleX", localScale.x, ref n);
            BabylonUtils.CreateCodeProperty(varName, "scaleY", localScale.y, ref n);
        }

        protected virtual void GenerateHoverCursor(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "hoverCursor", controlAdapter.hoverCursor, ref n);
        }

        protected virtual void GeneratePosition(CanvasExporterUtils.AlignmentHorizontal horizontal,
            CanvasExporterUtils.AlignmentVertical vertical, ref string n)
        {
            var target = rectTransform;
            var swapVector = CanvasExporterUtils.SwapSignPosition(target.anchoredPosition);

            var pixelRect = canvas.pixelRect;
            var valx = swapVector.x / pixelRect.size.x * 100f;
            var valy = swapVector.y / pixelRect.size.y * 100f;

            if (vertical == CanvasExporterUtils.AlignmentVertical.CENTER)
            {
                BabylonUtils.CreateCodeProperty(varName, "top", swapVector.y, BabylonUtils.Unit.PIXEL, ref n);
                BabylonUtils.CreateCodeProperty(varName, "verticalAlignment",
                    $"BABYLON.GUI.Control.VERTICAL_ALIGNMENT_{vertical}", false, ref n);
            }
            else if (vertical != CanvasExporterUtils.AlignmentVertical.STRETCH)
            {
                var cal = swapVector.y >= 0
                    ? swapVector.y - target.rect.height / 2
                    : swapVector.y + target.rect.height / 2;
                BabylonUtils.CreateCodeProperty(varName, "top", cal, BabylonUtils.Unit.PIXEL, ref n);
                BabylonUtils.CreateCodeProperty(varName, "verticalAlignment",
                    $"BABYLON.GUI.Control.VERTICAL_ALIGNMENT_{vertical}", false, ref n);
            }
            else
            {
                BabylonUtils.CreateCodeProperty(varName, "top", valy, BabylonUtils.Unit.PERCENT, ref n);
                BabylonUtils.CreateCodeProperty(varName, "verticalAlignment",
                    $"BABYLON.GUI.Control.{CanvasExporterUtils.BabylonVerticalAlignment.VERTICAL_ALIGNMENT_CENTER}",
                    false, ref n);
            }

            if (horizontal == CanvasExporterUtils.AlignmentHorizontal.CENTER)
            {
                BabylonUtils.CreateCodeProperty(varName, "left", swapVector.x, BabylonUtils.Unit.PIXEL, ref n);
                BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment",
                    $"BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_{horizontal}", false, ref n);
            }
            else if (horizontal != CanvasExporterUtils.AlignmentHorizontal.STRETCH)
            {
                var cal = swapVector.x >= 0
                    ? swapVector.x - target.rect.width / 2
                    : swapVector.x + target.rect.width / 2;
                BabylonUtils.CreateCodeProperty(varName, "left", cal, BabylonUtils.Unit.PIXEL, ref n);
                BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment",
                    $"BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_{horizontal}", false, ref n);
            }
            else
            {
                BabylonUtils.CreateCodeProperty(varName, "left", valx, BabylonUtils.Unit.PERCENT, ref n);
                BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment",
                    $"BABYLON.GUI.Control.{CanvasExporterUtils.BabylonHorizontalAlignment.HORIZONTAL_ALIGNMENT_CENTER}",
                    false, ref n);
            }
        }
    }
}