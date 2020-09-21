using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider slider;
    public Toggle fullScreen;
    public GameObject fullScreenPanel;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            fullScreenPanel.gameObject.SetActive(false);
        }
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
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
        Screen.fullScreen = isFullScreen;
    }
}
