using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Notes taken from: https://www.youtube.com/watch?v=dycHQFEz8VI*/
public class GenerateTerrain : MonoBehaviour {
    //No sense making it public and editing in Unity
    //Because it would be deleted and recreated constantly
    //an the single edit would be lost
    int heightScale;
    float detailScale;

    /*list of trees it owns
    List<GameObject> myTrees = new List<GameObject>();*/

	// Use this for initialization
	void Start () {
        heightScale = 7;
        detailScale = 20f;

        //get and edit mesh
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        //calculate heights for each vertex
        for(int i=0; i<vertices.Length; i++)
        {
            float x = ((vertices[i].x + this.transform.position.x) / detailScale);
            float z = ((vertices[i].z + this.transform.position.z) / detailScale);
            vertices[i].y = Mathf.PerlinNoise(x, z) * heightScale;

            /*spread trees out using perlin Noise layering
            if(vertices[i].y > 6 && Mathf.PerlinNoise(x+5/10, z+5/10) * 10 > 4.5)
            {
                GameObject newTree = Spawner.getTree();
                if(newTree != null)
                {
                    newTree.transform.position = new Vector3(x, vertices[i].y, z);
                    newTree.SetActive(true);
                    myTrees.Add(newTree);
                }
            }*/
        }

        //aply changes
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    /*
    private void OnDestroy()
    {
        for(int i=0; i<myTrees.Count; i++)
        {
            myTrees[i].SetActive(false);
        }
        myTrees.Clear();
    }*/
}
