using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [SerializeField] public GameObject menu;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Slider sizeXSlider;
    [SerializeField] private Slider sizeYSlider;
    [SerializeField] private Slider sizeZSlider;
    [SerializeField] public GameObject winMenu;
    [SerializeField] public GameObject looseMenu;
    [SerializeField] public GameObject mainMenu;
    [SerializeField] private TMP_Text xLabel;
    [SerializeField] private TMP_Text yLabel;
    [SerializeField] private TMP_Text zLabel;

    void Start()
    {
        sizeXSlider.onValueChanged.AddListener(OnSliderXValueChanged);
        sizeYSlider.onValueChanged.AddListener(OnSliderYValueChanged);
        sizeZSlider.onValueChanged.AddListener(OnSliderZValueChanged);
        xLabel.text = _gameSettings.getX().ToString();
        yLabel.text = _gameSettings.getY().ToString();
        zLabel.text = _gameSettings.getZ().ToString();
    }

    void OnSliderXValueChanged(float value)
    {
        int rounded = Mathf.RoundToInt(value);

        if (rounded % 2 == 0)
        {
            rounded += 1;
        }

        sizeXSlider.SetValueWithoutNotify(rounded);

        _gameSettings.setX(rounded);
        xLabel.text = _gameSettings.getX().ToString();
    }

    void OnSliderYValueChanged(float value)
    {
        int rounded = Mathf.RoundToInt(value);

        if (rounded % 2 == 0)
        {
            rounded += 1;
        }

        sizeYSlider.SetValueWithoutNotify(rounded);

        _gameSettings.setY(rounded);
        yLabel.text = _gameSettings.getY().ToString();
    }

    void OnSliderZValueChanged(float value)
    {
        int rounded = Mathf.RoundToInt(value);

        if (rounded % 2 == 0)
        {
            rounded += 1;
        }

        sizeZSlider.SetValueWithoutNotify(rounded);

        _gameSettings.setZ(rounded);
        zLabel.text = _gameSettings.getZ().ToString();
    }

    public void doExitGame()
    {
        Application.Quit();
    }

}
