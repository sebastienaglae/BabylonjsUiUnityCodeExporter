using UnityEngine;
using static PROJECT.CanvasExporterUtils;

namespace PROJECT
{
    public interface IBabylonUI
    {
        UiProperties Generate();
        Control GetControl();
        GameObject GetGameObject();
    }
}