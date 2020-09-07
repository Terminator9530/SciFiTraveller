using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider slider;
    public Toggle fullScreen;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            slider.value = 0.4f;
            AdjustVolume(0.4f);
        }

        if (PlayerPrefs.HasKey("FullScreen"))
        {
            fullScreen.isOn = PlayerPrefs.GetInt("FullScreen") == 1 ? true : false;
        }
        else
        {
            fullScreen.isOn = true;
            SetFullScreen(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustVolume(float vol)
    {
        PlayerPrefs.SetFloat("Volume", vol);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            PlayerPrefs.SetFloat("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("FullScreen", 0);
        }
        Screen.fullScreen = isFullScreen;
    }
}
