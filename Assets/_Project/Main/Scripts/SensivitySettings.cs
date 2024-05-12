using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensivitySettings : MonoBehaviour
{
    public Slider slider;
    public static float ValueSlider;
    public Button close, save;

    public void Init()
    {
        close.onClick.AddListener(Close);
        save.onClick.AddListener(Save);
        Debug.Log("HAS SAVES SETTING?");
        if (ValueSlider == 0)
        {
            slider.value = 0.5f;
            if (Application.isMobilePlatform)
            { 
                ValueSlider = slider.value * 10;
            }
            else
            { 
                ValueSlider = slider.value * 1000;
            }
        }

    
        gameObject.SetActive(false);
    }

    public void Open()
    {
        if (Application.isMobilePlatform)
        {
            slider.value = ValueSlider / 10;
        }
        else
        {
            slider.value = ValueSlider / 1000;
        }
        gameObject.SetActive(true);
    }

    private void Save()
    {
        if (Application.isMobilePlatform)
        {
            ValueSlider = slider.value * 10;
        }
        else
        {
            ValueSlider = slider.value * 1000;
        }
        Close();
    }

    private void Close()
    {
        if (Application.isMobilePlatform)
        {
            slider.value = ValueSlider / 10;
        }
        else
        {
            slider.value = ValueSlider / 1000;
        }
        gameObject.SetActive(false);
    }
}
