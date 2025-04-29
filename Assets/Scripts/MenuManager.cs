using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Slider sizeXSlider;
    [SerializeField] private Slider sizeYSlider;
    [SerializeField] private Slider sizeZSlider;

    void Start()
    {
        sizeXSlider.onValueChanged.AddListener(OnSliderXValueChanged);
        sizeYSlider.onValueChanged.AddListener(OnSliderYValueChanged);
        sizeZSlider.onValueChanged.AddListener(OnSliderZValueChanged);
    }

    void OnSliderXValueChanged(float value)
    {
        int intSize = Mathf.RoundToInt(value);
        _gameSettings.setX(intSize);
    }

    void OnSliderYValueChanged(float value)
    {
        int intSize = Mathf.RoundToInt(value);
        _gameSettings.setY(intSize);
    }

    void OnSliderZValueChanged(float value)
    {
        int intSize = Mathf.RoundToInt(value);
        _gameSettings.setZ(intSize);
    }

}
