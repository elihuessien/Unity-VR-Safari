using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour {
    public int points;
    Transform camera;
	// Use this for initialization
	void Start () {
        points = 0;
        camera = transform.Find("OVRCameraRig");
        if(camera == null)
        {
            Debug.Log("Could not find Camera");
        }
    }
	
	// Update is called once per frame
	public int GetPoints () {

        RaycastHit hit;
        Physics.Raycast(camera.position, camera.forward, out hit, 20);
        Debug.DrawRay(camera.position, camera.forward * 20);

        int inc = 0;
        if(hit.collider.gameObject == null)
        {
            return 0;
        }
        switch (hit.collider.tag)
        {
            case "Veg":
                inc = 1;
                break;


            case "Tree":
                inc = 1;
                break;


            case "Herby":
                if (Vector3.Distance(transform.position, hit.collider.transform.position) < 20)
                    inc = 5;
                else
                    inc = 2;
                break;


            case "Carny":
                if (Vector3.Distance(transform.position, hit.collider.transform.position) < 20)
                    inc = 8;
                else
                    inc = 3;
                break;


            default:
                break;
        }

        points += inc;
        return inc;
	}
}
