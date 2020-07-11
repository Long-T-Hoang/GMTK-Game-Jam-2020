using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public int gameScene;
    public GameObject optionMenu;
    public GameObject mainMenu;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(gameScene);
    }

    public void OptionToggle()
    {
        if(optionMenu.activeInHierarchy)
        {
            optionMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            optionMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
