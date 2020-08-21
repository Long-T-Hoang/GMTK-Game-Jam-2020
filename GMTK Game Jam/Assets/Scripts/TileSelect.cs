using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
 
public class TileSelect : MonoBehaviour
{
    public Tile tile;
    public Tilemap tilemap;
    public int tileSlot;

    private static int tileSelected = 1;

    // Update is called once per frame
    void Update()
    {
        if (tileSelected == tileSlot && tile != null && tilemap != null)
        {
            GridScript.placementTile = tile;
            GridScript.placementMap = tilemap;
        }
    }
    public void selectTile()
    {
        tileSelected = tileSlot;
        Debug.Log(tileSelected);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
