using UnityEditor;
using UnityEngine;

namespace PROJECT
{
    [CustomEditor(typeof(ButtonAdapter))]
    [CanEditMultipleObjects]
    public class ButtonAdapterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var myTarget = (ButtonAdapter) target;
            DrawDefaultInspector();
            GUILayout.Label("Update the visual");
            if (GUILayout.Button("Update"))
                myTarget.UpdateUI();
        }
    }
}