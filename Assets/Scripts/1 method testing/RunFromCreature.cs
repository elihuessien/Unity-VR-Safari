using UnityEngine;

public class RunFromCreature : MonoBehaviour
{
    public string enemyType = "plant";
    public float lookOutRange = 20;
    // Use this for initialization

    CreatureAI creature;
    void Start()
    {
        creature = GetComponent<CreatureAI>();
    }

    // Update is called once per frame
    void DoAIBehaviour()
    {
        //this part checks if the enemy type exists
        if (CreatureAI.creaturesByType.ContainsKey(enemyType) == false)
            return;

        //find the closest creature to us
        //set defaults
        CreatureAI closest = null;
        float distance = Mathf.Infinity;


        //Loop through all enemy to find the closest
        foreach (CreatureAI c in CreatureAI.creaturesByType[enemyType])
        {
            // if the creature is dead, then move on
            if (c.health <= 0)
                continue;

            //get it's distance from us
            float dist = Vector3.Distance(this.transform.position, c.transform.position);
            //if it's closer that the current closest then it is the closest
            if (closest == null || dist < distance)
            {
                closest = c;
                distance = dist;
            }
        }

        //if there was no closest then do nothing
        //this part checks if any food in the types list exists
        //or if we need to worry
        if (closest == null || distance > lookOutRange)
            return;
        

        //if we get this far, there an enemy
        
        //if not closer move closer
        Vector3 dir = this.transform.position - closest.transform.position;
        //this form of calculation increases the importance the closer it gets
        //to the enemy
        float descisionmportance = 10 / (distance * distance);
        WeightedDirection wd = new WeightedDirection(dir, descisionmportance);
        creature.desiredDirections.Add(wd);
    }

}
