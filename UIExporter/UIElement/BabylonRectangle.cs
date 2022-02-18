using UnityEngine;

namespace PROJECT
{
	
public class BabylonRectangle : BabylonContainer, BabylonUI
{
	private readonly RectangleAdapter rectangleAdapter;

	public BabylonRectangle(string uiName, GameObject gameObject, string varName,int zIndex, Canvas canvas) : base(uiName, gameObject, varName, zIndex, canvas)
	{
		rectangleAdapter = gameObject.GetComponent<RectangleAdapter>();
	}

	public new CanvasExporterUtils.Control GetControl()
	{
		return CanvasExporterUtils.Control.RECTANGLE;
	}

	public new GameObject GetGameObject()
	{
		return gameObject;
	}

	public new void Generate(ref string n)
	{
		rectangleAdapter.UpdateUI();
		GenerateControl(ref n);

		GenerateDefault(ref n);

		GenerateAddControl(ref n);
	}

	protected override void GenerateDefault(ref string n)
	{
		base.GenerateDefault(ref n);
		GenerateBorder(ref n);
	}

	private void GenerateControl(ref string n)
	{
		n += $"var {varName} = new BABYLON.GUI.Rectangle(\"{gameObject.name}\");\n";
	}

	protected virtual void GenerateBorder(ref string n)
	{
		BabylonUtils.CreateCodeProperty(varName, "cornerRadius", rectangleAdapter.cornerRadius, ref n);
		BabylonUtils.CreateCodeProperty(varName, "thickness", rectangleAdapter.thickness, ref n);
	}

	protected override void GenerateColor(ref string n)
	{
		BabylonUtils.CreateCodeProperty(varName, "color", rectangleAdapter.color, ref n);
	}
}

}