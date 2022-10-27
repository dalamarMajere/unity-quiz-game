using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderController : MonoBehaviour
{
    private Slider _slider;
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxValue(int maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public void UpdateSlider(int currentValue)
    {
        _slider.value = currentValue;
    }
}
