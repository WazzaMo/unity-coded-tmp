/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */



namespace Kreation.Util
{
    public static class AnchorCalculations
    {
        public const float FULL_WIDTH = 1f, TOP_ANCHOR = 1f;

        public static (float minX, float maxX) ExpandedXAnchor(
            float margin
        ) => (minX: margin, maxX: FULL_WIDTH - margin);

        public static (float minX, float maxX) XAnchor(
            VerticalLayoutAlignment alignment,
            float progress,
            float margin,
            float collapsedWidth
        ) => (alignment == VerticalLayoutAlignment.Left)
            ? XAnchorLeftAlign(progress, margin, collapsedWidth)
            : XAnchorRightAlign(progress, margin, collapsedWidth);

        private static (float minX, float maxX) XAnchorLeftAlign(
            float progress,
            float margin,
            float collapsedWidth
        )
        {
            float max
                = (progress * collapsedWidth)
                + ((1 - progress) * FULL_WIDTH);
            return (minX: margin, maxX: max - margin);
        }

        private static (float minX, float maxX) XAnchorRightAlign(
            float progress,
            float margin,
            float collapsedWidth
        )
        {
            float min
                = (progress * collapsedWidth)
                + ((1 - progress) * margin);
            return (minX: min, maxX: FULL_WIDTH - margin);
        }

    }
}
