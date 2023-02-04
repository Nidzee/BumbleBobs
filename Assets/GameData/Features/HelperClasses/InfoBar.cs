using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBar : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _valueLabel;

    float _maxValue;
    float _currentValue;


    public void InitInfoBar(float maxValue, float currentValue)
    {
        _maxValue = maxValue;
        _currentValue = currentValue;


        _slider.maxValue = 1;
        _slider.minValue = 0;
        _slider.value = _currentValue / _maxValue;


        _valueLabel.text = _currentValue + "/" + _maxValue;
    }

    public void ReduceValue(float reduceValue)
    {
        // Reduce current value by points
        _currentValue -= reduceValue;

        // Clamp value
        if (_currentValue <= 0)
        {
            _currentValue = 0;
        }

        // Change slider fill
        _slider.value = _currentValue / _maxValue;
        _valueLabel.text = _currentValue + "/" + _maxValue;
    }
}