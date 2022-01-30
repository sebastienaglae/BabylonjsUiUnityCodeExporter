using UnityEngine;

namespace PROJECT
{
	public class BabylonInputTextParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			return gameObject.GetComponent<InputTextAdapter>() != null;
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas)
		{
			return new BabylonInputText(uiName, gameObject, varName, canvas);
		}
	}
}
