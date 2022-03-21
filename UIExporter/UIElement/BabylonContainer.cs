using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    public class BabylonContainer : BabylonControl, IBabylonUI
    {
        private readonly ContainerAdapter _containerAdapter;
        private readonly Image _image;

        public BabylonContainer(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas) : base(
            uiName, gameObject, varName, zIndex, canvas)
        {
            _containerAdapter = gameObject.GetComponent<ContainerAdapter>();
            _image = gameObject.GetComponent<Image>();
        }

        public UiProperties Generate()
        {
            _containerAdapter.UpdateUI();
            var n = "";
            var uiProperties = new UiProperties(GenerateControl(), varName);

            GenerateDefault(ref n);

            GenerateAddControl(ref n);

            uiProperties.AddProperties(n);
            return uiProperties;
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

        private (string, string) GenerateControl()
        {
            return ($"new BABYLON.GUI.Container(\"{gameObject.name}\");", "BABYLON.GUI.Container");
        }

        protected virtual void GenerateAdapt(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "adaptWidthToChildren", _containerAdapter.adaptWidthToChildren,
                ref n);
            BabylonUtils.CreateCodeProperty(varName, "adaptHeightToChildren", _containerAdapter.adaptHeightToChildren,
                ref n);
        }

        protected override void GenerateAddControl(ref string n)
        {
            if (_containerAdapter.parent != null)
                BabylonUtils.CreateCodeMethod(_containerAdapter.parent.uniqueID, "addControl", "this." + varName,
                    ref n);
            else
                BabylonUtils.CreateCodeMethod(uiName, "addControl", "this." + varName, ref n);
        }

        protected virtual void GenerateBackground(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "background", _image.color, ref n);
        }

        protected override void GenerateAlpha(ref string n)
        {
            BabylonUtils.CreateCodeProperty(varName, "alpha", _image.color.a, ref n);
        }
    }
}