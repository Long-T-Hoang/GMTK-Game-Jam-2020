using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    public GameObject confirmationPopUp;
    public GameObject gameButton;

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
}
