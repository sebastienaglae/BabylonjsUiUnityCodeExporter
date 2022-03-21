using UnityEngine;

namespace PROJECT
{
    public interface IBabylonParser
    {
        bool CanParse(GameObject gameObject);
        IBabylonUI Parse(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas);
    }
}