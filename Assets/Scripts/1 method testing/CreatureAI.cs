using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAI : MonoBehaviour {
    //all creatures store themselves in a List by type
    //this is so that other creatures can find 
    //each other using this list

    //what stores type of the creature
    public string creatureType = "prey";
    static public Dictionary<string, List<CreatureAI>> creaturesByType;

    //stores all desired dirrections so that I can make
    //a movement decision
    public List<WeightedDirection> desiredDirections;
    Vector3 velocity;

    //Basic creature variables
    public int speed;
    public int accuracy;
    public float health;
    public float hunger;
    public float hungerLossRate;

    // Initializing and updating known creatures list
    void Start () {
        //if a dictionary hasn't been instanciated,
        //then make one
		if(creaturesByType == null)
        {
            creaturesByType = new Dictionary<string, List<CreatureAI>>();
        }

        //if the dictionarry doesn't contain this creature type,
        //add it
        if(creaturesByType.ContainsKey(creatureType) == false)
        {
            creaturesByType[creatureType] = new List<CreatureAI>();
        }
        //finally add yourself to the list
        creaturesByType[creatureType].Add(this);
	}


    //If the creature dies update the list again
    private void OnDestroy()
    {
        creaturesByType[creatureType].Remove(this);
    }






    // I use fixed update funtion to calculate any physics for the creature movements
    void FixedUpdate () {
        //if get hungrier
        hunger = Mathf.Clamp(hunger - Time.deltaTime * hungerLossRate, 0, 100);
        //if you are starving loose health
        if(hunger <= 0)
        {
            health = Mathf.Clamp(health - Time.deltaTime * 10f, 0, 100);
        }
        //if you have no health then die
        if(health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        //Broadcast a message to all AI scripts to give me movement details
        //So that I can make a movement decision
        //desired directions is initialized each time
        desiredDirections = new List<WeightedDirection>();
        //all behaviours are called to populate the desired directions array
        BroadcastMessage("DoAIBehaviour", SendMessageOptions.DontRequireReceiver);

        //new direction is calculated based on weights
        Vector3 direction = Vector3.zero;
        foreach(WeightedDirection wd in desiredDirections)
        {
            
            direction += wd.direction * wd.weight;
        }

        //normalize output
        velocity = Vector3.Lerp(velocity, direction.normalized * speed, Time.deltaTime * accuracy);

        //move in the direction decided
        transform.Translate(velocity * Time.deltaTime);
	}


    //TODO aany trigger functions needed?
}
