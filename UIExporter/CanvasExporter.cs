using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static PROJECT.CanvasExporterUtils;

namespace PROJECT
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasExporter : MonoBehaviour
    {
        private const string UiFolderPath = "Assets/Content/Custom/Ui";

        #region Generation

        public void GenerateClassCode(bool createFile)
        {
            var n = "";
            GenerateIdealSize(uiName, canvas.gameObject, ref n);
            var uiClassCreator = new UiClassCreator(GenerateCreateUi(uiName), uiName, n);

            foreach (var t in _babylonUiElements)
            {
                uiClassCreator.Add(t.Generate());
            }

            if (createFile)
            {
                var fileName = uiClassCreator.GetClassName() + ".ts";
                CreateFile(UiFolderPath + "/" + fileName, uiClassCreator.ToString());
                AssetDatabase.Refresh();
            }
            else
            {
                GUIUtility.systemCopyBuffer = uiClassCreator.ToString();
            }
        }

        #endregion

        #region Variable

        public string uiName = "advancedTexture";

        [NonReorderable] [ReadOnly] public List<GameObject> sceneUiGameObjects;
        [NonReorderable] [ReadOnly] public List<Control> sceneControlsTypeDetected;
        [ReadOnly] public Canvas canvas;

        private List<IBabylonUI> _babylonUiElements;

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
            _babylonUiElements ??= new List<IBabylonUI>();
            sceneUiGameObjects ??= new List<GameObject>();
            sceneControlsTypeDetected ??= new List<Control>();
            sceneUiGameObjects.Clear();
            _babylonUiElements.Clear();
            sceneControlsTypeDetected.Clear();

            IBabylonParser[] babylonParsers =
            {
                new BabylonContainerParser(),
                new BabylonRectangleParser(),
                new BabylonButtonParser(),
                new BabylonImageParser(),
                new BabylonTextParser(),
                new BabylonInputTextParser(),
                new BabylonScrollViewerParser()
            };
            var zIndex = CreateHierarchicalStructure(canvas);
            var existingIds = new List<string>();
            var controlAdapters = new List<ControlAdapter>(canvas.GetComponentsInChildren<ControlAdapter>(true));
            controlAdapters.Sort();
            foreach (var uiGameObject in controlAdapters.Select(sceneUi => sceneUi.gameObject))
            {
                foreach (var babylonParser in babylonParsers)
                {
                    if (!babylonParser.CanParse(uiGameObject)) continue;
                    var uniqueID = GetUniqueNonNumberId();
                    var actualID = uiGameObject.GetComponent<ControlAdapter>().uniqueID;
                    if (existingIds.Contains(actualID))
                    {
                        GameObject[] objs = {uiGameObject};
                        Selection.objects = objs;
                        Debug.LogError("There multiple unique id !");
                        return false;
                    }

                    if (isEmptyNullWhiteSpace(actualID))
                        uiGameObject.GetComponent<ControlAdapter>().uniqueID = uniqueID;
                    else
                        uniqueID = uiGameObject.GetComponent<ControlAdapter>().uniqueID;
                    existingIds.Add(uniqueID);
                    var babylonUI = babylonParser.Parse(uiName, uiGameObject, uniqueID, zIndex[uiGameObject], canvas);
                    sceneControlsTypeDetected.Add(babylonUI.GetControl());
                    sceneUiGameObjects.Add(babylonUI.GetGameObject());
                    _babylonUiElements.Add(babylonUI);
                    break;
                }
            }

            return true;
        }

        #endregion
    }
}