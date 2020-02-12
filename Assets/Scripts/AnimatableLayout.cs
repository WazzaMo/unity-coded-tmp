/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;

using Kreation.Util;

public enum VerticalLayoutAlignment
{
    Left,
    Right
}

public enum LayoutState
{
    Collapsed,
    Expanded,
    CollapsedButExpanding,
    ExpandedButCollapsing
}

/// <summary>
/// Uses anchors and dynamic sizing, to control the layout
/// of a series of components and move their layout.
/// </summary>
[AddComponentMenu("DesignerTech/AnimatableLayout")]
public class AnimatableLayout : MonoBehaviour
{
    [Tooltip("Select the objects to layout")][SerializeField]
    private RectTransform[] _UiObjectsToLayout = null;

    [Tooltip("(Expanded, Collapsed) Exp smaller")][SerializeField]
    private Vector2[] _UiObjectsHeightFractions = null;

    [Tooltip("Margin around objects")][SerializeField]
    private float _AnchorMargin = 0.05f;

    [Tooltip("How wide when collapsed (fraction)")][SerializeField]
    private float _CollapsedWidth = 0.5f;

    [Tooltip("Collapse to left or right?")][SerializeField]
    private VerticalLayoutAlignment _CollapsedAlignment
        = VerticalLayoutAlignment.Left;

    [Tooltip("Anim progress per second.")][SerializeField]
    private float _Speed = 2f;

    [Tooltip("Start Expanded?")][SerializeField]
    private bool IsExpanded = true;

    private ChangeTrigger<LayoutState> _State;
    private float _Progress;
    private bool _IsReady;

    public void BeginCollapse()
    {
        if (_IsReady && _State.Value == LayoutState.Expanded)
        {
            _State.Value = LayoutState.ExpandedButCollapsing;
        }
    }

    public void BeginExpansion()
    {
        if (_IsReady && _State.Value == LayoutState.Collapsed)
        {
            _State.Value = LayoutState.CollapsedButExpanding;
        }
    }

    void Awake()
    {
        _State = new ChangeTrigger<LayoutState>(GetInitialLayoutState());
        _State.Add(PerformSetupOnStateChange);
        _IsReady = IsGoodConfig();
    }

    private void Start()
    {
        if (_IsReady)
        {
            var heights = GetHeightFractionsForMode();

            AnchorLayoutUtil.ExpandedLayout(
                _UiObjectsToLayout, heights, _AnchorMargin
            );
        }
    }

    private void FixedUpdate()
    {
        if (_IsReady && ! IsProgressComplete())
        {
            if (_State.Value == LayoutState.CollapsedButExpanding)
            {
                ProgressExpand();
            }
            else if (_State.Value == LayoutState.ExpandedButCollapsing)
            {
                ProgressCollapse();
            }
        }
    }

    private LayoutState GetInitialLayoutState()
        => IsExpanded ? LayoutState.Expanded : LayoutState.Collapsed;

    private bool IsGoodConfig()
        => _UiObjectsToLayout != null
        && _UiObjectsToLayout.Length > 0
        && _UiObjectsHeightFractions != null
        && _UiObjectsHeightFractions.Length == _UiObjectsToLayout.Length;

    private bool IsProgressComplete() => _Progress >= 1.0f;

    private bool IsToBeginExpansion(
        LayoutState newState, LayoutState pastState
    )
        => newState == LayoutState.CollapsedButExpanding
        && pastState == LayoutState.Collapsed;

    private bool IsToBeginCollapse(
        LayoutState newState, LayoutState pastState
    )
        => newState == LayoutState.ExpandedButCollapsing
        && pastState == LayoutState.Expanded;

    private void PerformSetupOnStateChange(
        LayoutState state,
        LayoutState previous
    )
    {
        if (IsToBeginCollapse(state,previous)
            || IsToBeginExpansion(state,previous) )
        {
            _Progress = 0;
        }
    }

    private void ProgressCollapse()
    {
        ComputeProgressPercentage();
        var heights = GetHeightFractionsForMode();
        if (IsProgressComplete())
        {
            _State.Value = LayoutState.Collapsed;
        }
        else
        {
            AnchorLayoutUtil.TweenedHorizontalLayout(
                _Progress, _UiObjectsToLayout, heights,
                _AnchorMargin, _CollapsedWidth, _CollapsedAlignment
            );
        }
    }

    private void ProgressExpand()
    {
        ComputeProgressPercentage();
        var heights = GetHeightFractionsForMode();
        if (IsProgressComplete())
        {
            _State.Value = LayoutState.Expanded;
        }
        else
        {
            AnchorLayoutUtil.TweenedHorizontalLayout(
                1 -_Progress, _UiObjectsToLayout, heights,
                _AnchorMargin, _CollapsedWidth, _CollapsedAlignment
            );
        }
    }

    private float[] GetHeightFractionsForMode()
    {
        int length = _UiObjectsHeightFractions.Length;
        float[] fractions = new float[length];
        for(int index = 0; index < length; index++)
        {
            fractions[index] = GetFractionByStateAndIndex(index);
        }
        return fractions;
    }

    private float GetFractionByStateAndIndex(int index)
    {
        return AnimatableLayoutUtil.GetTweenedFractionByLayout(
            GetExpandedHeight(index), GetCollapsedHeight(index),
            _Progress, _State.Value
        );
    }

    private float GetExpandedHeight(int index)
        => _UiObjectsHeightFractions[index].x;

    private float GetCollapsedHeight(int index)
        => _UiObjectsHeightFractions[index].y;
    private void ComputeProgressPercentage()
        => _Progress += (_Speed * Time.deltaTime);
}
