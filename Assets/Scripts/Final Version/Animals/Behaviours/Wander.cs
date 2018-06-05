using UnityEngine;

public class Wander : MonoBehaviour
{
    GameObject player;
    float interval;
    Vector3 playerPosition;
    float range;
    AnimalAI animalAI;
    Vector3 target;
    float arriveRange;


    // Use this for initialization
    void Start()
    {
        animalAI = GetComponent<AnimalAI>();
        player = GameObject.Find("Player");
        terrain t = GameObject.Find("Terrain").GetComponent<terrain>();
        range = GameObject.Find("Terrain").GetComponent<terrain>().worldSize;
        arriveRange = 2;
        interval = 0;
        target = Vector3.zero;
    }

    //pick a random idle time
    void ResetInterval()
    {
        interval = (int) Random.Range(10, 20);
    }

    void PickRandPosition ()
    {
        target = Vector3.zero;
        interval -= Time.deltaTime;

        if (interval <= 0)
        {
            //get player's position
            playerPosition = player.transform.position;

            //generate random positions
            float randX = Random.Range(playerPosition.x - range, playerPosition.x + range);
            float randZ = Random.Range(playerPosition.z - range, playerPosition.z + range);


            //make the target
            target = new Vector3(randX, 0, randZ);

            ResetInterval();
        }
    }

    // Update is called once per frame
    void DoAIBehaviour()
    {
        Vector3 dir = Vector3.zero;


        //if we don't have a target pick one
        if (target == Vector3.zero)
            PickRandPosition();
        else
        {
            //make a point on the same y plane as us
            Vector3 targ = new Vector3(target.x, transform.position.y, target.z);
            
            //if we have arrived at our target or we have no target then we pick another
            if (Vector3.Distance(targ, transform.position) < arriveRange)
                PickRandPosition();
            else
            {
                //move towards the target
                dir = target - transform.position;
            }
        }

        //add to the list of steers
        float weight = 0.1f;
        WeightedSteer wd = new WeightedSteer(dir, weight);
        animalAI.desiredDirections.Add(wd);
    }
}
