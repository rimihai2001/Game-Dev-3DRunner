using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Array with the prefab tiles
    public GameObject[] tilePrefabs;

    //The Z position to calculate when and where the next tile will spawn
    public float zSpawn = 0;

    //The length of a single tile
    public float tileLength = 200;

    //The number of prefab tiles
    public int numberOfTiles = 9;

    //List that contains the tiles that are being shown in the game
    private List<GameObject> activeTiles = new List<GameObject>();

    //Variable to link the script to the position of the player
    public Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //We spawn two empty tiles in order to start the game smooth
        SpawnTile(0);
        SpawnTile(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform.position.z - 50 > zSpawn - (numberOfTiles *  tileLength))
        {
            //Spawns the next tile
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            //Deletes the last used tile
            DeleteTile();
        }
    }

    //Function that will spawn the next tile
    public void SpawnTile(int tileIndex)
    {
        GameObject newTile = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(newTile);
        zSpawn += tileLength;
    }

    //Function to delete the last used tile in order to save memory
    private void DeleteTile()
    {
        //Destroys the last tile
        Destroy(activeTiles[0]);

        //Removes the tile from the List
        activeTiles.RemoveAt(0);
    }
}
