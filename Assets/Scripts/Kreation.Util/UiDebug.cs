/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;

namespace Kreation.Util
{

    public static class UiDebug
    {
        public static void DumpRectTrans(
            GameObject gameObject
        )
        {
            RectTransform _RectTransform
                = gameObject.GetComponent<RectTransform>();

            Vector2
                anchorMin = _RectTransform.anchorMin,
                anchorMax = _RectTransform.anchorMax,
                anchorPos = _RectTransform.anchoredPosition,
                pivot = _RectTransform.pivot,
                offsetMin = _RectTransform.offsetMin,
                offsetMax = _RectTransform.offsetMax;

            Rect rect = _RectTransform.rect;
            Vector3
                pos3d = _RectTransform.localPosition,
                scale = _RectTransform.localScale;

            Debug.Log(
    $@"RectTransform Details::
Anchor Min - x:{anchorMin.x} y: {anchorMin.y}
Anchor Max - x:{anchorMax.x} y: {anchorMax.y}
Anchor Position - {anchorPos.x}, {anchorPos.y}
Pivot: {pivot.x}, {pivot.y}
Scale: {scale.x}, {scale.y}, {scale.z}

--- Less Useful Parts of RectTransform ---
(calc) Rect - pos: {rect.x}, {rect.y} size: {rect.width}, {rect.height}
3D Pos: {pos3d.x}, {pos3d.y}, {pos3d.z}
Offset {offsetMin.x}, {offsetMin.y} to {offsetMax.x}, {offsetMax.y}
"
            );
        }

        public static T GetComponentOrWarn<T>(
            this MonoBehaviour mono,
            string warning
        ) where T : Component
        {
            var comp = mono.GetComponent<T>();
            if (comp == null)
            {
                Debug.LogWarning(warning);
            }
            return comp;
        }
    }

}
