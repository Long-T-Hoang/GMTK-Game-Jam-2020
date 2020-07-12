using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelect : MonoBehaviour
{
    public Tile tile;
    public Tilemap tilemap;
    public int tileSlot;

    // Update is called once per frame
    void Update()
    {
        if (InventoryScript.tileSelected == tileSlot && tile != null && tilemap != null)
        {
            GridScript.placementTile = tile;
            GridScript.placementMap = tilemap;
        }
    }
    public void selectTile()
    {
        InventoryScript.tileSelected = tileSlot;
        Debug.Log(InventoryScript.tileSelected);
    }
}
