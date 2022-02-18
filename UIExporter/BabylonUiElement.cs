using UnityEditor;
using UnityEngine;

namespace PROJECT
{
	
public class BabylonUiElement : Editor
{
	private static readonly string testFolderPath = "Assets/UIExporter/Test";
	private static readonly string uiFolderPath = "Assets/UIExporter/Prefabs";

	#region Test

	[MenuItem("GameObject/BabylonUi/Test/Rectangle", false, -1)]
	public static void RectangleTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Rectangle.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Text", false, -1)]
	public static void TextTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Text.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/InputText", false, -1)]
	public static void InputTextTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/InputText.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Image", false, -1)]
	public static void ImageTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Image.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Anchor stretch", false, -1)]
	public static void AnchorStretchTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Anchor stretch.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Anchor dock (0)", false, -1)]
	public static void AnchorAnchorDockOne(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Anchor dock (0).prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Anchor dock (1)", false, -1)]
	public static void AnchorAnchorDockTwo(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Anchor dock (1).prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Test/Container", false, -1)]
	public static void ContainerTest(MenuCommand menuCommand)
	{
		var path = $"{testFolderPath}/Container.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{testFolderPath}/", "").Replace(".prefab", "");
	}

	#endregion

	#region Ui

	[MenuItem("GameObject/BabylonUi/Container", false, -1)]
	public static void Container(MenuCommand menuCommand)
	{
		var path = $"{uiFolderPath}/BabylonContainer.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{uiFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Text", false, -1)]
	public static void Text(MenuCommand menuCommand)
	{
		var path = $"{uiFolderPath}/BabylonText.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{uiFolderPath}/", "").Replace(".prefab", "");
	}

	[MenuItem("GameObject/BabylonUi/Image", false, -1)]
	public static void Image(MenuCommand menuCommand)
	{
		var path = $"{uiFolderPath}/BabylonImage.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{uiFolderPath}/", "").Replace(".prefab", "");
	}
	
	[MenuItem("GameObject/BabylonUi/Button", false, -1)]
	public static void Button(MenuCommand menuCommand)
	{
		var path = $"{uiFolderPath}/BabylonButton.prefab";
		var obj = Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), (menuCommand.context as GameObject)?.transform);
		var gameObject = obj as GameObject;
		if (gameObject != null) gameObject.name = path.Replace($"{uiFolderPath}/", "").Replace(".prefab", "");
	}

	#endregion
}

}