using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonButton : BabylonRectangle, BabylonUI
	{
		private readonly Button button;
		private readonly ButtonAdapter buttonAdapter;
		private readonly Image image;
		private readonly TextMeshProUGUI text;

		public BabylonButton(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(uiName,
		gameObject,
		varName, zIndex, canvas)
		{
			image = gameObject.GetComponent<Image>();
			text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			button = gameObject.GetComponent<Button>();
			buttonAdapter = gameObject.GetComponent<ButtonAdapter>();
		}


		public new void Generate(ref string n)
		{
			buttonAdapter.UpdateUI();
			GenerateControl(ref n);

			GenerateDefault(ref n);
			GenerateStyle(ref n);

			GenerateAddControl(ref n);
		}

		public new CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.BUTTON;
		}

		public new GameObject GetGameObject()
		{
			return gameObject;
		}

		private void GenerateControl(ref string n)
		{
			var imageLink = GetLinkImage();
			if (CanvasExporterUtils.isEmptyNullWhiteSpace(text.text) && !CanvasExporterUtils.isEmptyNullWhiteSpace(imageLink))
				if (buttonAdapter.iconButton)
					n += $"var {varName} = BABYLON.GUI.Button.CreateImageButton(\"{gameObject.name}\",\"\", \"{imageLink}\");\n";
				else
					n += $"var {varName} = BABYLON.GUI.Button.CreateImageOnlyButton(\"{gameObject.name}\", \"{imageLink}\");\n";
			else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(text.text) && !CanvasExporterUtils.isEmptyNullWhiteSpace(imageLink))
				if (buttonAdapter.iconButton)
					n += $"var {varName} = BABYLON.GUI.Button.CreateImageButton(\"{gameObject.name}\",\"{text.text}\", \"{imageLink}\");\n";
				else
					n += $"var {varName} = BABYLON.GUI.Button.CreateImageWithCenterTextButton(\"{gameObject.name}\",\"{text.text}\", \"{imageLink}\");\n";
			else
				n += $"var {varName} = BABYLON.GUI.Button.CreateSimpleButton(\"{gameObject.name}\", \"{text.text}\");\n";
		}
		
		protected virtual void GenerateStyle(ref string n)
		{
			var styleName = $"{varName}_style";
			var fontSize = text.fontSize;

			var fontStyle = TextUtils.GetFontStylePro(text);
			var fontWeight = TextUtils.GetFontWeightPro(text);
			n += $"var {styleName} = {uiName}.createStyle();\n";

			BabylonUtils.CreateCodeProperty(styleName, "fontSize", fontSize, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontStyle", fontStyle, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontFamily", buttonAdapter.fontFamily, true, ref n);
			BabylonUtils.CreateCodeProperty(styleName, "fontWeight", fontWeight, true, ref n);
			BabylonUtils.CreateCodeAffectation(varName, "style", styleName, ref n);
		}

		protected override void GenerateColor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "color", text.color, ref n);
		}

		protected virtual string GetLinkImage()
		{
			var srcImg = "";
			if (image.sprite != null)
			{
				srcImg = AssetDatabase.GetAssetPath(image.sprite.texture);
				if (CanvasExporterUtils.isEmptyNullWhiteSpace(srcImg))
					srcImg = buttonAdapter.imageUrl;
			}
			else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(buttonAdapter.imageUrl))
			{
				srcImg = buttonAdapter.imageUrl;
			}

			return srcImg;
		}
	}
}
