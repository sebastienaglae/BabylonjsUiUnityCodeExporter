using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    public sealed class BabylonInputText : BabylonControl, IBabylonUI
    {
        private readonly Image _image;
        private readonly TMP_InputField _inputText;
        private readonly InputTextAdapter _inputTextAdapter;
        private readonly TextMeshProUGUI _placeholderText;
        private readonly TextMeshProUGUI _text;

        public BabylonInputText(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName, gameObject, varName, zIndex, canvas)
        {
            _image = gameObject.GetComponent<Image>();
            _inputText = gameObject.GetComponent<TMP_InputField>();
            _inputTextAdapter = gameObject.GetComponent<InputTextAdapter>();
            _text = _inputText.textComponent as TextMeshProUGUI;
            _placeholderText = _inputText.placeholder as TextMeshProUGUI;
        }

        public UiProperties Generate()
        {
            _inputTextAdapter.UpdateUI();
            var n = "";
            var n0 = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);
            GenerateBackground(ref n);
            GenerateBorder(ref n);
            GeneratePlaceholder(ref n);
            GenerateStyle(ref n0);
            GenerateText(ref n);
            GenerateReadOnly(ref n);
            GenerateShadow(ref n);
            GenerateDynamicSize(ref n);
            GenerateHighlight(ref n);
            GenerateMargin(ref n);
            GenerateFocus(ref n);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            uiProperties.AddExternalProperties(n0);
            return uiProperties;
        }

        public CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.INPUT_TEXT;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private (string, string) GenerateControl()
        {
            switch (_inputText.contentType)
            {
                case TMP_InputField.ContentType.Password:
                    return ("new BABYLON.GUI.InputPassword();", "BABYLON.GUI.InputPassword");
                case TMP_InputField.ContentType.Alphanumeric:
                case TMP_InputField.ContentType.Standard:
                case TMP_InputField.ContentType.Autocorrected:
                case TMP_InputField.ContentType.IntegerNumber:
                case TMP_InputField.ContentType.DecimalNumber:
                case TMP_InputField.ContentType.Name:
                case TMP_InputField.ContentType.EmailAddress:
                case TMP_InputField.ContentType.Pin:
                case TMP_InputField.ContentType.Custom:
                default:
                    return ("new BABYLON.GUI.InputText();", "BABYLON.GUI.InputText");
            }
        }

        private void GenerateBorder(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "thickness", _inputTextAdapter.borderThickness, ref n);
            BabylonUtils.CreateCodeProperty(varName, "focusedColor", _inputTextAdapter.borderColorFocused, ref n);
        }

        private void GenerateDynamicSize(ref string n)
        {
            if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_inputTextAdapter.maxWidth))
                BabylonUtils.CreateCodeProperty(varName, "maxWidth", _inputTextAdapter.maxWidth, true, ref n);
            BabylonUtils.CreateCodeProperty(varName, "autoStretchWidth", _inputTextAdapter.autoStretchWidth, ref n);
        }

        private void GenerateReadOnly(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "isReadOnly", _inputText.readOnly, ref n);
        }

        private void GenerateBackground(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "background", _image.color, ref n);
            BabylonUtils.CreateCodeProperty(varName, "focusedBackground", _inputText.colors.selectedColor, ref n);
            BabylonUtils.CreateCodeProperty(varName, "alpha", _image.color.a, ref n);
        }

        private void GeneratePlaceholder(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "placeholderText", _placeholderText.text, true, ref n);
            BabylonUtils.CreateCodeProperty(varName, "placeholderColor", _placeholderText.color, ref n);
        }

        private void GenerateText(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "text", _text.text, true, ref n);
            BabylonUtils.CreateCodeProperty(varName, "color", _text.color, ref n);
        }

        private void GenerateStyle(ref string n)
        {
            var styleName = $"{varName}_style";
            var fontSize = _placeholderText.fontSize;

            var fontStyle = TextUtils.GetFontStylePro(_placeholderText);
            var fontWeight = TextUtils.GetFontWeightPro(_placeholderText);
            n += $"let {styleName} = this.{uiName}.createStyle();\n";

            BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontFamily", _inputTextAdapter.fontFamily, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
            BabylonUtils.CreateCodeAffectation("this." + varName, "style", styleName, ref n);
        }

        protected override void GenerateShadow(ref string n)
        {
            var color = _inputTextAdapter.shadowColor;
            var shadowOffsetX = _inputTextAdapter.shadowOffset.x;
            var shadowOffsetY = -_inputTextAdapter.shadowOffset.y;
            var shadowBlur = _inputTextAdapter.shadowBlur;
            BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", shadowOffsetX, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", shadowOffsetY, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowBlur", shadowBlur, ref n);
        }

        protected override void GenerateHighlight(ref string n)
        {
            base.GenerateHighlight(ref n);
            BabylonUtils.CreateCodeProperty(varName, "highligherOpacity", _inputTextAdapter.highligherOpacity, ref n);
            BabylonUtils.CreateCodeProperty(varName, "highlightedText", _inputTextAdapter.highlightedText, true, ref n);
            BabylonUtils.CreateCodeProperty(varName, "textHighlightColor", _inputTextAdapter.textHighlightColor, ref n);
        }

        private void GenerateMargin(ref string n)
        {
            if (!CanvasExporterUtils.isEmptyNullWhiteSpace(_inputTextAdapter.margin))
                BabylonUtils.CreateCodeProperty(varName, "margin", _inputTextAdapter.margin, true, ref n);
        }

        private void GenerateFocus(ref string n)
        {
            if (_inputTextAdapter.focus)
                BabylonUtils.CreateCodeMethod(varName, "focus", "", ref n);
        }
    }
}