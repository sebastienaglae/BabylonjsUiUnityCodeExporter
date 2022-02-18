using UnityEngine;
using static PROJECT.CanvasExporterUtils;

namespace PROJECT
{
	
	public interface BabylonUI
	{
		void Generate(ref string n);
		Control GetControl();
		GameObject GetGameObject();
	}

}