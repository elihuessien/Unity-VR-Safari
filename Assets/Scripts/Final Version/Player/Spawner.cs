using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public int numLife = 10;
    int worldSize;

    [SerializeField]
    GameObject[] prefabs;

    /*trees
    //code adapted from https://www.youtube.com/watch?v=uDMfVMKM_98
    static int numtrees = 500;
    public GameObject tree;
    static GameObject[] trees;*/

    //rest of life
    public List<GameObject> life;
	// Use this for initialization
	void Start () {
        worldSize = GameObject.Find("Terrain").GetComponent<terrain>().worldSize;
        life = new List<GameObject>();

        /*generate tree pool
        trees = new GameObject[numtrees];
        for(int i = 0; i < numtrees; i++)
        {
            trees[i] = Instantiate(tree, Vector3.zero, Quaternion.identity);
            trees[i].SetActive(false);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		if(life.Count < numLife)
        {
            MakeLife();
        }

        //clean up
        for(int i = (life.Count - 1) ; i > 0; i--)
        {
            GameObject g = life[i];
            if (g == null)
                life.Remove(g);
        }
	}

    void MakeLife()
    {
        //get random position within the world
        float randX = Random.Range(transform.position.x - worldSize, transform.position.x + worldSize);
        float randZ = Random.Range(transform.position.z - worldSize, transform.position.z + worldSize);
        Vector3 pos = new Vector3(randX, 6, randZ);

        //make random life at that position
        GameObject g = Instantiate(GetPrefab(), pos, Quaternion.identity);

        //add new life to the list
        life.Add(g);
    }

    GameObject GetPrefab()
    {
        //chose a random life to instanciate
        int choice = Random.Range(0, prefabs.Length);

        return prefabs[choice];
    }


    /*get a tree
    static public GameObject getTree()
    {
        for(int i = 0; i < numtrees; i++)
        {
            if(!trees[i].activeSelf)
            {
                return trees[i];
            }
        }
        return null;
    }*/
}
