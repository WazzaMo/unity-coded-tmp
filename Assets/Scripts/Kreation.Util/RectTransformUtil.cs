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

        public static void FixSizeAndSetPosition(
            this RectTransform _RectTransform,
            UiPositionSettings setting,
            Vector2 anchorMin, Vector2 anchorMax,
            Vector2 pivot, Vector2 position,
            Vector3 scale
        )
        {
            if (_RectTransform != null && setting.IsValid)
            {
                if (setting.IsSizeFixed)
                {
                    SetPositionWhenSizeAlreadyFixed(
                        _RectTransform,
                        anchorMin, anchorMax,
                        pivot, position, scale
                    );
                }
                else
                {
                    SetPositionAndConvertToFixedSize(_RectTransform,
                        anchorMin, anchorMax,
                        pivot, position, scale
                    );
                }
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
                SetAnchorPivotPositionAndScale(_RectTransform, positionSettings);
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

        private static void SetAnchorPivotPositionAndScale(
            RectTransform _RectTransform,
            UiPositionSettings positionSettings
        )
        {
            SetAnchorPivotAndScale(_RectTransform,
                    positionSettings.AnchorMin, positionSettings.AnchorMax,
                    positionSettings.Pivot, positionSettings.Scale
                );

            if (positionSettings.IsSizeFixed)
            {
                SetUiPositionFixedSizeStrategy(_RectTransform, positionSettings);
            }
            else
            {
                SetUiPositionBoundaryBasedStrategy(
                    _RectTransform, positionSettings
                );
            }
        }

        private static void SetAnchorPivotAndScale(
            RectTransform _RectTransform,
            Vector2 anchorMin, Vector2 anchorMax,
            Vector2 pivot, Vector3 scale
        )
        {
            _RectTransform.anchorMin = anchorMin;
            _RectTransform.anchorMax = anchorMax;
            _RectTransform.pivot = pivot;
            _RectTransform.localScale = scale;
        }

        private static void SetPositionWhenSizeAlreadyFixed(
            RectTransform _RectTransform,
            Vector2 anchorMin, Vector2 anchorMax,
            Vector2 pivot, Vector2 position,
            Vector3 scale
        )
        {
            SetAnchorPivotAndScale(_RectTransform,
                anchorMin, anchorMax, pivot, scale
            );
            _RectTransform.anchoredPosition = position;
        }

        private static void SetPositionAndConvertToFixedSize(
            RectTransform _RectTransform,
            Vector2 anchorMin, Vector2 anchorMax,
            Vector2 pivot, Vector2 position,
            Vector3 scale
        )
        {
            Rect rect = _RectTransform.rect;
            SetPositionWhenSizeAlreadyFixed(
                _RectTransform,
                anchorMin, anchorMax,
                pivot, position, scale
            );
            _RectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                rect.width
            );
            _RectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                rect.height
            );
        }

        private static void SetUiPositionFixedSizeStrategy(
            RectTransform _RectTransform,
            UiPositionSettings positionSettings
        )
        {
            _RectTransform.anchoredPosition = positionSettings.Position;
            _RectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                positionSettings.PosMaxOrSize.x
            );
            _RectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                positionSettings.PosMaxOrSize.y
            );
        }

        private static void SetUiPositionBoundaryBasedStrategy(
            RectTransform _RectTransform,
            UiPositionSettings positionSettings
        )
        {
            _RectTransform.offsetMin = positionSettings.Position;
            _RectTransform.offsetMax = positionSettings.PosMaxOrSize;
        }
    }

}
