using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    public sealed class BabylonButton : BabylonRectangle, IBabylonUI
    {
        private readonly Button _button;
        private readonly ButtonAdapter _buttonAdapter;
        private readonly Image _image;
        private readonly TextMeshProUGUI _text;

        public BabylonButton(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName,
            gameObject,
            varName, zIndex, canvas)
        {
            _image = gameObject.GetComponent<Image>();
            _text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _button = gameObject.GetComponent<Button>();
            _buttonAdapter = gameObject.GetComponent<ButtonAdapter>();
        }

        public new UiProperties Generate()
        {
            _buttonAdapter.UpdateUI();
            var n = "";
            var n0 = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);
            GenerateStyle(ref n0);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            uiProperties.AddExternalProperties(n0);
            return uiProperties;
        }

        public new CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.BUTTON;
        }

        public new GameObject GetGameObject()
        {
            return gameObject;
        }

        private (string, string) GenerateControl()
        {
            var imageLink = GetLinkImage();
            if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_text.text) ||
                CanvasExporterUtils.isEmptyNullWhiteSpace(imageLink))
            {
                if (CanvasExporterUtils.isEmptyNullWhiteSpace(_text.text) ||
                    CanvasExporterUtils.isEmptyNullWhiteSpace(imageLink))
                    return (
                        $"BABYLON.GUI.Button.CreateSimpleButton(\"{gameObject.name}\", \"{_text.text}\");",
                        "BABYLON.GUI.Button");
                if (_buttonAdapter.iconButton)
                    return (
                        $"BABYLON.GUI.Button.CreateImageButton(\"{gameObject.name}\",\"{_text.text}\", \"{imageLink}\");",
                        "BABYLON.GUI.Button");
                return (
                    $"BABYLON.GUI.Button.CreateImageWithCenterTextButton(\"{gameObject.name}\",\"{_text.text}\", \"{imageLink}\");",
                    "BABYLON.GUI.Button");
            }

            if (_buttonAdapter.iconButton)
                return ($"BABYLON.GUI.Button.CreateImageButton(\"{gameObject.name}\",\"\", \"{imageLink}\");",
                    "BABYLON.GUI.Button");
            return ($"BABYLON.GUI.Button.CreateImageOnlyButton(\"{gameObject.name}\", \"{imageLink}\");",
                "BABYLON.GUI.Button");
        }

        private void GenerateStyle(ref string n)
        {
            var styleName = $"{varName}_style";
            var fontSize = _text.fontSize;

            var fontStyle = TextUtils.GetFontStylePro(_text);
            var fontWeight = TextUtils.GetFontWeightPro(_text);
            n += $"let {styleName} = this.{uiName}.createStyle();\n";

            BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontFamily", _buttonAdapter.fontFamily, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
            BabylonUtils.CreateCodeAffectation("this." + varName, "style", styleName, ref n);
        }

        protected override void GenerateHighlight(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "highlightColor", _button.colors.highlightedColor, ref n);
            BabylonUtils.CreateCodeProperty(varName, "highlightLineWidth", controlAdapter.highlightLineWidth, ref n);
            BabylonUtils.CreateCodeProperty(varName, "isHighlighted", controlAdapter.isHighlighted, ref n);
        }

        protected override void GenerateColor(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "color", _text.color, ref n);
        }

        private string GetLinkImage()
        {
            var srcImg = "";
            if (_image.sprite != null)
            {
                srcImg = AssetDatabase.GetAssetPath(_image.sprite.texture);
                if (CanvasExporterUtils.isEmptyNullWhiteSpace(srcImg))
                    srcImg = _buttonAdapter.imageUrl;
            }
            else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_buttonAdapter.imageUrl))
            {
                srcImg = _buttonAdapter.imageUrl;
            }

            return srcImg;
        }
    }
}