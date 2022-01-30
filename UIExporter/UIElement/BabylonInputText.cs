using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonInputText : BabylonControl, BabylonUI
	{
		private readonly Image image;
		private readonly TMP_InputField inputText;
		private readonly InputTextAdapter inputTextAdapter;
		private readonly TextMeshProUGUI placeholderText;
		private readonly TextMeshProUGUI text;

		public BabylonInputText(string uiName, GameObject gameObject, string varName, Canvas canvas) : base(uiName, gameObject, varName, canvas)
		{
			image = gameObject.GetComponent<Image>();
			inputText = gameObject.GetComponent<TMP_InputField>();
			inputTextAdapter = gameObject.GetComponent<InputTextAdapter>();
			text = inputText.textComponent as TextMeshProUGUI;
			placeholderText = inputText.placeholder as TextMeshProUGUI;
		}

		public void Generate(ref string n)
		{
			inputTextAdapter.UpdateUI();
			GenerateControl(ref n);
			
			GenerateDefault(ref n);
			GenerateBackground(ref n);
			GenerateBorder(ref n);
			GeneratePlaceholder(ref n);
			GenerateStyle(ref n);
			GenerateText(ref n);
			GenerateReadOnly(ref n);
			GenerateCursor(ref n);
			GenerateShadow(ref n);
			GenerateDynamicSize(ref n);

			CanvasExporterUtils.GenerateAddControl(varName, uiName, ref n);
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.INPUT_TEXT;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		private void GenerateControl(ref string n)
		{
			switch (inputText.contentType)
			{
			case TMP_InputField.ContentType.Password:
				n += $"var {varName} = new BABYLON.GUI.InputPassword();\n";
				break;
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
				n += $"var {varName} = new BABYLON.GUI.InputText();\n";
				break;
			}
		}

		private void GenerateBorder(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "thickness", inputTextAdapter.borderThickness, ref n);
			BabylonUtils.CreateCodeProperty(varName, "focusedColor", inputTextAdapter.borderColorFocused, ref n);
		}

		private void GenerateDynamicSize(ref string n)
		{
			if (!CanvasExporterUtils.isEmptyNullWhiteSpace(inputTextAdapter.maxWidth))
				BabylonUtils.CreateCodeProperty(varName, "maxWidth", inputTextAdapter.maxWidth, true, ref n);
			BabylonUtils.CreateCodeProperty(varName, "autoStretchWidth", inputTextAdapter.autoStretchWidth, ref n);
		}

		private void GenerateReadOnly(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "isReadOnly", inputText.readOnly, ref n);
		}

		private void GenerateCursor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "hoverCursor", inputTextAdapter.cursor, ref n);
		}

		private void GenerateBackground(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "background", image.color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "focusedBackground", inputText.colors.selectedColor, ref n);
			BabylonUtils.CreateCodeProperty(varName, "alpha", image.color.a, ref n);
		}

		private void GeneratePlaceholder(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "placeholderText", placeholderText.text, true, ref n);
			BabylonUtils.CreateCodeProperty(varName, "placeholderColor", placeholderText.color, ref n);
		}

		private void GenerateText(ref string n)
		{
			if (!CanvasExporterUtils.isEmptyNullWhiteSpace(text.text))
				BabylonUtils.CreateCodeProperty(varName, "text", text.text, true, ref n);
			BabylonUtils.CreateCodeProperty(varName, "color", text.color, ref n);
		}

		private void GenerateStyle(ref string n)
		{
			var styleName = $"{varName}_style";
			var fontSize = placeholderText.fontSize;

			var fontStyle = TextUtils.GetFontStylePro(placeholderText);
			var fontWeight = TextUtils.GetFontWeightPro(placeholderText);
			n += $"var {styleName} = {uiName}.createStyle();\n";

			BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontFamily", inputTextAdapter.fontFamily, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
			BabylonUtils.CreateCodeAffectation(varName, "style", styleName, ref n);
		}

		private void GenerateShadow(ref string n)
		{
			var color = inputTextAdapter.shadowColor;
			var shadowOffsetX = inputTextAdapter.shadowOffset.x;
			var shadowOffsetY = -inputTextAdapter.shadowOffset.y;
			var shadowBlur = inputTextAdapter.shadowBlur;
			BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", shadowOffsetX, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", shadowOffsetY, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowBlur", shadowBlur, ref n);
		}
	}
}
