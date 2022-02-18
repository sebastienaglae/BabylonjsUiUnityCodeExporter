using System.Collections.Generic;
using System.Linq;
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
			sceneControlsTypeDetected ??= new List<Control>();
			sceneUiGameObjects.Clear();
			babylonUiElements.Clear();
			sceneControlsTypeDetected.Clear();

			IBabylonParser[] babylonParsers =
			{
				new BabylonContainerParser(),
				new BabylonRectangleParser(),
				new BabylonButtonParser(),
				new BabylonImageParser(),
				new BabylonTextParser()
			};
			var zIndex = CreateHierarchicalStructure(canvas);
			var controlAdapters = new List<ControlAdapter>(canvas.GetComponentsInChildren<ControlAdapter>(true));
			controlAdapters.Sort();
			foreach (var uiGameObject in controlAdapters.Select(sceneUi => sceneUi.gameObject))
			{
				foreach (var babylonParser in babylonParsers)
				{
					if (!babylonParser.CanParse(uiGameObject)) continue;
					var uniqueID = GetUniqueNonNumberId();

					if (isEmptyNullWhiteSpace(uiGameObject.GetComponent<ControlAdapter>().uniqueID))
						uiGameObject.GetComponent<ControlAdapter>().uniqueID = uniqueID;
					else
						uniqueID = uiGameObject.GetComponent<ControlAdapter>().uniqueID;
					var babylonUI = babylonParser.Parse(uiName, uiGameObject, uniqueID, zIndex[uiGameObject], canvas);
					sceneControlsTypeDetected.Add(babylonUI.GetControl());
					sceneUiGameObjects.Add(babylonUI.GetGameObject());
					babylonUiElements.Add(babylonUI);
					break;
				}
			}

			return true;
		}

		#endregion
	}
}
