using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PROJECT
{
    public class ContainerAdapter : MonoBehaviour, IAdapter
    {
        [ReadOnly]
        public IAdapter parent;
        
        public void UpdateUI()
        {
            parent = CanvasExporterUtils.GetParent(gameObject);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
