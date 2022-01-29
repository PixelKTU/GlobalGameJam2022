using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject optionsUI;
    [SerializeField] UnityEngine.UI.Slider volumeSlider;
    [SerializeField] UnityEngine.UI.Toggle shakeToggle;
    public void BackButton()
    {
        mainUI.SetActive(true);
        optionsUI.SetActive(false);
        SettingsManager.instance.Save();
    }
    public void ShakeToggle()
    {
        SettingsManager.instance.screenShake = shakeToggle.isOn;
    }
    public void VolumeChange()
    {
        SettingsManager.instance.volume = (int)volumeSlider.value;
        AudioManager.instance.SetAudioLevel(volumeSlider.value);
    }
    public void UpdateValues()
    {
        volumeSlider.value = SettingsManager.instance.volume;
        shakeToggle.isOn = SettingsManager.instance.screenShake;
        print(SettingsManager.instance.screenShake);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(SettingsManager.instance.screenShake);
            print(SettingsManager.instance.volume);
        }
    }
    
}
