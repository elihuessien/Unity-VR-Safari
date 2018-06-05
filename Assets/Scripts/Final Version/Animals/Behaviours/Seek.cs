using UnityEngine;

public class Seek : MonoBehaviour {
    AnimalAI animalAI;
    public int weight;
    public float hungerThreashoold;
    public int biteDamage;
    public int foodGain;
    public int eatrange;

    // Use this for initialization
    void Start () {
        animalAI = GetComponent<AnimalAI>();
        hungerThreashoold = 50;
        eatrange = 1;
        biteDamage = 5;
        foodGain = 5;
	}
	
	// Update is called once per frame
	void DoAIBehaviour() {
        //if nothing to seek do nothing
        if (animalAI.foods.Count == 0)
            return;

        //if not hungry do nothing
        if (animalAI.a.hunger > hungerThreashoold)
            return;


        //holds the closest things to watch out for
        float record = Mathf.Infinity;
        GameObject r = null;

        //look for closest food to eat
        foreach (GameObject g in animalAI.foods)
        {
            //if creature is dead, move on
            if (g.GetComponent<Animal>().health <= 0)
                continue;

            float dist = Vector3.Distance(transform.position, g.transform.position);
            //find the closest one
            if (dist < record)
            {
                record = dist;
                r = g;
            }
        }

        //if within range to eat, then eat
        if(record < eatrange)
        {
            //make shure we don't overkill enemy
            //get maximum posible bite and apply
            float bite = Mathf.Clamp(biteDamage * Time.deltaTime, 0, r.GetComponent<Animal>().health);
            r.GetComponent<Animal>().health -= bite;
            animalAI.a.hunger += bite * foodGain;
        }
        else if(record != Mathf.Infinity)
        {
            //else move closer
            Vector3 dir = r.transform.position - this.transform.position;
            WeightedSteer wd = new WeightedSteer(dir, weight);
            animalAI.desiredDirections.Add(wd);
        }
    }
}
