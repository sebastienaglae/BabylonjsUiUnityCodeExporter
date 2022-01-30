using UnityEngine;

namespace PROJECT
{
	public class BabylonTextParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			return gameObject.GetComponent<TextAdapter>() != null;
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas)
		{
			return new BabylonText(uiName, gameObject, varName, canvas);
		}
	}
}
