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
            var myTarget = (InputTextAdapter) target;
            DrawDefaultInspector();
            GUILayout.Label("Update the visual");
            if (GUILayout.Button("Update"))
                myTarget.UpdateUI();
        }
    }
}