using UnityEngine;

namespace PROJECT
{
    public class BabylonInputTextParser : IBabylonParser
    {
        public bool CanParse(GameObject gameObject)
        {
            var inputTextAdapter = gameObject.GetComponent<InputTextAdapter>();
            if (inputTextAdapter == null) return false;
            return !inputTextAdapter.GetType().IsSubclassOf(typeof(InputTextAdapter));
        }

        public IBabylonUI Parse(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas)
        {
            return new BabylonInputText(uiName, gameObject, varName, zIndex, canvas);
        }
    }
}