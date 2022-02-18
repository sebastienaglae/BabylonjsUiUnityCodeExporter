using UnityEngine;

namespace PROJECT
{
	public class BabylonContainerParser : IBabylonParser
	{
		public bool CanParse(GameObject gameObject)
		{
			var containerAdapter = gameObject.GetComponent<ContainerAdapter>();
			if (containerAdapter == null) return false;
			return !containerAdapter.GetType().IsSubclassOf(typeof(ContainerAdapter));
		}

		public BabylonUI Parse(string uiName, GameObject gameObject, string varName,int zIndex, Canvas canvas)
		{
			return new BabylonContainer(uiName, gameObject, varName,  zIndex, canvas);
		}
	}

}