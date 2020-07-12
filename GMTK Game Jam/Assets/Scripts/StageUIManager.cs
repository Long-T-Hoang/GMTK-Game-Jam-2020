﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    public GameObject confirmationPopUp;
    public GameObject gameButton;
    public GameObject scoreboard;

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
