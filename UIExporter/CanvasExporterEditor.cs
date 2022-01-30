using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	[CustomEditor(typeof(CanvasExporter))]
	public class CanvasExporterEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var myTarget = (CanvasExporter)target;
			DrawDefaultInspector();
			if (GUILayout.Button("Update ui"))
				myTarget.GetUiElement();
			if (GUILayout.Button("Generate ui"))
				if (myTarget.GetUiElement())
					myTarget.GenerateUi();
		}
	}
}
