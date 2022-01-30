using TMPro;
using UnityEngine;

namespace PROJECT
{
	public class BabylonText : BabylonControl, BabylonUI
	{
		private readonly TextAdapter textAdapter;
		private readonly TextMeshProUGUI textPro;

		public BabylonText(string uiName, GameObject gameObject, string varName, Canvas canvas) : base(uiName, gameObject, varName, canvas)
		{
			textPro = gameObject.GetComponent<TextMeshProUGUI>();
			textAdapter = gameObject.GetComponent<TextAdapter>();
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.TEXT;
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
			CanvasExporterUtils.GenerateAlpha(varName, textPro.color, ref n);
			GenerateTextAlignment(ref n);
			GenerateFontColor(ref n);
			GenerateTextOutline(ref n);
			GenerateShadow(ref n);
			GenerateUnderline(ref n);
			GenerateLineThrough(ref n);
			GenerateOverflow(ref n);
			GenerateLineSpacing(ref n);
			GenerateCursor(ref n);

			GenerateStyle(ref n);
			CanvasExporterUtils.GenerateAddControl(varName, uiName, ref n);
		}

		private void GenerateStyle(ref string n)
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

		private void GenerateCursor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "hoverCursor", textAdapter.cursor, ref n);
		}

		private void GenerateLineSpacing(ref string n)
		{
			var value = textPro.lineSpacing;
			BabylonUtils.CreateCodeProperty(varName, "lineSpacing", value, BabylonUtils.Unit.ELEMENT_RELATIVE, ref n);
		}

		private void GenerateOverflow(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "textWrapping", $"BABYLON.GUI.TextWrapping.{TextUtils.GetOverflow(textPro)}", false, ref n);
		}

		private void GenerateUnderline(ref string n)
		{
			var value = TextUtils.GetFontOptions(FontStyles.Underline, textPro.fontStyle);
			BabylonUtils.CreateCodeProperty(varName, "underline", value, ref n);
		}

		private void GenerateLineThrough(ref string n)
		{
			var value = TextUtils.GetFontOptions(FontStyles.Strikethrough, textPro.fontStyle);
			BabylonUtils.CreateCodeProperty(varName, "lineThrough", value, ref n);
		}

		private void GenerateControl(ref string n)
		{
			n += $"var {varName} = new BABYLON.GUI.TextBlock(\"{gameObject.name}\", \"{CanvasExporterUtils.EncodeLine(textPro.text)}\");\n";
		}

		private void GenerateTextAlignment(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "textHorizontalAlignment", TextUtils.GetHorizontalAlignmentPro(textPro), ref n);
			BabylonUtils.CreateCodeProperty(varName, "textVerticalAlignment", TextUtils.GetVerticalAlignmentPro(textPro), ref n);
		}

		private void GenerateTextOutline(ref string n)
		{
			if (!textPro.materialForRendering.IsKeywordEnabled("OUTLINE_ON"))
				return;

			var color = textPro.outlineColor;
			BabylonUtils.CreateCodeProperty(varName, "outlineColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "outlineWidth", textPro.outlineWidth * 20f, ref n);
		}

		private void GenerateShadow(ref string n)
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

		private void GenerateFontColor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "color", textPro.color, ref n);
		}
	}
}
