using UnityEditor;
using UnityEngine;

namespace PROJECT
{
    [CustomEditor(typeof(CanvasExporter))]
    public class CanvasExporterEditor : Editor
    {
        bool createFileToggle = true;
        public override void OnInspectorGUI()
        {
            var babylonCanvas = (CanvasExporter) target;
            DrawDefaultInspector();
            if (GUILayout.Button("Update ui"))
                babylonCanvas.GetUiElement();

            createFileToggle = GUILayout.Toggle(createFileToggle, "Create file");
            if (GUILayout.Button("Generate class"))
            {
                if (babylonCanvas.GetUiElement())
                    babylonCanvas.GenerateClassCode(createFileToggle);
            }
        }
    }
}