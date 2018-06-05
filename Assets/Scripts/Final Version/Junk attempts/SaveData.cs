using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*All scene managment scripts were adapted from: 
 https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/game-state?playlist=44381*/
[CreateAssetMenu]
public class SaveData : MonoBehaviour {

    [SerializeField]
    public class PointsData
    {
        public int points = 0;


        public void Clear ()
        {
            points = 0;
        }


        public void SetValue (int val)
        {
            points = val;
        }

        public int GetValue ()
        {
            return points;
        }
    }

    //write the rest of the funtions based on the
    PointsData savedData = new PointsData();
    public void Reset()
    {
        savedData.Clear();
    }

    public int Load()
    {
       return savedData.GetValue();
    }

    public void Save(int points)
    {
        savedData.SetValue(points);
    }
 }
