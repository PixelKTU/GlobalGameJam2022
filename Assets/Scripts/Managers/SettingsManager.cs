using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    const string volumeKey = "volume";
    const string shakeKey = "shake";

    public bool screenShake = true;
    public int volume = 100;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        Load();
    }

    public void Save()
    {
        SetInt(volumeKey, volume);
        SetInt(shakeKey, screenShake == true ? 1 : 0);
    }
    public void Load()
    {
        screenShake = GetInt(shakeKey) == 1 ? true : false;
        volume = GetInt(volumeKey);
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

}
