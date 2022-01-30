using UnityEngine;

namespace PROJECT
{
	public class BabylonRectangleParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			return gameObject.GetComponent<RectangleAdapter>() != null;
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas)
		{
			return new BabylonRectangle(uiName, gameObject, varName, canvas);
		}
	}
}
