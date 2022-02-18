using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonImage : BabylonControl, BabylonUI
	{
		private readonly Image image;
		private readonly ImageAdapter imageAdapter;

		public BabylonImage(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(uiName, gameObject, varName, zIndex, canvas)
		{
			image = gameObject.GetComponent<Image>();
			imageAdapter = gameObject.GetComponent<ImageAdapter>();
		}

		public void Generate(ref string n)
		{
			imageAdapter.UpdateUI();
			GenerateControl(ref n);

			GenerateDefault(ref n);
			GenerateSlice(ref n);
			GeneratePreserveAspect(ref n);
			GenerateDetectPointerOnOpaqueOnly(ref n);

			GenerateAddControl(ref n);
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.IMAGE;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		private void GenerateControl(ref string n)
		{
			var srcImg = "";
			if (image.sprite != null)
			{
				srcImg = AssetDatabase.GetAssetPath(image.sprite.texture);
				if (CanvasExporterUtils.isEmptyNullWhiteSpace(srcImg))
					srcImg = imageAdapter.sourceUrl;
			}
			else if (!CanvasExporterUtils.isEmptyNullWhiteSpace(imageAdapter.sourceUrl))
			{
				srcImg = imageAdapter.sourceUrl;
			}
			n += $"var {varName} = new BABYLON.GUI.Image(\"{gameObject.name}\", \"{srcImg}\");\n";
		}

		protected override void GenerateAlpha(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "alpha", image.color.a, ref n);
		}

		protected virtual void GenerateSlice(ref string n)
		{
			if (image.sprite != null)
			{
				if (image.type != Image.Type.Sliced) return;
				BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false, ref n);
			}
			else
			{
				if (!imageAdapter.populateNinePatchSlicesFromImage) return;
				BabylonUtils.CreateCodeProperty(varName, "populateNinePatchSlicesFromImage", true, ref n);
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NINE_PATCH", false, ref n);
			}
		}

		protected virtual void GenerateDetectPointerOnOpaqueOnly(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "detectPointerOnOpaqueOnly", imageAdapter.detectPointerOnOpaqueOnly, ref n);
		}

		protected virtual void GeneratePreserveAspect(ref string n)
		{
			if (image.type != Image.Type.Simple) return;
			if (image.preserveAspect)
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_UNIFORM", false, ref n);
			else
				BabylonUtils.CreateCodeProperty(varName, "stretch", "BABYLON.GUI.Image.STRETCH_NONE", false, ref n);
		}
	}
}
