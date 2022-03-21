using TMPro;
using UnityEngine;

namespace PROJECT
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAdapter : ControlAdapter, IAdapter
    {
        [ReadOnly] public float shadowOffsetX;

        [Header("Font Settings")] public string fontFamily = "arial";
        [ReadOnly] protected float alpha;
        [ReadOnly] protected Color color;
        [ReadOnly] protected float shadowBlur;
        [ReadOnly] protected Color shadowColor = new Color(1, 1, 1, 1);
        [ReadOnly] protected float shadowOffsetY;

        public new void UpdateUI()
        {
            base.UpdateUI();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}