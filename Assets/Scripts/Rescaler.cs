/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public enum ScaleChange
{
    ToTopLeft,
    ToTopRight,
    ToBottomLeft,
    ToBottomRight
}

[RequireComponent(typeof(Image))]
public class Rescaler : MonoBehaviour
{
    [SerializeField] private float _ScaleFactor = 0.2f;
    [SerializeField] private float _Padding = 10f;

    private RectTransform _RectTransform;
    private Image _Panel;
    private Vector2 _OriginalPivot;
    private Vector3 _OriginalScale;
    private Vector2 _OriginalAnchorMin, _OriginalAnchorMax;

    private readonly Vector3 _100PC = new Vector3(1f, 1f, 1f);

    private readonly Vector2
        __PosTopLeft = new Vector2(1f, -1f),
        __PosBottomLeft = new Vector2(1f, 1f),
        __PosTopRight = new Vector2(-1f, -1f),
        __PosBottomRight = new Vector2(-1, 1f);

    private readonly Vector2
        TOPLEFT = new Vector2(0, 1f),
        TOPRIGHT = new Vector2(1, 1f),
        BTMLEFT = Vector2.zero,
        BTMRIGHT = new Vector2(1f, 0);

    void Start()
    {
        SetupComponentRefs();
        SaveDimensions();
    }

    public void ResetToNormal()
    {
        _RectTransform.pivot = _OriginalPivot;
        _RectTransform.localScale = _OriginalScale;
        _RectTransform.anchorMin = _OriginalAnchorMin;
        _RectTransform.anchorMax = _OriginalAnchorMax;
    }

    public void TopLeft() => TopLeftAndScale(_ScaleFactor, _Padding);

    public void TopLeftAndScale(float scale, float padding)
        => PinAndScale(scale, padding, TOPLEFT, __PosTopLeft);

    public void TopRight()
        => TopRightAndScale(_ScaleFactor, _Padding);

    public void TopRightAndScale(float scale, float padding)
        => PinAndScale(scale, padding, TOPRIGHT, __PosTopRight);

    public void BottomLeft() => BottomLeftAndScale(_ScaleFactor, _Padding);

    public void BottomLeftAndScale(float scale, float padding)
        => PinAndScale(scale, padding, BTMLEFT, __PosBottomLeft);

    public void BottomRight() => BottomRightAndScale(_ScaleFactor, _Padding);

    public void BottomRightAndScale(float scale, float padding)
        => PinAndScale(scale, padding, BTMRIGHT, __PosBottomRight);

    public void DumpRectTrans()
    {
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

    private void PinAndScale(
        float scale,
        float padding,
        Vector2 pinPoint,
        Vector2 position
    )
    {
        _RectTransform.anchorMin = pinPoint;
        _RectTransform.anchorMax = pinPoint;
        _RectTransform.pivot = pinPoint;
        _RectTransform.anchoredPosition = padding * position;
        _RectTransform.localScale = scale * _100PC;
    }


    private void SetupComponentRefs()
    {
        _RectTransform = GetComponent<RectTransform>();
        _Panel = GetComponent<Image>();
    }

    private void SaveDimensions()
    {
        _OriginalAnchorMin = _RectTransform.anchorMin;
        _OriginalAnchorMax = _RectTransform.anchorMax;
        _OriginalPivot = _RectTransform.pivot;
        _OriginalScale = _RectTransform.localScale;
    }
}
