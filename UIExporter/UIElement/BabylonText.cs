using TMPro;
using UnityEngine;

namespace PROJECT
{
	public class BabylonText : BabylonControl, BabylonUI
	{
		private readonly TextAdapter textAdapter;
		private readonly TextMeshProUGUI textPro;

		public BabylonText(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(uiName, gameObject, varName, zIndex, canvas)
		{
			textPro = gameObject.GetComponent<TextMeshProUGUI>();
			textAdapter = gameObject.GetComponent<TextAdapter>();
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.TEXT_BLOCK;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		public void Generate(ref string n)
		{
			textAdapter.UpdateUI();
			GenerateControl(ref n);

			GenerateDefault(ref n);
			GenerateTextAlignment(ref n);
			GenerateTextOutline(ref n);
			GenerateUnderline(ref n);
			GenerateLineThrough(ref n);
			GenerateOverflow(ref n);
			GenerateLineSpacing(ref n);
			GenerateStyle(ref n);

			GenerateAddControl(ref n);
		}

		protected override void GenerateAlpha(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "alpha", textPro.color.a, ref n);
		}

		protected virtual void GenerateStyle(ref string n)
		{
			var styleName = $"{varName}_style";
			var fontSize = textPro.fontSize;

			var fontStyle = TextUtils.GetFontStylePro(textPro);
			var fontWeight = TextUtils.GetFontWeightPro(textPro);
			n += $"var {styleName} = {uiName}.createStyle();\n";

			BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontFamily", textAdapter.fontFamily, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
			BabylonUtils.CreateCodeAffectation(varName, "style", styleName, ref n);
		}

		protected virtual void GenerateLineSpacing(ref string n)
		{
			var value = textPro.lineSpacing;
			BabylonUtils.CreateCodeProperty(varName, "lineSpacing", value, BabylonUtils.Unit.ELEMENT_RELATIVE, ref n);
		}

		protected virtual void GenerateOverflow(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "textWrapping", $"BABYLON.GUI.TextWrapping.{TextUtils.GetOverflow(textPro)}", false, ref n);
		}

		protected virtual void GenerateUnderline(ref string n)
		{
			var value = TextUtils.GetFontOptions(FontStyles.Underline, textPro.fontStyle);
			BabylonUtils.CreateCodeProperty(varName, "underline", value, ref n);
		}

		protected virtual void GenerateLineThrough(ref string n)
		{
			var value = TextUtils.GetFontOptions(FontStyles.Strikethrough, textPro.fontStyle);
			BabylonUtils.CreateCodeProperty(varName, "lineThrough", value, ref n);
		}

		protected virtual void GenerateControl(ref string n)
		{
			n += $"var {varName} = new BABYLON.GUI.TextBlock(\"{gameObject.name}\", \"{CanvasExporterUtils.EncodeLine(textPro.text)}\");\n";
		}

		protected virtual void GenerateTextAlignment(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "textHorizontalAlignment", TextUtils.GetHorizontalAlignmentPro(textPro), ref n);
			BabylonUtils.CreateCodeProperty(varName, "textVerticalAlignment", TextUtils.GetVerticalAlignmentPro(textPro), ref n);
		}

		protected virtual void GenerateTextOutline(ref string n)
		{
			if (!textPro.materialForRendering.IsKeywordEnabled("OUTLINE_ON"))
				return;

			var color = textPro.outlineColor;
			BabylonUtils.CreateCodeProperty(varName, "outlineColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "outlineWidth", textPro.outlineWidth * 20f, ref n);
		}

		protected override void GenerateShadow(ref string n)
		{
			if (!textPro.materialForRendering.IsKeywordEnabled("UNDERLAY_ON"))
				return;

			var color = textPro.materialForRendering.GetColor("_UnderlayColor");
			var underlayOffsetX = textPro.materialForRendering.GetFloat("_UnderlayOffsetX");
			var underlayOffsetY = textPro.materialForRendering.GetFloat("_UnderlayOffsetY");
			var underlaySoftness = textPro.materialForRendering.GetFloat("_UnderlaySoftness");
			BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", underlayOffsetX * 2f, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", underlayOffsetY * -2f, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowBlur", underlaySoftness * 5f, ref n);
		}

		protected override void GenerateColor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "color", textPro.color, ref n);
		}
	}
}
