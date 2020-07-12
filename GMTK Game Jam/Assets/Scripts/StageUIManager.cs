using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    public GameObject confirmationPopUp;
    public GameObject gameButton;
    public GameObject scoreboard;
    public GameObject scoreLabel;

    Player playerScript;

    // Methods
    public void ConfirmationToggle()
    {
        if(gameButton.activeInHierarchy)
        {
            gameButton.SetActive(false);
            confirmationPopUp.SetActive(true);
        }
        else
        {
            gameButton.SetActive(true);
            confirmationPopUp.SetActive(false);
        }
    }

    public void NoUI()
    {
        gameButton.SetActive(false);
        confirmationPopUp.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(LevelTransition.NextLevel);
        LevelTransition.incrementLevel();
    }

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }

    // Update score
    private void Update()
    {
        if(playerScript.IsWin)
        {
            scoreboard.SetActive(true);
        }
    }
}
