using UnityEngine;

namespace PROJECT
{
    public class BabylonImageParser : IBabylonParser
    {
        public bool CanParse(GameObject gameObject)
        {
            var imageAdapter = gameObject.GetComponent<ImageAdapter>();
            if (imageAdapter == null) return false;
            return !imageAdapter.GetType().IsSubclassOf(typeof(ImageAdapter));
        }

        public IBabylonUI Parse(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas)
        {
            return new BabylonImage(uiName, gameObject, varName, zIndex, canvas);
        }
    }
}