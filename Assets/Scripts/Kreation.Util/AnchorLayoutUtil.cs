/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */

using UnityEngine;

namespace Kreation.Util
{

    public static class AnchorLayoutUtil
    {
        /// <summary>
        ///     Manipulate the layout for horizontal
        ///     collapse progress.
        /// </summary>
        /// <param name="progress">
        ///     How collapsed the object should be.
        ///     0 is expanded, 1 = collapsed.
        /// </param>
        /// <param name="uiObjects">Array of RectTransforms</param>
        /// <param name="heightFractions">Array of heights</param>
        /// <param name="anchorMargin">margin around object</param>
        /// <param name="collapsedWidth">collapse threshold</param>
        /// <param name="alignment">Left or Right</param>
        public static void TweenedHorizontalLayout(
            float progress,
            RectTransform[] uiObjects,
            float[] heightFractions,
            float anchorMargin,
            float collapsedWidth,
            VerticalLayoutAlignment alignment
        )
        {
            float leftX, rightX;

            (leftX, rightX) = AnchorCalculations.XAnchor(
                alignment, progress, anchorMargin, collapsedWidth
            );
            ProcessLoop(
                anchorMargin, leftX, rightX, uiObjects, heightFractions
            );
        }

        public static void ExpandedLayout(
            RectTransform[] uiObjects,
            float[] heightFractions,
            float anchorMargin
        )
        {
            float leftX, rightX;
            (leftX, rightX) = AnchorCalculations.ExpandedXAnchor(
                anchorMargin
            );
            ProcessLoop(
                anchorMargin, leftX, rightX, uiObjects, heightFractions
            );
        }

        private static void ProcessLoop(
            float margin,
            float leftX, float rightX,
            RectTransform[] uiObjects,
            float[] heightFractions
        )
        {
            Vector2 anchorMin, anchorMax;

            float topY, bottomY, height;

            int length = uiObjects.Length;
            topY = AnchorCalculations.TOP_ANCHOR - margin;
            bottomY = topY;

            anchorMin = new Vector2(leftX, bottomY);
            anchorMax = new Vector2(rightX, topY);

            for(int index = 0; index < length; index++)
            {
                height = heightFractions[index];
                bottomY -= height;
                anchorMin.y = bottomY;
                anchorMax.y = topY;

                RectTransformUtil.SetPositionAndAnchors(
                    uiObjects[index],
                    anchorMin, anchorMax,
                    RectTransformUtil.PivotCentreMid,
                    Vector2.zero, Vector2.zero,
                    RectTransformUtil._100PC
                );
                topY -= height;
            }
        }
    }

}
