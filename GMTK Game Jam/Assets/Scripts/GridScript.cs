﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridScript : MonoBehaviour
{
    // Variables
    public Tile highlightTile;
    public Tilemap highlightMap;

    public static Tile placementTile;
    public static Tilemap placementMap;

    private Vector3Int previousCell;
    private Vector3Int currentCell;

    Player playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            highlightMap.SetTile(currentCell, null);

            return;
        }

        // get current grid location
        currentCell = highlightMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // if the position has changed
        if (currentCell != previousCell)
        {
            // set the new tile
            highlightMap.SetTile(currentCell, highlightTile);

            // erase previous
            highlightMap.SetTile(previousCell, null);

            // save the new position for next frame
            previousCell = currentCell;
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerScript.Playing)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.transform.CompareTag("Wall") || hit.transform.CompareTag("Death"))
                {
                    return;
                }
            }

            if (placementMap.HasTile(currentCell))
            {
                placementMap.SetTile(currentCell, null);
            }
            else
            {
                placementMap.SetTile(currentCell, placementTile);
            }
        }
    }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

    //Disable build if the player is moving
    public void playGame()
    {
        playing = !playing;
    }
=======
>>>>>>> ca9a087976cc028fb50e9eaee06882c6c00a463b
=======
>>>>>>> ca9a087976cc028fb50e9eaee06882c6c00a463b
=======
>>>>>>> ca9a087976cc028fb50e9eaee06882c6c00a463b
}
