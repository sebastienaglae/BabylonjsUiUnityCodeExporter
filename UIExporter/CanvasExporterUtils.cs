using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PROJECT
{
    public static class CanvasExporterUtils
    {
        public enum AlignmentHorizontal
        {
            STRETCH = 1000,
            LEFT = 0,
            CENTER = 50,
            RIGHT = 100
        }

        public enum AlignmentVertical
        {
            STRETCH = 1000,
            BOTTOM = 0,
            CENTER = 50,
            TOP = 100
        }

        public enum BabylonHorizontalAlignment
        {
            HORIZONTAL_ALIGNMENT_CENTER,
            HORIZONTAL_ALIGNMENT_LEFT,
            HORIZONTAL_ALIGNMENT_RIGHT
        }

        public enum BabylonVerticalAlignment
        {
            VERTICAL_ALIGNMENT_CENTER,
            VERTICAL_ALIGNMENT_BOTTOM,
            VERTICAL_ALIGNMENT_TOP
        }

        public enum Control
        {
            NONE,
            CONTAINER,
            RECTANGLE,
            BUTTON,
            SELECTION_PANEL,
            SCROLLVIEWER,
            TOGGLE_BUTTON,
            STACK_PANEL,
            VIRTUAL_KEYBOARD,
            GRID,
            ELLIPSE,
            TEXT_BLOCK,
            IMAGE,
            CHECKBOX,
            INPUT_TEXT,
            COLOR_PICKER,
            LINE,
            MULTILINE,
            RADIO_BUTTON,
            BASE_SLIDER,
            SLIDER,
            SCROLLBAR,
            IMAGE_SCROLLBAR,
            IMAGE_BASED_SLIDER,
            DISPLAY_GRID
        }

        public static bool isEmptyNullWhiteSpace(string value)
        {
            return value.Contains("\u200B") || string.IsNullOrWhiteSpace(value);
        }

        public static string ConvertEmptyNullWhiteSpace(string value)
        {
            while (value.Contains("\u200B"))
                value = value.Replace("\u200B", "");
            return value;
        }

        public static (string, string) GenerateCreateUi(string uiName)
        {
            return ("BABYLON.GUI.AdvancedDynamicTexture",
                $"BABYLON.GUI.AdvancedDynamicTexture.CreateFullscreenUI(\"{uiName}\");\n");
        }

        public static void GenerateIdealSize(string uiName, GameObject u, ref string n)
        {
            var width = (int) u.GetComponent<RectTransform>().rect.width;
            n += $"{uiName}.idealWidth = {width};\n";
        }

        public static IAdapter GetParent(GameObject gameObject)
        {
            var parent = gameObject.transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<IAdapter>() != null)
                    return parent.GetComponent<IAdapter>();
                parent = parent.parent;
            }

            return null;
        }

        public static string Convert(float val)
        {
            var result = val.ToString("#.##").Replace(",", ".");
            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        public static IEnumerable<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }

        public static Vector2 SwapSignPosition(Vector2 vec)
        {
            return new Vector2(vec.x, -vec.y);
        }

        public static string FixHex(string hex)
        {
            for (var i = hex.Length; i < 2; i++)
                hex = "0" + hex;

            return hex;
        }

        public static string ColorConvert(Color color)
        {
            return
                $"{FixHex(((int) (color.r * 255)).ToString("X"))}{FixHex(((int) (color.g * 255)).ToString("X"))}{FixHex(((int) (color.b * 255)).ToString("X"))}";
        }

        public static string ColorConvert(Color32 color)
        {
            return $"{FixHex(color.r.ToString("X"))}{FixHex(color.g.ToString("X"))}{FixHex(color.b.ToString("X"))}";
        }

        public static string EncodeLine(string str)
        {
            return str.Replace("\n", "\\n");
        }

        public static int GetHierarchicalLevel(GameObject gameObject)
        {
            if (gameObject.transform.parent.GetComponent<Canvas>())
                return 0;

            var actualTransform = gameObject.transform.parent;
            var level = 0;
            while (actualTransform.GetComponent<Canvas>() == null)
            {
                actualTransform = actualTransform.parent;
                level++;
            }

            return level;
        }

        public static Dictionary<GameObject, int> CreateHierarchicalStructure(Canvas canvas)
        {
            var dictionary = new Dictionary<GameObject, int>();
            var parents = new List<GameObject>
            {
                canvas.gameObject
            };

            var p = 0;
            var id = 0;
            while (parents.Count != 0)
            {
                var clone = parents.ToArray();
                parents.Clear();
                foreach (var t in clone)
                {
                    for (var j = 0; j < t.transform.childCount; j++)
                    {
                        var child = t.transform.GetChild(j);
                        dictionary.Add(child.gameObject, id);
                        parents.Add(child.gameObject);
                        id++;
                    }
                }

                p++;
            }

            return dictionary;
        }

        public static string GetUniqueNonNumberId()
        {
            const string dic = "ABCDEFGHIJ";
            var uuid = Guid.NewGuid().ToString();
            var newUuid = "";
            if (newUuid == null) throw new ArgumentNullException(nameof(newUuid));
            foreach (var t in uuid)
            {
                var isNumeric = int.TryParse(t.ToString(), out var result);
                if (isNumeric)
                    newUuid += dic[result];
                else
                    newUuid += t;
            }

            while (newUuid.Contains("-"))
                newUuid = newUuid.Replace("-", "");

            return newUuid;
        }

        public static (AlignmentHorizontal horizontal, AlignmentVertical vertical) GetAlignment(
            RectTransform rectTransform)
        {
            AlignmentHorizontal horizontal;
            AlignmentVertical vertical;
            var anchorMax = rectTransform.anchorMax;
            var anchorMaxX = (int) (anchorMax.x * 100);
            var anchorMaxY = (int) (anchorMax.y * 100);
            if (rectTransform.anchorMin == rectTransform.anchorMax)
            {
                horizontal = (AlignmentHorizontal) anchorMaxX;
                vertical = (AlignmentVertical) anchorMaxY;
                return (horizontal, vertical);
            }

            if (rectTransform.anchorMin == Vector2.zero && rectTransform.anchorMax == Vector2.one)
            {
                horizontal = AlignmentHorizontal.STRETCH;
                vertical = AlignmentVertical.STRETCH;
            }
            else if (Math.Abs(rectTransform.anchorMin.x - rectTransform.anchorMax.x) < Mathf.Epsilon)
            {
                horizontal = (AlignmentHorizontal) anchorMaxX;
                vertical = AlignmentVertical.STRETCH;
            }
            else
            {
                vertical = (AlignmentVertical) anchorMaxY;
                horizontal = AlignmentHorizontal.STRETCH;
            }

            return (horizontal, vertical);
        }

        public static void CreateFile(string path, string content)
        {
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}