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
public class SmoothScaler : MonoBehaviour
{
    public ScaleChange _ScaleType = ScaleChange.ToBottomLeft;

    private RectTransform _RectTransform;
    private Image _Panel;
    private Rect _OriginalDimensions;
    private Vector2 _Max, _Min;
    private Vector2 _OriginalPivot;
    private Vector3 _OriginalScale;

    private readonly Vector3 HALF = new Vector3(0.5f, 0.5f, 0.5f);

    void Start()
    {
        SetupComponentRefs();
        SaveDimensions();
    }

    public void ResetToNormal()
    {
        _RectTransform.offsetMax = _Max;
        _RectTransform.offsetMin = _Min;
        _RectTransform.localScale = _OriginalScale;
    }

    public void TopLeft()
    {
        _RectTransform.anchoredPosition = new Vector2(0, 0);
        _RectTransform.localScale = HALF;
    }

    private void SetupComponentRefs()
    {
        _RectTransform = GetComponent<RectTransform>();
        _Panel = GetComponent<Image>();
    }

    private void SaveDimensions()
    {
        _OriginalDimensions = _RectTransform.rect;
        _Max = _RectTransform.offsetMax;
        _Min = _RectTransform.offsetMin;
        _OriginalPivot = _RectTransform.anchoredPosition;
        _OriginalScale = _RectTransform.localScale;
    }
}
