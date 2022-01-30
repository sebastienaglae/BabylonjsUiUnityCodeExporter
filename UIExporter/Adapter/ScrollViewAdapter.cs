using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PROJECT
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewAdapter : MonoBehaviour, IAdapter
    {
        public void UpdateUI()
        {
            
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
