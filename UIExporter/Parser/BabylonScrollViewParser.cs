using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PROJECT
{
	public class BabylonScrollViewParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			return gameObject.GetComponent<ScrollViewAdapter>() != null;
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas)
		{
			return new BabylonScrollView(uiName, gameObject, varName, canvas);
		}
	}
}
