using UnityEngine;

public class SeekFood : MonoBehaviour {
    public string foodType = "plant";
    public int hungerTolerance = 50;
    public float eatingRange = 1f;
    public float biteDamage = 5f;
    public float eatingGains = 5f;
    public int descisionmportance = 1;
    // Use this for initialization

    CreatureAI creature;
	void Start () {
        creature = GetComponent<CreatureAI>();
    }
	
	// Update is called once per frame
	void DoAIBehaviour() {
        //this part checks if the food type exists
        if (CreatureAI.creaturesByType.ContainsKey(foodType) == false)
            return;

        //find the closest creature to us
        //set defaults
        CreatureAI closest = null;
        float distance = Mathf.Infinity;


        //Loop through all food to find the closest
        foreach(CreatureAI c in CreatureAI.creaturesByType[foodType])
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
        if (closest == null)
            return;


        //if not hungry or beside food, do nothing
        if (creature.hunger >= hungerTolerance && distance > eatingRange)
            return;

        //if we get this far, there is food
        //if we are close enough, take a byte else get closer
        if (distance < eatingRange)
        {
            float bite = Mathf.Clamp(biteDamage * Time.deltaTime, 0, closest.health);
            closest.health -= bite;
            creature.hunger += bite * eatingGains;
        }
        else
        {
            //if not closer move closer
            Vector3 dir = closest.transform.position - this.transform.position;
            WeightedDirection wd = new WeightedDirection(dir, descisionmportance);
            creature.desiredDirections.Add(wd);
        }

	}
}
