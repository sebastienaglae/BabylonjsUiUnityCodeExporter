using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(InputTextAdapter))]
	[CanEditMultipleObjects]
	public class InputTextAdapterEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (InputTextAdapter)target;
			DrawDefaultInspector();
			if (GUILayout.Button("Update ui"))
				myTarget.UpdateUI();
		}
	}
}
