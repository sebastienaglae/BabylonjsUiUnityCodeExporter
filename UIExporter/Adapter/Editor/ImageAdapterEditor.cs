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
			GUILayout.Label("Update the visual");
			if (GUILayout.Button("Update"))
				myTarget.UpdateUI();
		}
	}
}
