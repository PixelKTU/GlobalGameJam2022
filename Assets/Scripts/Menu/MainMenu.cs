using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject optionsUI;
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {
        mainUI.SetActive(false);
        optionsUI.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
