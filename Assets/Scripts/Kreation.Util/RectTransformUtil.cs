/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using System;

using UnityEngine;


namespace Kreation.Util
{

    /// <summary>
    /// Utility methods to make working with RectTransforms
    /// easier and more reliable for variation in screen resolution
    /// and layout. Useful for TextMesh Pro but also for UI in general.
    /// </summary>
    public static class RectTransformUtil
    {
        public static readonly Vector3 _100PC = new Vector3(1f, 1f, 1f);

        public static readonly Vector2
            AnchorPosTopLeft = new Vector2(1f, -1f),
            AnchorPosBottomLeft = new Vector2(1f, 1f),
            AnchorPosTopRight = new Vector2(-1f, -1f),
            AnchorPosBottomRight = new Vector2(-1, 1f);

        public static readonly Vector2
            PivotTopLeft = new Vector2(0, 1f),
            PivotTopRight = new Vector2(1, 1f),
            PivotBottomLeft = Vector2.zero,
            PivotBottomRight = new Vector2(1f, 0);

        public static void SetUiPosition(
            this RectTransform _RectTransform,
            Vector2 anchorMin, Vector2 anchorMax,
            Vector2 pivot, Vector2 position,
            Vector3 scale
        )
        {
            if (_RectTransform != null)
            {
                _RectTransform.anchorMin = anchorMin;
                _RectTransform.anchorMax = anchorMax;
                _RectTransform.pivot = pivot;
                _RectTransform.anchoredPosition = position;
                _RectTransform.localScale = scale;
            }
        }

        public static UiPositionSettings GetUiPosition(
            this RectTransform _RectTransform
        )
        {
            return new UiPositionSettings(_RectTransform);
        }

        public static void SetUiPosition(
            this RectTransform _RectTransform,
            UiPositionSettings positionSettings
        )
        {
            if (_RectTransform != null && positionSettings.IsValid)
            {
                _RectTransform.anchorMin = positionSettings.AnchorMin;
                _RectTransform.anchorMax = positionSettings.AnchorMax;
                _RectTransform.pivot = positionSettings.Pivot;
                _RectTransform.anchoredPosition = positionSettings.Position;
                _RectTransform.localScale = positionSettings.Scale;
            }
            else
            {
                if (_RectTransform == null)
                {
                    Debug.LogWarning("NULL RectTransform - is component on a UI GameObject?");
                }
                if (! positionSettings.IsValid)
                {
                    Debug.LogWarning("Position of UI element not captured because GetUiPosition() was called with invalid RectTransform.");
                }
            }
        }
    }

}
