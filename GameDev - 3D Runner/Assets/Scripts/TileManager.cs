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


    //List that contains the coins that are being shown in the game
    private List<GameObject> activeCoins = new List<GameObject>();

    //Variable to link the script to the position of the player
    public Transform PlayerTransform;

    // Number of coins to spawn
    public GameObject coinPrefab;

    //The X value of lanes
    int[] lane_pos = new int[] { -11, 0, 11 };

    // Start is called before the first frame update
    void Start()
    {
        //We spawn two empty tiles in order to start the game smooth
        SpawnTile(0);
        SpawnTile(0);
        SpawnTile(0);
        SpawnTile(1);
        SpawnCoins();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform.position.z > zSpawn - (numberOfTiles *  tileLength))
        {
            //Spawns the next tile
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            //Deletes the last used tile
            DeleteTile();
        }

        if(activeCoins.Count> 0)
        {
            //Debug.Log(11111);
            if (activeCoins[0] != null)
            {
                if (activeCoins[0].transform.position.z < PlayerTransform.position.z)
                {
                    DeleteCoin();
                }
            }
            else
            {
                activeCoins.RemoveAt(0);
            }
            
        }

        
    }

    //Function that will spawn the next tile
    public void SpawnTile(int tileIndex)
    {
        GameObject newTile = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(newTile);
        SpawnCoins();
        zSpawn += tileLength;
    }

    //Function to delete the last used tile in order to save memory
    private void DeleteCoin()
    {
        
        //Destroys the last coin if it was not already destroyed
        try
        {
            if (activeCoins[0] != null)
            {
                Destroy(activeCoins[0]);
            }
            activeCoins.RemoveAt(0);    
        }
        catch
        {
            activeCoins.RemoveAt(0);
        }
        
    }

    //Function to delete the last used tile in order to save memory
    private void DeleteTile()
    {

        //Destroys the last tile
        Destroy(activeTiles[0]);

        //Removes the tile from the List
        activeTiles.RemoveAt(0);
    }

    // Function used to spawn the coins along the map
    void SpawnCoins()
    {
        int coinsToSpawn = 2;

        for(int i = 0; i < coinsToSpawn; i++)
        {
            // save the object we spawned
            GameObject temp = Instantiate(coinPrefab, transform);

            // set the position of the coin equal to a random point in the collider
            temp.transform.position = GetRandomPoint();

            activeCoins.Add(temp);
        }

        
    }

    // Function to generate a random position on the map to spawn the coin
    Vector3 GetRandomPoint ()
    {
        int lane = Random.Range(1, 3);
        int x_pos = lane_pos[lane];

        // generate a point with random coordinates
        Vector3 point = new Vector3(
            x_pos,
            2,
            Random.Range(PlayerTransform.position.z + 100, (PlayerTransform.position.z + 200) + 1000)
            );

        return point;
    }
}
