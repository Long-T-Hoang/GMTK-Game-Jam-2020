using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(LevelTransition.NextLevel);
        LevelTransition.incrementLevel();
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
