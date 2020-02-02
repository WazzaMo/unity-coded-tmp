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
        public Vector2 PosMaxOrSize;
        public Vector3 Scale;
        public bool IsValid { get; private set; }
        public bool IsSizeFixed { get; private set; }

        public UiPositionSettings(RectTransform _RectTransform)
        {
            AnchorMin = SafeGet(_RectTransform, _RectTransform.anchorMin);
            AnchorMax = SafeGet(_RectTransform, _RectTransform.anchorMax);
            Pivot = SafeGet(_RectTransform, _RectTransform.pivot);
            (IsSizeFixed, Position, PosMaxOrSize)
                = GetPositionOrBoundaryOffsets(_RectTransform);
            Scale = SafeGet(_RectTransform, _RectTransform.localScale);
            IsValid = (_RectTransform != null);
        }

        public UiPositionSettings(UiPositionSettings other)
        {
            AnchorMin = other.AnchorMin;
            AnchorMax = other.AnchorMax;
            Pivot = other.Pivot;
            Position = other.Position;
            PosMaxOrSize = other.PosMaxOrSize;
            Scale = other.Scale;
            IsSizeFixed = other.IsSizeFixed;
            IsValid = other.IsValid;
        }

        public Vector2 WidthAndHeight {
            get =>new Vector2(PosMaxOrSize.x - Position.x, PosMaxOrSize.y - Position.y);
        }

        private static bool IsFixedSize(
            Vector2 anchorMin,
            Vector2 anchorMax
        )   => anchorMin.x == anchorMax.x
            || anchorMin.y == anchorMax.y;

        private static T SafeGet<T>(RectTransform rt, T value)
            => (rt == null) ? default(T) : value;

        private static (bool, Vector2, Vector2) GetPositionOrBoundaryOffsets(
            RectTransform rt
        )
        {
            if (rt == null) return (
                false, default(Vector2), default(Vector2)
            );

            bool isSizeFixed = IsFixedSize(rt.anchorMin, rt.anchorMax);
            Vector2 position, sizeOrOffset;
            (position, sizeOrOffset) = isSizeFixed
                ? GetBySize(rt)
                : GetPosByBoundary(rt);
            return (isSizeFixed, position, sizeOrOffset);
        }

        private static (Vector2,Vector2) GetPosByBoundary(RectTransform rt)
            => (rt.offsetMin, rt.offsetMax);

        private static (Vector2, Vector2) GetBySize(RectTransform rt)
            => (
                rt.anchoredPosition,
                new Vector2(rt.rect.width, rt.rect.height)
            );
    }
}
