using System.Collections;
using UnityEngine;

public class WeightedDirection
{
    public readonly Vector3 direction;
    public readonly float weight;

	// This class's function is to stoore all the weighted dirrections
    // so it doesn't need start and update

    //function to store dirrection
    public WeightedDirection(Vector3 d, float w)
    {
        direction = d;
        weight = w;
    }
}
