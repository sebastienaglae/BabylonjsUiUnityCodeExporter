using UnityEngine;

namespace PROJECT
{
    public class BabylonTextParser : IBabylonParser
    {
        public bool CanParse(GameObject gameObject)
        {
            var textAdapter = gameObject.GetComponent<TextAdapter>();
            if (textAdapter == null) return false;
            return !textAdapter.GetType().IsSubclassOf(typeof(TextAdapter));
        }

        public IBabylonUI Parse(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas)
        {
            return new BabylonText(uiName, gameObject, varName, zIndex, canvas);
        }
    }
}