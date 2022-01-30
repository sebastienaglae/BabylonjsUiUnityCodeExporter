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
			if (GUILayout.Button("Update ui"))
				myTarget.UpdateUI();
		}
	}
}
