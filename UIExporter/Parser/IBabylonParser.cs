using UnityEngine;

namespace PROJECT
{
	public interface IBabylonParser
	{
		bool CanParse(GameObject gameObject);
		BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas);
	}
}
