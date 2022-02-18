using UnityEngine;

namespace PROJECT
{
	public class BabylonButtonParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			var buttonAdapter = gameObject.GetComponent<ButtonAdapter>();
			if (buttonAdapter == null) return false;
			return !buttonAdapter.GetType().IsSubclassOf(typeof(ButtonAdapter));
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName,int zIndex, Canvas canvas)
		{
			return new BabylonButton(uiName, gameObject, varName,  zIndex, canvas);
		}
	}
}
