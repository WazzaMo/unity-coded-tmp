/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;
using TMPro;    // Knowing the Namespace is half the battle!!

using Kreation.Util;

/// <summary>
///     Makes the right edge of a UI object stretch out
///     buy adjusting the offset of the right edge from the
///     anchor based on the parent object. This is used to
///     demonstrate the effect of resizing child objects
///     in a resizing panel.
/// </summary>
[AddComponentMenu("DesignerTech/Resizer")]
public class Resizer : MonoBehaviour
{
    [Tooltip("Used to control the variation of the right edge")][SerializeField]
    private float RightMax = 100f, RightMin = 10f;

    [Tooltip("Speed of movement")][SerializeField]
    private float Speed = 1;

    [Tooltip("Tell all TMP text to auto font size")][SerializeField]
    private bool AutoSizeText = false;


    private TMP_Text[] _ChildTexts;
    private RectTransform _Transform;

    private float _RightRange, _RightOffset;
    private ChangeTrigger<bool> _AutoSizeTrigger;

    void Start()
    {
        _AutoSizeTrigger = new ChangeTrigger<bool>(AutoSizeText);
        _AutoSizeTrigger.Add(UpdateTextState);

        SetupComponentReferences();
        SetupRightValues();
        UpdateTextState(AutoSizeText, ! AutoSizeText);
    }

    void Update()
    {
        _AutoSizeTrigger.Value = AutoSizeText;
        float sizeFactor = Mathf.Sin(Time.time * Speed * Mathf.PI);
        SetRightSide(ComputeRightOffset(sizeFactor));
    }

    private void SetupComponentReferences()
    {
        _ChildTexts = GetComponentsInChildren<TMP_Text>();
        _Transform = GetComponent<RectTransform>();
    }

    private void SetupRightValues()
    {
        _RightRange = (RightMax - RightMin) / 2f;
        _RightOffset = (RightMax + RightMin) / 2f;
    }

    private void UpdateTextState(bool isEnabled, bool previouslyIsEnabled)
    {
        if (_ChildTexts != null && _ChildTexts.Length > 0)
        {
            for(int index = 0; index < _ChildTexts.Length; index++)
            {
                _ChildTexts[index]
                    .enableAutoSizing = isEnabled;
            }
        }
    }

    private float ComputeRightOffset(float sizeFactor)
        => _RightOffset + sizeFactor * _RightRange;

    private void SetRightSide(float right)
    {
        Vector2 offset = _Transform.offsetMax;
        offset.x = right;
        _Transform.offsetMax = offset;
    }
}
