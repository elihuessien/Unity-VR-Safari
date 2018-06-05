using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SM;
public class Run : State<AI>
{
    private static Run instance;
    private Run()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public static Run Instance
    {
        get
        {
            if(instance ==null)
            {
                new Run();
            }
            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        Debug.Log("Runign Away");
    }

    public override void ExitState(AI owner)
    {
    }

    public override void UpdateState(AI owner)
    {


        
        foreach (GameObject g in owner.objects)
        {
            Vector3 dir = g.transform.position - owner.transform.position;

            //for now if in inner range run;
            if (dir.magnitude < owner.innerRange)
            {
            }
        }
    }
}
