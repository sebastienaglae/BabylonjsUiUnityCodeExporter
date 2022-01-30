using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonImage : BabylonControl, BabylonUI
	{
		private readonly Image image;
		private readonly ImageAdapter imageAdapter;

		public BabylonImage(string uiName, GameObject gameObject, string varName, Canvas canvas) : base(uiName, gameObject, varName, canvas)
		{
			image = gameObject.GetComponent<Image>();
			imageAdapter = gameObject.GetComponent<ImageAdapter>();
		}

		public void Generate(ref string n)
		{
			imageAdapter.UpdateUI();
			GenerateControl(ref n);
			
			CanvasExporterUtils.GenerateAlpha(varName, image.color, ref n);
			GenerateDefault(ref n);
			GenerateShadow(ref n);
			GenerateCursor(ref n);
			GenerateSlice(ref n);
			GeneratePreserveAspect(ref n);

			CanvasExporterUtils.GenerateAddControl(varName, uiName, ref n);
		}

		private void GenerateControl(ref string n)
		{
			var srcImg = "";
			if (image.sprite != null)
				srcImg = AssetDatabase.GetAssetPath(image.sprite.texture);
			else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(imageAdapter.urlImage))
				srcImg = imageAdapter.urlImage;
			n += $"var {varName} = new BABYLON.GUI.Image(\"{gameObject.name}\", \"{srcImg}\");\n";
		}

		private void GenerateSlice(ref string n)
		{
			if (image.sprite != null)
			{
				if (image.type != Image.Type.Sliced) return;
				BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false, ref n);
			}
			else
			{
				if (!imageAdapter.isSlicedUrl) return;
				BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false, ref n);
			}
		}

		private void GeneratePreserveAspect(ref string n)
		{
			if (image.sprite != null)
			{
				if (image.preserveAspect) return;
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_UNIFORM", false, ref n);
			}
			else
			{
				if (!imageAdapter.preserveAspectUrl) return;
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_UNIFORM", false, ref n);
			}
		}


		private void GenerateShadow(ref string n)
		{
			var color = imageAdapter.shadowColor;
			var shadowOffsetX = imageAdapter.shadowOffset.x;
			var shadowOffsetY = -imageAdapter.shadowOffset.y;
			var shadowBlur = imageAdapter.shadowBlur;
			BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", shadowOffsetX, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", shadowOffsetY, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowBlur", shadowBlur, ref n);
		}

		private void GenerateCursor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "hoverCursor", imageAdapter.cursor, ref n);
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.IMAGE;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}
	}
}
