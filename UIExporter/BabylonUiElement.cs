using UnityEditor;
using UnityEngine;

namespace PROJECT
{
    public class BabylonUiElement : Editor
    {
        private const string TestFolderPath = "Assets/UIExporter/Test";
        private const string UIFolderPath = "Assets/UIExporter/Prefabs";

        #region Test

        [MenuItem("GameObject/BabylonUi/Test/Rectangle", false, -1)]
        public static void RectangleTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Rectangle.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Text", false, -1)]
        public static void TextTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Text.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/InputText", false, -1)]
        public static void InputTextTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/InputText.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Image", false, -1)]
        public static void ImageTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Image.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Anchor stretch", false, -1)]
        public static void AnchorStretchTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Anchor stretch.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Anchor dock (0)", false, -1)]
        public static void AnchorAnchorDockOne(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Anchor dock (0).prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Anchor dock (1)", false, -1)]
        public static void AnchorAnchorDockTwo(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Anchor dock (1).prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Test/Container", false, -1)]
        public static void ContainerTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Container.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }
        
        [MenuItem("GameObject/BabylonUi/Test/Button", false, -1)]
        public static void ButtonTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Button.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }
        
        [MenuItem("GameObject/BabylonUi/Test/Button 1", false, -1)]
        public static void ButtonAlternativeTest(MenuCommand menuCommand)
        {
            var path = $"{TestFolderPath}/Button 1.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{TestFolderPath}/", "").Replace(".prefab", "");
        }

        #endregion

        #region Ui

        [MenuItem("GameObject/BabylonUi/Container", false, -1)]
        public static void Container(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonContainer.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Text", false, -1)]
        public static void Text(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonText.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Image", false, -1)]
        public static void Image(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonImage.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Button", false, -1)]
        public static void Button(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonButton.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }

        [MenuItem("GameObject/BabylonUi/Canvas", false, -1)]
        public static void Canvas(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonCanvas.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject == null) return;
            gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }
        
        [MenuItem("GameObject/BabylonUi/Rectangle", false, -1)]
        public static void Rectangle(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonRectangle.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }
        
        [MenuItem("GameObject/BabylonUi/InputText", false, -1)]
        public static void InputText(MenuCommand menuCommand)
        {
            var path = $"{UIFolderPath}/BabylonInputText.prefab";
            var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)),
                (menuCommand.context as GameObject)?.transform);
            var gameObject = obj as GameObject;
            if (gameObject != null) gameObject.name = path.Replace($"{UIFolderPath}/", "").Replace(".prefab", "");
        }

        #endregion
    }
}