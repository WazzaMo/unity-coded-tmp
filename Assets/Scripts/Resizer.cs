/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


using UnityEngine;


using TMPro;    // Knowing the Namespace is half the battle!!


public class Resizer : MonoBehaviour
{
    public float RightMax, RightMin;
    public float Speed = 1;
    public bool AutoSizeText = false;

    private TMP_Text[] _ChildTexts;
    private RectTransform _Transform;

    private float _RightRange, _RightOffset;
    private ChangeTrigger<bool> _AutoSizeTrigger;

    void Start()
    {
        _AutoSizeTrigger = new ChangeTrigger<bool>(AutoSizeText);
        _AutoSizeTrigger.Add(x => Debug.Log($"New value: {x}"));
        _AutoSizeTrigger.Add(UpdateTextState);

        SetupComponentReferences();
        SetupRightValues();
        UpdateTextState(AutoSizeText);
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

    private void UpdateTextState(bool isEnabled)
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
