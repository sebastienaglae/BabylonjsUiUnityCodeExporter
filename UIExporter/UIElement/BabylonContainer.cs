using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	public class BabylonContainer : BabylonControl, BabylonUI
	{
		private readonly ContainerAdapter containerAdapter;
		private readonly Image image;

		public BabylonContainer(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(uiName, gameObject, varName, zIndex, canvas)
		{
			containerAdapter = gameObject.GetComponent<ContainerAdapter>();
			image = gameObject.GetComponent<Image>();
		}

		public void Generate(ref string n)
		{
			containerAdapter.UpdateUI();
			GenerateControl(ref n);

			GenerateDefault(ref n);

			GenerateAddControl(ref n);
		}

		public CanvasExporterUtils.Control GetControl()
		{
			return CanvasExporterUtils.Control.CONTAINER;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		protected override void GenerateDefault(ref string n)
		{
			base.GenerateDefault(ref n);
			GenerateAdapt(ref n);
			GenerateBackground(ref n);
			GenerateAlpha(ref n);
		}

		private void GenerateControl(ref string n)
		{
			n += $"var {varName} = new BABYLON.GUI.Container(\"{gameObject.name}\");\n";
		}

		protected virtual void GenerateAdapt(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "adaptWidthToChildren", containerAdapter.adaptWidthToChildren, ref n);
			BabylonUtils.CreateCodeProperty(varName, "adaptHeightToChildren", containerAdapter.adaptHeightToChildren, ref n);
		}

		protected override void GenerateAddControl(ref string n)
		{
			if (containerAdapter.parent != null)
				BabylonUtils.CreateCodeMethod(containerAdapter.parent.uniqueID, "addControl", varName, ref n);
			else
				BabylonUtils.CreateCodeMethod(uiName, "addControl", varName, ref n);
		}

		protected virtual void GenerateBackground(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "background", image.color, ref n);
		}

		protected override void GenerateAlpha(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "alpha", image.color.a, ref n);
		}
	}
}
