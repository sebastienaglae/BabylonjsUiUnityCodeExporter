using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    public sealed class BabylonImage : BabylonControl, IBabylonUI
    {
        private readonly Image _image;
        private readonly ImageAdapter _imageAdapter;

        public BabylonImage(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName, gameObject, varName, zIndex, canvas)
        {
            _image = gameObject.GetComponent<Image>();
            _imageAdapter = gameObject.GetComponent<ImageAdapter>();
        }

        public UiProperties Generate()
        {
            _imageAdapter.UpdateUI();
            var n = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);
            GenerateSlice(ref n);
            GeneratePreserveAspect(ref n);
            GenerateDetectPointerOnOpaqueOnly(ref n);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            return uiProperties;
        }

        public CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.IMAGE;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private (string, string) GenerateControl()
        {
            var srcImg = "";
            if (_image.sprite != null)
            {
                srcImg = AssetDatabase.GetAssetPath(_image.sprite.texture);
                if (CanvasExporterUtils.isEmptyNullWhiteSpace(srcImg))
                    srcImg = _imageAdapter.sourceUrl;
            }
            else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_imageAdapter.sourceUrl))
            {
                srcImg = _imageAdapter.sourceUrl;
            }

            return ($"new BABYLON.GUI.Image(\"{gameObject.name}\", \"{srcImg}\");", "BABYLON.GUI.Image");
        }

        protected override void GenerateAlpha(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "alpha", _image.color.a, ref n);
        }

        private void GenerateSlice(ref string n)
        {
            if (_image.sprite != null)
            {
                if (_image.type != Image.Type.Sliced) return;
                BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
                BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false,
                    ref n);
            }
            else
            {
                if (!_imageAdapter.populateNinePatchSlicesFromImage) return;
                BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
                BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false,
                    ref n);
            }
        }

        private void GenerateDetectPointerOnOpaqueOnly(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "detectPointerOnOpaqueOnly",
                _imageAdapter.detectPointerOnOpaqueOnly, ref n);
        }

        private void GeneratePreserveAspect(ref string n)
        {
            if (_image.type != Image.Type.Simple) return;
            if (_image.preserveAspect)
                BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_UNIFORM", false, ref n);
            else
                BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NONE", false, ref n);
        }
    }
}