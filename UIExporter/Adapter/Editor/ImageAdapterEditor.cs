using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(ImageAdapter))]
	[CanEditMultipleObjects]
	public class BabylonImageEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (ImageAdapter)target;
			DrawDefaultInspector();
			if (GUILayout.Button("Update ui"))
				myTarget.UpdateUI();
		}
	}
}
