using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(ContainerAdapter))]
	[CanEditMultipleObjects]
	public class ContainerAdapterEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (ContainerAdapter)target;
			DrawDefaultInspector();
			GUILayout.Label("Update the visual");
			if (GUILayout.Button("Update"))
				myTarget.UpdateUI();
		}
	}
}
