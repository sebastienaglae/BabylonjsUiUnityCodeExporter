using TMPro;
using UnityEngine;

namespace PROJECT
{
    public sealed class BabylonText : BabylonControl, IBabylonUI
    {
        private readonly TextAdapter _textAdapter;
        private readonly TextMeshProUGUI _textPro;
        private static readonly int UnderlayColor = Shader.PropertyToID("_UnderlayColor");
        private static readonly int UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
        private static readonly int UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
        private static readonly int UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");

        public BabylonText(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName, gameObject, varName, zIndex, canvas)
        {
            _textPro = gameObject.GetComponent<TextMeshProUGUI>();
            _textAdapter = gameObject.GetComponent<TextAdapter>();
        }

        public CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.TEXT_BLOCK;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public UiProperties Generate()
        {
            _textAdapter.UpdateUI();
            var n = "";
            var n0 = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);
            GenerateTextAlignment(ref n);
            GenerateTextOutline(ref n);
            GenerateUnderline(ref n);
            GenerateLineThrough(ref n);
            GenerateOverflow(ref n);
            GenerateLineSpacing(ref n);
            GenerateStyle(ref n0);

            GenerateAddControl(ref n);
            uiProperties.AddProperties(n);
            uiProperties.AddExternalProperties(n0);
            return uiProperties;
        }

        protected override void GenerateAlpha(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "alpha", _textPro.color.a, ref n);
        }

        private void GenerateStyle(ref string n)
        {
            var styleName = $"{varName}_style";
            var fontSize = _textPro.fontSize;

            var fontStyle = TextUtils.GetFontStylePro(_textPro);
            var fontWeight = TextUtils.GetFontWeightPro(_textPro);
            n += $"let {styleName} = this.{uiName}.createStyle();\n";

            BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontFamily", _textAdapter.fontFamily, true, ref n);
            BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
            BabylonUtils.CreateCodeAffectation("this." + varName, "style", styleName, ref n);
        }

        private void GenerateLineSpacing(ref string n)
        {
            var value = _textPro.lineSpacing;
            BabylonUtils.CreateCodeProperty(varName, "lineSpacing", value, BabylonUtils.Unit.ELEMENT_RELATIVE, ref n);
        }

        private void GenerateOverflow(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "textWrapping",
                $"BABYLON.GUI.TextWrapping.{TextUtils.GetOverflow(_textPro)}", false, ref n);
        }

        private void GenerateUnderline(ref string n)
        {
            var value = TextUtils.GetFontOptions(FontStyles.Underline, _textPro.fontStyle);
            BabylonUtils.CreateCodeProperty(varName, "underline", value, ref n);
        }

        private void GenerateLineThrough(ref string n)
        {
            var value = TextUtils.GetFontOptions(FontStyles.Strikethrough, _textPro.fontStyle);
            BabylonUtils.CreateCodeProperty(varName, "lineThrough", value, ref n);
        }

        private (string, string) GenerateControl()
        {
            return (
                $"new BABYLON.GUI.TextBlock(\"{gameObject.name}\", \"{CanvasExporterUtils.EncodeLine(_textPro.text)}\");",
                "BABYLON.GUI.TextBlock");
        }

        private void GenerateTextAlignment(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "textHorizontalAlignment",
                TextUtils.GetHorizontalAlignmentPro(_textPro), ref n);
            BabylonUtils.CreateCodeProperty(varName, "textVerticalAlignment",
                TextUtils.GetVerticalAlignmentPro(_textPro), ref n);
        }

        private void GenerateTextOutline(ref string n)
        {
            if (!_textPro.materialForRendering.IsKeywordEnabled("OUTLINE_ON"))
                return;

            var color = _textPro.outlineColor;
            BabylonUtils.CreateCodeProperty(varName, "outlineColor", color, ref n);
            BabylonUtils.CreateCodeProperty(varName, "outlineWidth", _textPro.outlineWidth * 20f, ref n);
        }

        protected override void GenerateShadow(ref string n)
        {
            if (!_textPro.materialForRendering.IsKeywordEnabled("UNDERLAY_ON"))
                return;

            var color = _textPro.materialForRendering.GetColor(UnderlayColor);
            var underlayOffsetX = _textPro.materialForRendering.GetFloat(UnderlayOffsetX);
            var underlayOffsetY = _textPro.materialForRendering.GetFloat(UnderlayOffsetY);
            var underlaySoftness = _textPro.materialForRendering.GetFloat(UnderlaySoftness);
            BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", underlayOffsetX * 2f, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", underlayOffsetY * -2f, ref n);
            BabylonUtils.CreateCodeProperty(varName, "shadowBlur", underlaySoftness * 5f, ref n);
        }

        protected override void GenerateColor(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "color", _textPro.color, ref n);
        }
    }
}