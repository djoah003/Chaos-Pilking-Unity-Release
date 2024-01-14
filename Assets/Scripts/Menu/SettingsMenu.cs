using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Slider MasterSlider;

    private RectTransform ViewPort;

   

    void Start()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions(); // Clear all the options in resolutions dropdown

        List<string> options = new List<string>(); // Create a list of strings which are going to be our options

        int currentResolutionIndex = 0;
        
        // Loop through each element in our resolution array
        // For each of them will create a formatted string that displays our resolution
        // and add it to options list
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "hz";
            options.Add(option);

           

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); // add options list to resolutin drop-down
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution My_resolution = resolutions[resolutionIndex];
        Screen.SetResolution(My_resolution.width, My_resolution.height, Screen.fullScreen);
    }


    void Update()
    {
        
    }
    

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVol", volume);


    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
