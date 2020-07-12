using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    // Variables
    public Tilemap wallPlacementMap;
    public Tilemap reversePlacementMap;

    Player playerScript;
    Vector3 PLAYER_SPAWN;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        PLAYER_SPAWN = playerScript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        playerScript.transform.position = PLAYER_SPAWN;

        wallPlacementMap.ClearAllTiles();
        reversePlacementMap.ClearAllTiles();
    }
}
