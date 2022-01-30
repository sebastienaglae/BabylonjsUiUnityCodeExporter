using TMPro;
using UnityEngine;

namespace PROJECT
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextAdapter : MonoBehaviour, IAdapter
	{
		[Header("Font Settings")]
		public string fontFamily = "arial";
		[Header("Cursor Settings")]
		public CanvasExporterUtils.CursorType cursor;

		public void UpdateUI()
		{
			Debug.Log("Update not needed");
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}
	}
}
