using UnityEngine;

namespace PROJECT
{
	public abstract class BabylonControl
	{
		protected readonly Canvas canvas;
		protected readonly GameObject gameObject;
		protected readonly string uiName;
		protected readonly string varName;

		protected BabylonControl(string uiName, GameObject gameObject, string varName, Canvas canvas)
		{
			this.uiName = uiName;
			this.gameObject = gameObject;
			this.varName = varName;
			this.canvas = canvas;
		}

		protected void GenerateDefault(ref string n)
		{
			//CanvasExporterUtils.GenerateAlpha(varName, image.color, ref n);
			GenerateIsVisible(ref n);
			GeneratePlacement(ref n);
			GenerateRotation(ref n);
		}

		private void GenerateIsVisible(ref string n)
		{
			BabylonUtils.CreateCodeProperty(varName, "isVisible", gameObject.activeInHierarchy, ref n);
		}

		private void GenerateRotation(ref string n)
		{
			var val = gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z * Mathf.Deg2Rad;

			BabylonUtils.CreateCodeProperty(varName, "rotation", -val, ref n);
		}

		private void GeneratePlacement(ref string n)
		{
			var (horizontal, vertical) = CanvasExporterUtils.GetAlignment(gameObject.GetComponent<RectTransform>());
			GenerateSize(varName, gameObject, canvas, horizontal, vertical, ref n);
			GeneratePosition(varName, gameObject, canvas, horizontal, vertical, ref n);
		}

		private static void GenerateSize(string varName, GameObject u, Canvas canvas, CanvasExporterUtils.AlignmentHorizontal horizontal, CanvasExporterUtils.AlignmentVertical vertical, ref string n)
		{
			float valw;
			float valh;
			var target = u.GetComponent<RectTransform>();
			if (u.transform.parent.CompareTag("DontRender"))
				target = u.transform.parent.GetComponent<RectTransform>();

			if (horizontal == CanvasExporterUtils.AlignmentHorizontal.STRETCH)
			{
				valw = target.rect.width / canvas.pixelRect.size.x * 100f;
				BabylonUtils.CreateCodeProperty(varName, "width", valw, BabylonUtils.Unit.PERCENT, ref n);
			}
			else
			{
				valw = target.rect.width;
				BabylonUtils.CreateCodeProperty(varName, "width", valw, BabylonUtils.Unit.PIXEL, ref n);
			}

			if (vertical == CanvasExporterUtils.AlignmentVertical.STRETCH)
			{
				valh = target.rect.height / canvas.pixelRect.size.y * 100f;
				BabylonUtils.CreateCodeProperty(varName, "height", valh, BabylonUtils.Unit.PERCENT, ref n);
			}
			else
			{
				valh = target.rect.height;
				BabylonUtils.CreateCodeProperty(varName, "height", valh, BabylonUtils.Unit.PIXEL, ref n);
			}
		}

		private static void GeneratePosition(string varName, GameObject u, Canvas canvas, CanvasExporterUtils.AlignmentHorizontal horizontal, CanvasExporterUtils.AlignmentVertical vertical, ref string n)
		{
			Vector2 swapVector;
			var target = u.GetComponent<RectTransform>();
			if (u.transform.parent.CompareTag("DontRender"))
				target = u.transform.parent.GetComponent<RectTransform>();


			Debug.Log(u.name + " : " + target.position);
			Debug.Log(u.name + " : " + target.pivot);
			Debug.Log(u.name + " : " + target.offsetMin);
			Debug.Log(u.name + " : " + target.offsetMax);
			Debug.Log(u.name + " : " + target);
			Debug.Log(u.name + " : " + target.localPosition);
			Debug.Log(u.name + " : " + target.rect.left);
			Debug.Log(u.name + " : " + target.rect.right);
			Debug.Log(u.name + " : " + target.rect.top);
			Debug.Log(u.name + " : " + target.rect.bottom);
			Debug.Log(u.name + " : " + target.rect);
			Debug.Log(u.name + " : " + target.anchoredPosition);
			Debug.Log(u.name + " : " + target.rect.center);
			Debug.Log(u.name + " : " + target.rect.position);
			Debug.Log(u.name + " : " + target.rect.y);
			Debug.Log(u.name + " : " + target.rect.x);
			Debug.Log(u.name + " : " + target.transform.position);
			swapVector = CanvasExporterUtils.SwapSignPosition(target.anchoredPosition);

			var valx = swapVector.x / canvas.pixelRect.size.x * 100f;
			var valy = swapVector.y / canvas.pixelRect.size.y * 100f;

			if (vertical == CanvasExporterUtils.AlignmentVertical.CENTER)
			{
				BabylonUtils.CreateCodeProperty(varName, "top", swapVector.y, BabylonUtils.Unit.PIXEL, ref n);
				BabylonUtils.CreateCodeProperty(varName, "verticalAlignment", $"BABYLON.GUI.Control.VERTICAL_ALIGNMENT_{vertical}", false, ref n);
			}
			else if (vertical != CanvasExporterUtils.AlignmentVertical.STRETCH)
			{
				float cal = swapVector.y >= 0 ? swapVector.y - (target.rect.height / 2) : swapVector.y + (target.rect.height / 2);
				BabylonUtils.CreateCodeProperty(varName, "top", cal, BabylonUtils.Unit.PIXEL, ref n);
				BabylonUtils.CreateCodeProperty(varName, "verticalAlignment", $"BABYLON.GUI.Control.VERTICAL_ALIGNMENT_{vertical}", false, ref n);
			}
			else
			{
				BabylonUtils.CreateCodeProperty(varName, "top", valy, BabylonUtils.Unit.PERCENT, ref n);
				BabylonUtils.CreateCodeProperty(varName, "verticalAlignment", $"BABYLON.GUI.Control.{CanvasExporterUtils.BabylonVerticalAlignment.VERTICAL_ALIGNMENT_CENTER}", false, ref n);
			}

			if (horizontal == CanvasExporterUtils.AlignmentHorizontal.CENTER)
			{
				BabylonUtils.CreateCodeProperty(varName, "left", swapVector.x, BabylonUtils.Unit.PIXEL, ref n);
				BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment", $"BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_{horizontal}", false, ref n);
			}
			else if (horizontal != CanvasExporterUtils.AlignmentHorizontal.STRETCH) // LEFT & RIGHT
			{
				float cal = swapVector.x >= 0 ? swapVector.x - (target.rect.width / 2) : swapVector.x + (target.rect.width / 2);
				BabylonUtils.CreateCodeProperty(varName, "left", cal, BabylonUtils.Unit.PIXEL, ref n);
				BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment", $"BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_{horizontal}", false, ref n);
			}
			else
			{
				BabylonUtils.CreateCodeProperty(varName, "left", valx, BabylonUtils.Unit.PERCENT, ref n);
				BabylonUtils.CreateCodeProperty(varName, "horizontalAlignment", $"BABYLON.GUI.Control.{CanvasExporterUtils.BabylonHorizontalAlignment.HORIZONTAL_ALIGNMENT_CENTER}", false, ref n);
			}
		}
	}
}
