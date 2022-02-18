using System;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	
	public class BabylonScrollView : BabylonControl, BabylonUI
	{
		private readonly Image image;
		private readonly ScrollRect scrollRect;
		private readonly ScrollViewAdapter scrollRectAdapter;

		public BabylonScrollView(string uiName, GameObject gameObject, string varName,int zIndex, Canvas canvas) : base(uiName, gameObject, varName, zIndex, canvas)
		{
			scrollRect = gameObject.GetComponent<ScrollRect>();
			image = gameObject.GetComponent<Image>();
			scrollRectAdapter = gameObject.GetComponent<ScrollViewAdapter>();
		}

		public void Generate(ref string n)
		{
			scrollRectAdapter.UpdateUI();
			GenerateControl(ref n);

			CanvasExporterUtils.GenerateBackgroundColor(varName, image.color, ref n);
			GenerateDefault(ref n);

			CanvasExporterUtils.GenerateAddControl(varName, uiName, ref n);
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.SCROLLVIEWER;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		private void GenerateControl(ref string n)
		{
			n += $"var {varName} = new BABYLON.GUI.ScrollViewer(\"{gameObject.name}\");\n";
		}

		public string GetUniqueId()
		{
			throw new NotImplementedException();
		}
	}

}