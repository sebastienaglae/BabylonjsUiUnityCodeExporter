using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    public sealed class BabylonScrollViewer : BabylonRectangle, IBabylonUI
    {
        private readonly ScrollViewerAdapter _scrollViewerAdapter;
        private readonly Image _image;
        private readonly ScrollRect _scrollRect;
        private readonly Scrollbar _scrollbarHorizontal;
        private readonly Scrollbar _scrollbarVertical;
        private readonly Image _scrollbarHorizontalImage;
        private readonly Image _scrollRectVerticalImage;

        public BabylonScrollViewer(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) :
            base(uiName, gameObject, varName, zIndex, canvas)
        {
            _scrollViewerAdapter = gameObject.GetComponent<ScrollViewerAdapter>();
            _image = gameObject.GetComponent<Image>();
            _scrollRect = gameObject.GetComponent<ScrollRect>();
            _scrollbarHorizontal = _scrollRect.horizontalScrollbar;
            _scrollbarVertical = _scrollRect.verticalScrollbar;
            _scrollbarHorizontalImage = _scrollbarHorizontal.image;
            _scrollRectVerticalImage = _scrollbarVertical.image;
        }

        public new CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.SCROLLVIEWER;
        }

        public new GameObject GetGameObject()
        {
            return gameObject;
        }

        public new UiProperties Generate()
        {
            _scrollViewerAdapter.UpdateUI();
            var n = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);
            GenerateBar(ref n);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            return uiProperties;
        }

        protected override void GenerateDefault(ref string n)
        {
            base.GenerateDefault(ref n);
            GenerateBorder(ref n);
        }

        private (string, string) GenerateControl()
        {
            return ($"new BABYLON.GUI.ScrollViewer(\"{gameObject.name}\");", "BABYLON.GUI.ScrollViewer");
        }

        protected void GenerateBar(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "barBackground", _scrollViewerAdapter.barBackground, ref n);
            BabylonUtils.CreateCodeProperty(varName, "barColor", _scrollViewerAdapter.barColor, ref n);
            if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_scrollViewerAdapter.barImageUrl))
                BabylonUtils.CreateCodeProperty(varName, "barImage",   $"new BABYLON.GUI.Image(\"{varName}_barImage\", \"{_scrollViewerAdapter.barImageUrl}\")", false, ref n);
            BabylonUtils.CreateCodeProperty(varName, "barImageHeight", _scrollViewerAdapter.barImageHeight, ref n);
            if ((_scrollRect.horizontal || _scrollRect.vertical) &&
                (_scrollbarHorizontal != null || _scrollbarVertical != null))
            {
                var barSize = _scrollbarHorizontal == null
                    ? _scrollbarVertical.GetComponent<RectTransform>().rect.width : _scrollbarHorizontal.GetComponent<RectTransform>().rect.height;
                BabylonUtils.CreateCodeProperty(varName, "barSize", barSize, ref n);
            }
        }
    }
}