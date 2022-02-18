using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	[RequireComponent(typeof(TMP_InputField))]
	[RequireComponent(typeof(Image))]
	public class InputTextAdapter : ControlAdapter, IAdapter
	{
		/*
		 * [Tooltip("")]
		disableMobilePrompt
		promptMessage
		addKey
		autoStretchWidth
		background
		focusedBackground
		focusedColor
		highligherOpacity
		highlightedText
		margin
		maxWidth
		placeholderColor
		placeholderText
		text
		textHighlightColor
		thickness
		focus
		 */


		[Header("Font Settings")]
		public string fontFamily = "arial";
		[Header("Border Settings")]
		public Color borderColorFocused = new Color(1, 1, 1, 1);
		public float borderThickness;
		[Header("Shadows Settings")]
		public Color shadowColor = new Color(1, 1, 1, 1);
		public Vector2 shadowOffset;
		public float shadowBlur;
		[Header("Input Text Settings")]
		public bool autoStretchWidth;
		public string maxWidth = "0px";

		public new void UpdateUI()
		{
			base.UpdateUI();
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
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}
	}
}
