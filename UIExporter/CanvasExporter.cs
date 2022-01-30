using System.Collections.Generic;
using UnityEngine;
using static PROJECT.CanvasExporterUtils;

namespace PROJECT
{
	[RequireComponent(typeof(Canvas))]
	public class CanvasExporter : MonoBehaviour
	{
		#region Generation

		public void GenerateUi()
		{
			var n = "";
			GenerateCreateUi(uiName, ref n);
			GenerateIdealSize(uiName, canvas.gameObject, ref n);

			foreach (var t in babylonUiElements) t.Generate(ref n);

			GUIUtility.systemCopyBuffer = n;
		}

		#endregion

		#region Variable

		public string uiName = "advancedTexture";

		[NonReorderable]
		[ReadOnly]
		public List<GameObject> sceneUiGameObjects;
		[NonReorderable]
		[ReadOnly]
		public List<Control> sceneControlsTypeDetected;
		[ReadOnly]
		public Canvas canvas;

		private List<BabylonUI> babylonUiElements;

		#endregion

		#region GenerateProperties

		#endregion

		#region Utils

		public bool GetUiElement()
		{
			if (string.IsNullOrWhiteSpace(uiName))
			{
				Debug.LogWarning("The ui name is missing !");
				return false;
			}

			canvas = GetComponent<Canvas>();
			babylonUiElements ??= new List<BabylonUI>();
			sceneUiGameObjects ??= new List<GameObject>();
			sceneUiGameObjects.Clear();
			babylonUiElements.Clear();
			sceneControlsTypeDetected.Clear();

			IBabylonParser[] babylonParsers =
			{
				new BabylonInputTextParser(),
				new BabylonRectangleParser(),
				new BabylonTextParser(),
				new BabylonImageParser(),
				new BabylonScrollViewParser()
			};

			var uiId = 0;
			var adapters = canvas.GetComponentsInChildren<IAdapter>(true);
			if (adapters == null)
				return false;

			foreach (var sceneUi in adapters)
			{
				var uiGameObject = sceneUi.GetGameObject();
				foreach (var babylonParser in babylonParsers)
				{
					if (!babylonParser.CanParse(uiGameObject)) continue;
					var varName = $"element_{uiId}";
					var babylonUI = babylonParser.Parse(uiName, uiGameObject, varName, canvas);
					sceneControlsTypeDetected.Add(babylonUI.GetControl());
					sceneUiGameObjects.Add(babylonUI.GetGameObject());
					babylonUiElements.Add(babylonUI);
					uiId++;
					break;
				}
			}

			return true;
		}
	}

	#endregion
}
