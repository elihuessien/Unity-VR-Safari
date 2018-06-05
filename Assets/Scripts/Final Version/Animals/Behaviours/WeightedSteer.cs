using System.Collections;
using UnityEngine;

public class WeightedSteer
{
    public readonly Vector3 steer;
    public readonly float weight;

    // This class's function is to stoore all the weighted dirrections
    // so it doesn't need start and update

    //function to store dirrection
    public WeightedSteer(Vector3 s, float w)
    {
        steer = s;
        weight = w;
    }
}
