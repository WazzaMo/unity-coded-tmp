/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */



using System;

using UnityEngine;

namespace Kreation.Util
{
    /// <summary>
    /// Utility methods for the AnimatableLayout class.
    /// </summary>
    public static class AnimatableLayoutUtil
    {

        /// <summary>
        /// Calculate a linear tween with a fraction
        /// (relative mix) and a default height.
        /// </summary>
        /// <param name="height1">First height (h1)</param>
        /// <param name="height2">Second height (h2)</param>
        /// <param name="fraction">
        ///     Control on how much each height contributes to the
        ///     total tweened value.
        /// </param>
        /// <param name="isDefaultHeight1">
        ///     When TRUE, a fraction of 0 returns h1 and fration of 1, h2.
        ///     What FALSE, other way around.
        /// </param>
        /// <returns>
        ///     A value between the two heights, dependent on the
        ///     fraction and which height is set to be default.
        /// </returns>
        public static float LinearTweenHeights(
            float height1, float height2,
            float fraction, bool isDefaultHeight1
        )
        => isDefaultHeight1
            ? (fraction * height2) + ((1 - fraction) * height1)
            : ((1 - fraction) * height2) + (fraction * height1);

        /// <summary>
        ///     Returns the height fraction for layout based
        ///     on layout state and tweening fraction to be
        ///     somewhere between the two.
        /// </summary>
        /// <param name="expandedHeight">height when expanded</param>
        /// <param name="collapsedHeight">height when collapsed</param>
        /// <param name="fraction">tweening fraction</param>
        /// <param name="currentLayout">layout state</param>
        /// <returns>float height fraction between 0 and 1</returns>
        public static float GetTweenedFractionByLayout(
            float expandedHeight, float collapsedHeight,
            float fraction,
            LayoutState currentLayout
        )
            => LinearTweenHeights(
                collapsedHeight, expandedHeight,
                fraction, IsCollapsed(currentLayout)
            );

        /// <summary>
        ///     Returns a height fraction for layout based only
        ///     on the layout state. Is to be comparable to the
        ///     tweened version but this one lacks tweening.
        /// </summary>
        /// <param name="expandedHeight">height when expanded</param>
        /// <param name="collapsedHeight">height when collapsed</param>
        /// <param name="fraction">ignored - used in tweening</param>
        /// <param name="currentLayout">indicates collapsed/expanded</param>
        /// <returns>float height fraction between 0 and 1</returns>
        public static float GetFractionByLayout(
            float expandedHeight, float collapsedHeight,
            float fraction,
            LayoutState currentLayout
        )
            =>  IsCollapsed(currentLayout)
                ? expandedHeight : collapsedHeight;

        /// <summary>
        ///     Used to determine if layout state means more
        ///     collapsed than expanded.
        /// </summary>
        /// <param name="currentLayout"></param>
        /// <returns>true if mostly collapsed</returns>
        public static bool IsCollapsed(LayoutState currentLayout)
            =>  currentLayout == LayoutState.Collapsed
                || currentLayout == LayoutState.CollapsedButExpanding;
    }
}
