using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;

    private void Awake()
    {
        PlayerPrefs.SetInt("Stage", 0);
    }

    public void Play()
    {

        FindObjectOfType<SceneLoader>().LoadNextScene("Cutscene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchUI(GameObject altUI)
    {
        mainMenuUI.SetActive(false);
        altUI.SetActive(true);
    }

    public void ReturnButton(GameObject altUI)
    {
        mainMenuUI.SetActive(true);
        altUI.SetActive(false);
    }
}
