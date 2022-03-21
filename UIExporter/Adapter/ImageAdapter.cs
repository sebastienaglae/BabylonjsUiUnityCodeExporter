using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PROJECT
{
    [RequireComponent(typeof(Image))]
    public class ImageAdapter : ControlAdapter, IAdapter
    {
        [ReadOnly] public bool preserveAspectUrl;
        [ReadOnly] public float imageHeight;
        [ReadOnly] public float imageWidth;
        [ReadOnly] public float sliceBottom;
        [ReadOnly] public float sliceLeft;
        [ReadOnly] public float sliceRight;
        [ReadOnly] public float sliceTop;
        [ReadOnly] public float sourceHeight;
        [ReadOnly] public float sourceLeft;
        [ReadOnly] public float sourceTop;
        [ReadOnly] public float sourceWidth;

        [Header("Image settings")]
        [Tooltip(
            "Gets or sets a boolean indicating if pointers should only be validated on pixels with alpha > 0. Beware using this as this will comsume more memory as the image has to be stored twice")]
        public bool detectPointerOnOpaqueOnly;

        [Tooltip(
            "Gets or sets a boolean indicating if nine patch slices (left, top, right, bottom) should be read from image data")]
        public bool populateNinePatchSlicesFromImage;

        [Tooltip("Gets the image source url")] public string sourceUrl;
        [ReadOnly] protected float alpha;
        [ReadOnly] protected Color color;

        public new void UpdateUI()
        {
            base.UpdateUI();
            var imageComponent = GetComponent<Image>();

            if (imageComponent.sprite == null)
            {
                if (!CanvasExporterUtils.isEmptyNullWhiteSpace(sourceUrl))
                    StartCoroutine(GetTextureRequest(sourceUrl));
            }
            else
            {
                if (AssetDatabase.GetAssetPath(imageComponent.sprite.texture) == null)
                    StartCoroutine(GetTextureRequest(sourceUrl));
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private IEnumerator GetTextureRequest(string url)
        {
            var imageComponent = GetComponent<Image>();
            using var www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (!www.isDone) yield break;
                var texture = DownloadHandlerTexture.GetContent(www);
                imageComponent.sprite = Sprite.Create(texture,
                    new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), new Vector2(0.5f, 0.5f));
            }
        }
    }
}