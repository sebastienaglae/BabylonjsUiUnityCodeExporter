using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PROJECT
{
	public class ButtonAdapter : RectangleAdapter, IAdapter
    {
	    [ReadOnly]
	    protected Color color;
	    
	    [Header("Font Settings")]
	    public string fontFamily = "arial";
	    
    	[Header("Image Settings")]
    	[Tooltip("The image of the button")]
    	public string imageUrl;
    	[Tooltip("Is the image of the button is used as an icon")]
    	public bool iconButton;
    
    	public new void UpdateUI()
    	{
    		base.UpdateUI();
    		var imageComponent = GetComponent<Image>();
    
    		if (imageComponent.sprite == null)
    		{
    			if (!CanvasExporterUtils.isEmptyNullWhiteSpace(imageUrl))
    				StartCoroutine(GetTextureRequest(imageUrl));
    		}
    		else
    		{
    			if (AssetDatabase.GetAssetPath(imageComponent.sprite.texture) == null)
    				StartCoroutine(GetTextureRequest(imageUrl));
    		}
    	}
    
    	public new GameObject GetGameObject()
    	{
    		return gameObject;
    	}
    
    	private IEnumerator GetTextureRequest(string url)
    	{
    		var imageComponent = GetComponent<Image>();
    		using (var www = UnityWebRequestTexture.GetTexture(url))
    		{
    			yield return www.SendWebRequest();
    
    			if (www.isNetworkError || www.isHttpError)
    			{
    				Debug.Log(www.error);
    			}
    			else
    			{
    				if (!www.isDone) yield break;
    				var texture = DownloadHandlerTexture.GetContent(www);
    				imageComponent.sprite = Sprite.Create(texture, imageComponent.rectTransform.rect, imageComponent.rectTransform.pivot);
    			}
    		}
    	}
    }

}

