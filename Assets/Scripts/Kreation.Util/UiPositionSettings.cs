/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;

namespace Kreation.Util
{
    /// <summary>
    /// Holds the most important values from a RectTransform
    /// for controlling position, scale and anchoring of UI
    /// components within parent panels.
    /// </summary>
    public struct UiPositionSettings
    {
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
        public Vector2 Pivot;
        public Vector2 Position;
        public Vector3 Scale;
        public bool IsValid { get; private set; }

        public UiPositionSettings(RectTransform _RectTransform)
        {
            AnchorMin = SafeGet(_RectTransform, _RectTransform.anchorMin);
            AnchorMax = SafeGet(_RectTransform, _RectTransform.anchorMax);
            Pivot = SafeGet(_RectTransform, _RectTransform.pivot);
            Position = SafeGet(_RectTransform, _RectTransform.anchoredPosition);
            Scale = SafeGet(_RectTransform, _RectTransform.localScale);
            IsValid = (_RectTransform != null);
        }

        private static T SafeGet<T>(RectTransform rt, T value)
            => (rt == null) ? default(T) : value;
    }
}
