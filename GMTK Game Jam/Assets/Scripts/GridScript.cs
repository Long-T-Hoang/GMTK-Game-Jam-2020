using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    // Variables
    public Tile highlightTile;
    public Tilemap highlightMap;

    public Tile placementTile;
    public Tilemap placementMap;

    private Vector3Int previousCell;
    private Vector3Int currentCell;

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
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
        if (Input.GetMouseButtonDown(0))
            placementMap.SetTile(currentCell, placementTile);
    }
}
