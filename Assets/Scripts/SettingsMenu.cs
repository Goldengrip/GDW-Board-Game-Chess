using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;//variable to store resolutions
    private void Start()//Controls resolutions in settings menu 
    {
        resolutions =Screen.resolutions;

        resolutionDropdown.ClearOptions();
        //converts resolutions into string variables 
        List<string> options = new List<string>();

        int currentReselutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            //automatically find resolution for screen 
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentReselutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentReselutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetVolume (float volume)// volume adjusment 
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetFullScreen(bool isfullScreen)//toggles fullscreen 
    {
        Screen.fullScreen = isfullScreen;
    }
}
