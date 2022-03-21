using UnityEngine;

namespace PROJECT
{
    public class BabylonRectangle : BabylonContainer, IBabylonUI
    {
        private readonly RectangleAdapter _rectangleAdapter;

        public BabylonRectangle(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName, gameObject, varName, zIndex, canvas)
        {
            _rectangleAdapter = gameObject.GetComponent<RectangleAdapter>();
        }

        public new CanvasExporterUtils.Control GetControl()
        {
            return CanvasExporterUtils.Control.RECTANGLE;
        }

        public new GameObject GetGameObject()
        {
            return gameObject;
        }

        public new UiProperties Generate()
        {
            _rectangleAdapter.UpdateUI();
            var n = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            return uiProperties;
        }

        protected override void GenerateDefault(ref string n)
        {
            base.GenerateDefault(ref n);
            GenerateBorder(ref n);
        }

        private (string, string) GenerateControl()
        {
            return ($"new BABYLON.GUI.Rectangle(\"{gameObject.name}\");", "BABYLON.GUI.Rectangle");
        }

        protected virtual void GenerateBorder(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "cornerRadius", _rectangleAdapter.cornerRadius, ref n);
            BabylonUtils.CreateCodeProperty(varName, "thickness", _rectangleAdapter.thickness, ref n);
        }

        protected override void GenerateColor(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "color", _rectangleAdapter.color, ref n);
        }
    }
}