using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QualitySttings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _qualityDroopDown;

    private int _quality;

    [SerializeField]
    private Toggle _fullScreenToggle;

    [SerializeField]
    private TMP_Dropdown _resolutionDropDown;

    private Resolution[] _resolutions;

    private void Start()
    {
        _quality = PlayerPrefs.GetInt("Quality", 3);
        _qualityDroopDown.value = _quality;
        QualitySettings.SetQualityLevel(_quality);

        if (Screen.fullScreen)
            _fullScreenToggle.isOn = true;
        else
            _fullScreenToggle.isOn = false;

        CheckResolution();

    }

    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("Quality", value);
    }
    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void CheckResolution()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                actualResolution = i;
        }

        _resolutionDropDown.AddOptions(options);
        _resolutionDropDown.value = actualResolution;
        _resolutionDropDown.RefreshShownValue();

        _resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionIndex", 0);
    }

    public void ChangeResolution(int index)
    {
        PlayerPrefs.SetInt("ResolutionIndex", _resolutionDropDown.value);

        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
