/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */



namespace Kreation.Util
{
    public static class AnchorCalculations
    {
        public const float FULL_WIDTH = 1f, TOP_ANCHOR = 1f;

        /// <summary>
        ///     Factory for X min and max values given a certain
        ///     margin. Everything is expressed in fractional values
        ///     between 0 to 1.0 and mean a portion of the parent
        ///     object's rectangular area.
        /// </summary>
        /// <param name="margin">fraction for margin</param>
        /// <returns>tuple of x-min and x-max, each 0.0 to 1.0</returns>
        public static (float minX, float maxX) ExpandedXAnchor(
            float margin
        ) => (minX: margin, maxX: FULL_WIDTH - margin);

        /// <summary>
        ///     X anchor value factory for a partly or fully
        ///     collapsed inner object.
        /// </summary>
        /// <param name="alignment">Left or Right alignment</param>
        /// <param name="progress">
        ///     How far collapsed, expressed as a fraction.
        ///     (0 expanded / 1.0 fully collapsed)
        /// </param>
        /// <param name="margin">margin fraction (margin < 1.0f)</param>
        /// <param name="collapsedWidth">
        ///     The portion of the parent horizontal width that divides
        ///     LEFT from RIGHT.
        /// </param>
        /// <returns>Tuple of min-x and max-x float values.</returns>
        public static (float minX, float maxX) XAnchor(
            VerticalLayoutAlignment alignment,
            float progress,
            float margin,
            float collapsedWidth
        ) => (alignment == VerticalLayoutAlignment.Left)
            ? XAnchorLeftAlign(progress, margin, collapsedWidth)
            : XAnchorRightAlign(progress, margin, collapsedWidth);

        // Computes X anchor values when left-aligned.
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

        // Computes X anchor values when right-aligned.
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
