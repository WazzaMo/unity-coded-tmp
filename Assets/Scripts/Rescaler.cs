/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;

using Kreation.Util;

/// <summary>
/// Makes it easy to reposition a UI object relative to any
/// corner of the parent and apply a scale and padding.
/// </summary>
public class Rescaler : MonoBehaviour
{
    [SerializeField] private float _ScaleFactor = 0.2f;
    [SerializeField] private float _Padding = 10f;
    [SerializeField] private Vector2 _WidthAndHeight = new Vector2(100, 50);

    private RectTransform _RectTransform;
    private UiPositionSettings _OriginalSettings;

    void Start()
    {
        SetupComponentRefs();
        SaveDimensions();
    }

    public void ResetToNormal()
        => _RectTransform.SetUiPosition(_OriginalSettings);

    public void TopLeft()
        => PinAndScale(
            _ScaleFactor, _Padding,
            RectTransformUtil.PivotTopLeft,
            RectTransformUtil.AnchorPosTopLeft
        );

    public void TopRight()
        => PinAndScale(
            _ScaleFactor, _Padding,
            RectTransformUtil.PivotTopRight,
            RectTransformUtil.AnchorPosTopRight
        );

    public void BottomLeft()
        => PinAndScale(
            _ScaleFactor, _Padding,
            RectTransformUtil.PivotBottomLeft,
            RectTransformUtil.AnchorPosBottomLeft
        );

    public void BottomRight()
        => PinAndScale(
            _ScaleFactor, _Padding,
            RectTransformUtil.PivotBottomRight,
            RectTransformUtil.AnchorPosBottomRight
        );

    public void DumpRectTrans() => UiDebug.DumpRectTrans(gameObject);

    /// <summary>
    /// Uses a pivot point to "pin" the object as the relative
    /// side or corner point on the object from which the padding
    /// will be measured. If the Pin is the top-left corner and the
    /// padding is 10,-20 then there will be 10px from the left
    /// and 20 px from the top.  Remember that y is measured up
    /// so when measuring from the top, it needs to be negative.
    /// </summary>
    /// <param name="scale">Scale factors in 3 dimensions</param>
    /// <param name="padding">multiplier for the position</param>
    /// <param name="pinPoint">
    ///     x, y relative position (0 to 1) for pivot
    /// </param>
    /// <param name="position">x, y distance from edge in px</param>
    private void PinAndScale(
        float scale,
        float padding,
        Vector2 pinPoint,
        Vector2 position
    )
    {
        _RectTransform.FixSizeAndSetPosition(
            _OriginalSettings,
            pinPoint, pinPoint, pinPoint,
            padding * position,
            scale * RectTransformUtil._100PC
        );
    }

    private void SetupComponentRefs()
    {
        _RectTransform = this.GetComponentOrWarn<RectTransform>(
            $"GameObject {name} should be a UI object to have the component {nameof(Rescaler)} attached."
        );
    }

    private void SaveDimensions()
    {
        _OriginalSettings = _RectTransform.GetUiPosition();
        _WidthAndHeight = _OriginalSettings.WidthAndHeight;
    }
}
