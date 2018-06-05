using System.Collections;
using UnityEngine;


/*Notes taken from: https://www.youtube.com/watch?v=dycHQFEz8VI*/
class Tile
{
    public GameObject theTile;
    public float creationTime;

    public Tile(GameObject t, float ct)
    {
        theTile = t;
        creationTime = ct;
    }
}


public class terrain : MonoBehaviour {
    public GameObject player;
    public GameObject plane;


    public int planeSize = 10;
    public int halfTiles = 7;
    public int worldSize;
    Vector3 startPos;

    //Hashtable lightenees the memory and processing load
    Hashtable tiles = new Hashtable();

	// Use this for initializating the terain
	void Start () {
        player = GameObject.Find("Player");

        //Set The world size
        worldSize = planeSize * halfTiles;

        //everything starts at the center
        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        //Loop through all the tiles
        for(int x = -halfTiles; x < halfTiles; x++)
        {
            for(int z = -halfTiles; z < halfTiles; z++)
            {
                //get position for each tile
                Vector3 pos = new Vector3(x * planeSize + startPos.x, 0, z*planeSize + startPos.z);
               
                //make a tile there
                GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                //create unique name for each tile
                string tilename = "Tile_" + ((int)pos.x).ToString() + "_" + ((int)pos.z).ToString();
                t.name = tilename;

                //add the newly created tile to the list
                Tile tile = new Tile(t, updateTime);
                tiles.Add(tilename, tile);
            }
        }

	}

    // Update plane generation to follow the player
    void Update () {
        //Get how much the has player moved from the last check
        int xMove = (int)(player.transform.position.x - startPos.x);
        int zMove = (int)(player.transform.position.z - startPos.z);

        //if you have move one plane accross
        if (Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize)
        {
            float updateTime = Time.realtimeSinceStartup;

            //force position around nearest tile size
            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);


            //loop through tiles and doo the editing
            for (int x = -halfTiles; x < halfTiles; x++)
            {
                for (int z = -halfTiles; z < halfTiles; z++)
                {
                    //get position for each tile
                    Vector3 pos = new Vector3(x * planeSize + playerX, 0, z * planeSize + playerZ);

                    //get name for what the tile should be
                    string tilename = "Tile_" + ((int)pos.x).ToString() + "_" + ((int)pos.z).ToString();


                    //if the tile doesn't exist, add it
                    if(!tiles.Contains(tilename))
                    {
                        //make a tile there
                        GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);
                        t.name = tilename;
                        Tile tile = new Tile(t, updateTime);
                        tiles.Add(tilename, tile);
                    }
                    else
                    {
                        //else reset the creation time for the tile
                        (tiles[tilename] as Tile).creationTime = updateTime;
                    }
                }
            }



            //finaly, destroy any tiles that have "expired"
            //or are not in use

            //to do this create a new edited table based on the old one
            Hashtable newTerrain = new Hashtable();
            foreach (Tile tl in tiles.Values)
            {
                if(tl.creationTime != updateTime)
                {
                    //destroy the tile
                    Destroy(tl.theTile);
                }
                else
                {
                    newTerrain.Add(tl.theTile.name, tl);
                }
            }

            //copy values back in
            tiles = newTerrain;
            startPos = player.transform.position;
        }
    }
}
