using UnityEngine;

namespace PROJECT
{
	public class BabylonScrollViewerParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			var scrollViewerAdapter = gameObject.GetComponent<ScrollViewerAdapter>();
			if (scrollViewerAdapter == null) return false;
			return !scrollViewerAdapter.GetType().IsSubclassOf(typeof(ScrollViewerAdapter));
		}

		public IBabylonUI Parse(string uiName, GameObject gameObject, string varName, int zIndex, Canvas canvas)
		{
			return new BabylonScrollViewer(uiName, gameObject, varName, zIndex, canvas);
		}
	}
}
