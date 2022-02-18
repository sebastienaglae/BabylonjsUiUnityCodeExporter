using UnityEngine;

namespace PROJECT
{
	public class BabylonRectangleParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			var rectangleAdapter = gameObject.GetComponent<RectangleAdapter>();
			if (rectangleAdapter == null) return false;
			return !rectangleAdapter.GetType().IsSubclassOf(typeof(RectangleAdapter));
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName,int zIndex, Canvas canvas)
		{
			return new BabylonRectangle(uiName, gameObject, varName,  zIndex, canvas);
		}
	}

}