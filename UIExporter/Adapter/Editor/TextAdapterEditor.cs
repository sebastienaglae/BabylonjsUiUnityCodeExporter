using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(TextAdapter))]
	[CanEditMultipleObjects]
	public class TextImageEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (TextAdapter)target;
			DrawDefaultInspector();
			GUILayout.Label("Update the visual");
			if (GUILayout.Button("Update"))
				myTarget.UpdateUI();
		}
	}
}
