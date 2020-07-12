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
        SceneManager.LoadSceneAsync(LevelStats.NextLevel);
        LevelStats.incrementLevel();
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
}
