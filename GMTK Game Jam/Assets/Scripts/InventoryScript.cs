using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public GameObject inventory;
    //private bool inventoryEnabled = true;

    public static int tileSelected = 0;

    // Update is called once per frame
    void Update()
    {
        //Enable and disable inventory
        /*
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
        */

    }
}
