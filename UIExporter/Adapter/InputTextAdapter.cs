using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	[RequireComponent(typeof(TMP_InputField))]
	[RequireComponent(typeof(Image))]
	public class InputTextAdapter : MonoBehaviour, IAdapter
	{
		[Header("Font Settings")]
		public string fontFamily = "arial";
		[Header("Border Settings")]
		public Color borderColorFocused = new Color(1, 1, 1, 1);
		public float borderThickness;
		[Header("Shadows Settings")]
		public Color shadowColor = new Color(1, 1, 1, 1);
		public Vector2 shadowOffset;
		public float shadowBlur;
		[Header("Cursor Settings")]
		public CanvasExporterUtils.CursorType cursor;
		[Header("Input Text Settings")]
		public bool autoStretchWidth;
		public string maxWidth = "0px";

		public void UpdateUI()
		{
			var inputField = GetComponent<TMP_InputField>();
			var text = inputField.textComponent as TextMeshProUGUI;
			var placeholderText = inputField.placeholder as TextMeshProUGUI;
			if (placeholderText != null && text != null)
				if (string.IsNullOrWhiteSpace(placeholderText.text))
				{
					placeholderText.fontSize = text.fontSize;
					placeholderText.fontStyle = text.fontStyle;
					placeholderText.enableAutoSizing = text.enableAutoSizing;
				}
				else
				{
					text.fontSize = placeholderText.fontSize;
					text.fontStyle = placeholderText.fontStyle;
					text.enableAutoSizing = placeholderText.enableAutoSizing;
				}

			var frontObj = CreateFront(gameObject, borderThickness <= 0);
			if (frontObj != null)
			{
				var frontRectTransform = frontObj.GetComponent<RectTransform>();
				var mainRectTransform = GetComponent<RectTransform>();

				if (frontObj.GetComponent<Image>() == null)
					frontObj.AddComponent<Image>();
				frontObj.GetComponent<Image>().color = text.color;
				var rect = frontRectTransform.rect;
				mainRectTransform.sizeDelta = new Vector2(rect.width - borderThickness * 2, rect.height - borderThickness * 2);
			}
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		public static GameObject CreateFront(GameObject gameObject, bool cond)
		{
			// Get the parent gameObject
			var frontObj = gameObject.transform.parent.gameObject;
			if (cond)
			{
				if (!frontObj.CompareTag("DontRender")) return null;
				gameObject.transform.SetParent(frontObj.transform.parent);
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(frontObj.GetComponent<RectTransform>().rect.width, frontObj.GetComponent<RectTransform>().rect.height);
				DestroyImmediate(frontObj);
				return null;
			}

			var wasCreated = false;
			if (!frontObj.CompareTag("DontRender"))
			{
				frontObj = new GameObject("_obj");
				frontObj.tag = "DontRender";
				wasCreated = true;
				frontObj.AddComponent<RectTransform>();
			}

			frontObj.GetComponent<RectTransform>().position = gameObject.transform.GetComponent<RectTransform>().position;
			frontObj.transform.SetParent(gameObject.transform.parent);
			gameObject.transform.SetParent(frontObj.transform);
			if (wasCreated)
				frontObj.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width, gameObject.GetComponent<RectTransform>().rect.height);

			return frontObj;
		}
	}
}
