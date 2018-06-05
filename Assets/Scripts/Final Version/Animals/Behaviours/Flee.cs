using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    AnimalAI animalAI;
    public float visionThreashold;
    int innerRange;

    // Use this for initialization
    void Start()
    {
        animalAI = GetComponent<AnimalAI>();
        visionThreashold = 3.5f;
        innerRange = 10;
    }

    // Update is called once per frame
    void DoAIBehaviour()
    {
        //if you are hungry, don't care about player
        bool isAware;
        if (animalAI.a.health < 50)
            isAware = false;
        else
            isAware = true;

        //if nothing to fear do nothing
        if (animalAI.dangers.Count == 0)
            return;


        //holds the closest things to watch out for
        float record = Mathf.Infinity;
        GameObject r = null;

        //look for closest danger
        foreach (GameObject g in animalAI.dangers)
        {
            //Player ignore conditions
            if(g.name == "Player")
            {
                //get direction to player
                Vector3 d = g.transform.position - transform.position;

                //if sneaking, and you can't see them
                if (g.GetComponent<OVRPlayerController>().isCreeping &&
                    Vector3.Dot(d, transform.forward) < visionThreashold)
                {
                    //if we are aware don't let the player close
                    if (Vector3.Distance(g.transform.position, transform.position) > innerRange)
                        continue;
                    else if (isAware == false)
                        continue;
                }
            }

            float dist = Vector3.Distance(transform.position, g.transform.position);
            //find the closest one
            if (dist < record)
            {
                record = dist;
                r = g;
            }
        }
        

        //calculate weight based on distance from the danger
        float weight = 10f / (record * record);

        //move away
        Vector3 dir = Vector3.zero;
        if(r != null)
           dir = this.transform.position - r.transform.position;

        //add to the list of steers
        WeightedSteer wd = new WeightedSteer(dir, weight);
        animalAI.desiredDirections.Add(wd);
    }
}
