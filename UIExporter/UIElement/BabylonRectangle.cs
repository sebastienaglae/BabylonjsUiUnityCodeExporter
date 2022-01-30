using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonRectangle : BabylonControl, BabylonUI
	{
		private readonly Image image;
		private readonly RectangleAdapter rectangleAdapter;

		public BabylonRectangle(string uiName, GameObject gameObject, string varName, Canvas canvas) : base(uiName, gameObject, varName, canvas)
		{
			image = gameObject.GetComponent<Image>();
			rectangleAdapter = gameObject.GetComponent<RectangleAdapter>();
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.RECTANGLE;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		public void Generate(ref string n)
		{
			rectangleAdapter.UpdateUI();
			GenerateControl(ref n);
			CanvasExporterUtils.GenerateBackgroundColor(varName, image.color, ref n);
			GenerateDefault(ref n);
			GenerateBorder(ref n);
			GenerateShadow(ref n);
			GenerateCursor(ref n);

			CanvasExporterUtils.GenerateAddControl(varName, uiName, ref n);
		}

		private void GenerateControl(ref string n)
		{
			n += $"var {varName} = new BABYLON.GUI.Rectangle(\"{gameObject.name}\");\n";
		}

		private void GenerateBorder(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "color", rectangleAdapter.borderColor, ref n);
			BabylonUtils.CreateCodeProperty(varName, "cornerRadius", rectangleAdapter.borderRadius, ref n);
			BabylonUtils.CreateCodeProperty(varName, "thickness", rectangleAdapter.borderThickness, ref n);
		}

		private void GenerateCursor(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "hoverCursor", rectangleAdapter.cursor, ref n);
		}

		private void GenerateShadow(ref string n)
		{
			var color = rectangleAdapter.shadowColor;
			var shadowOffsetX = rectangleAdapter.shadowOffset.x;
			var shadowOffsetY = -rectangleAdapter.shadowOffset.y;
			var shadowBlur = rectangleAdapter.shadowBlur;
			BabylonUtils.CreateCodeProperty(varName, "shadowColor", color, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetX", shadowOffsetX, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowOffsetY", shadowOffsetY, ref n);
			BabylonUtils.CreateCodeProperty(varName, "shadowBlur", shadowBlur, ref n);
		}
	}
}
