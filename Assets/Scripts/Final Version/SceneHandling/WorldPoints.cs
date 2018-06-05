using UnityEngine;

public class WorldPoints : MonoBehaviour {
    public int points = 0;
    
    public int getPoints()
    {
        return points;
    }

    public void setPoints(int p)
    {
        points = p;
    }

    public void resetPoints()
    {
        points = 0;
    }
}
