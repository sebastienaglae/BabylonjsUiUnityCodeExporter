using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(RectTransform))]
	public class RectangleAdapter : MonoBehaviour, IAdapter
	{
		[Header("Border Settings")]
		public Color borderColor = new Color(1, 1, 1, 1);
		public float borderRadius;
		public float borderThickness;
		[Header("Shadows Settings")]
		public Color shadowColor = new Color(1, 1, 1, 1);
		public Vector2 shadowOffset;
		public float shadowBlur;
		[Header("Cursor Settings")]
		public CanvasExporterUtils.CursorType cursor;
		public CanvasExporterUtils.AlignmentHorizontal h;
		public CanvasExporterUtils.AlignmentVertical v;
		
		[ReadOnly]
		public GameObject parent;

		public void UpdateUI()
		{
			var g = CanvasExporterUtils.GetAlignment(gameObject.GetComponent<RectTransform>());
			h = g.horizontal;
			v = g.vertical;
			parent = CanvasExporterUtils.GetParent(gameObject)?.GetGameObject();
			var frontObj = CreateFront(gameObject, borderThickness <= 0);
			if (frontObj != null)
			{
				var frontRectTransform = frontObj.GetComponent<RectTransform>();
				var mainRectTransform = GetComponent<RectTransform>();

				if (frontObj.GetComponent<Image>() == null)
					frontObj.AddComponent<Image>();
				frontObj.GetComponent<Image>().color = borderColor;
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

		public static GameObject CreateBack(GameObject gameObject, bool cond)
		{
			// Get the parent gameObject
			var backObj = GetChildDontRender(gameObject);
			if (cond)
			{
				if (backObj == null) return null;
				if (!backObj.CompareTag("DontRender")) return null;
				DestroyImmediate(backObj);
				return null;
			}

			var wasCreated = false;
			if (backObj == null)
			{
				backObj = new GameObject("_obj");
				backObj.tag = "DontRender";
				backObj.AddComponent<RectTransform>();
				wasCreated = true;
			}
			if (!backObj.CompareTag("DontRender"))
			{
				backObj = new GameObject("_obj");
				backObj.tag = "DontRender";
				backObj.AddComponent<RectTransform>();
				wasCreated = true;
			}

			backObj.transform.SetParent(gameObject.transform);
			if (wasCreated)
				backObj.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width, gameObject.GetComponent<RectTransform>().rect.height);

			return backObj;
		}

		private static GameObject GetChildDontRender(GameObject gameObject)
		{
			for (var i = 0; i < gameObject.transform.childCount; i++)
				if (gameObject.transform.GetChild(i).CompareTag("DontRender"))
					return gameObject.transform.GetChild(i).gameObject;

			return null;
		}
	}
}
