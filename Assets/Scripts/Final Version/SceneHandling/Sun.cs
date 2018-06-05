using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    Transform player;
    public float DayLightMinutes;
    bool isSwitching = false;
    float Timer;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
        Timer = 0f;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //reinitialize position to follow the player
       this.transform.position = player.transform.position;


        //I need to allow the sunlight time last as long as the minutes
        //meaning I need the full orbit to be twice as long as the minutes
        float t = 2 * DayLightMinutes * 60;
        transform.rotation *= Quaternion.AngleAxis((360/t) * Time.deltaTime, Vector3.right);

        //alow full sunset then swwitch scenes
        if(Timer > (t / 2 * 1.05) && isSwitching == false)
        {
            //this bool is to stop multiple scene change calls
            isSwitching = true;
            SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
            sc.FadeAndLoadScene("End");
        }

        //update timer
        Timer += Time.deltaTime;
	}
}
