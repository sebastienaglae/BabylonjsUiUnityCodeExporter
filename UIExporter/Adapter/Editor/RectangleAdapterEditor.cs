using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(RectangleAdapter))]
	[CanEditMultipleObjects]
	public class BabylonRectangleEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (RectangleAdapter)target;
			DrawDefaultInspector();
			GUILayout.Label("Update the visual");
			if (GUILayout.Button("Update"))
				myTarget.UpdateUI();
		}
	}
}
