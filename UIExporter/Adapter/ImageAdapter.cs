using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
	[RequireComponent(typeof(Image))]
	public class ImageAdapter : MonoBehaviour, IAdapter
	{
		//[Header("Settings Take Into Account")]
		[Header("Cursor Settings")]
		public CanvasExporterUtils.CursorType cursor;
		[Header("Image Settings")]
		public string urlImage;
		public bool isSlicedUrl;
		public bool preserveAspectUrl;
		[Header("Shadows Settings")]
		public Color shadowColor = new Color(1, 1, 1, 1);
		public Vector2 shadowOffset;
		public float shadowBlur;
		[ReadOnly]
		public IAdapter parent;

		public void UpdateUI()
		{
			parent = CanvasExporterUtils.GetParent(gameObject);
			var image = GetComponent<Image>();
			
			if (isSlicedUrl && preserveAspectUrl)
				Debug.LogWarning($"isSlicedUrl and preserveAspectUrl cannot be both true (obj : {gameObject.name}, parent : {transform.parent.name})!");
			if (image.sprite == null)
			{
				Debug.LogWarning($"No sprite was detected, using urlImage (obj : {gameObject.name}, parent : {transform.parent.name})!");
				Debug.LogWarning($"isSlicedUrl and preserveAspectUrl are ignored (obj : {gameObject.name}, parent : {transform.parent.name})!");
			}
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}
	}
}
