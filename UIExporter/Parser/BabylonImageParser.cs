using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PROJECT
{
    public class BabylonImageParser : IBabylonParser
    {
	    public bool CanParse(GameObject gameObject)
	    {
		    return gameObject.GetComponent<ImageAdapter>() != null;
	    }

	    public BabylonUI Parse(string uiName, GameObject gameObject, string varName, Canvas canvas)
	    {
		    return new BabylonImage(uiName, gameObject, varName, canvas);
	    }
    }
}
