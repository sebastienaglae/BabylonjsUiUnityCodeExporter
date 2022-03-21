using System;
using UnityEditor;
using UnityEngine;

/**
* Editor Script Component
* @class TestUi
*/
[Babylon(Class="PROJECT.TestUi"), AddComponentMenu("Scripts/My Project/TestUi")]
public class TestUi : EditorScriptComponent
{
    /* Add Editor Properties To Script Component */
    // Example: [Tooltip("Example hello world property")]
    // Example: [Auto] public string helloWorld = "Hello World";

	/* [Serializable, HideInInspector] public string exportProperty = null; */
    public override void OnUpdateProperties(Transform transform, SceneExporterTool exporter)
    {
        // Example: this.helloWorld = "Update Hello World";
    }
}

// Optional Script Component Custom Editor Class
[CustomEditor(typeof(TestUi)), CanEditMultipleObjects]
public class TestUiEditor : Editor
{
    public void OnEnable()
    {
        TestUi owner = (TestUi)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}