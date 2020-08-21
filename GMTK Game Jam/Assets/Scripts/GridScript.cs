using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridScript : MonoBehaviour
{
    // Variables
    public Tilemap highlightMap;

    public static Tile placementTile;
    public static Tilemap placementMap;

    private Vector3Int previousCell;
    private Vector3Int currentCell;

    public int tileCount;
    public static int currentTileCount;

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
            highlightMap.SetTile(currentCell, placementTile);

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
                tileCount++;
            }
            else if (tileCount > 0)
            {
                placementMap.SetTile(currentCell, placementTile);
                tileCount--;
            }
        }

        currentTileCount = tileCount;
    }
}
