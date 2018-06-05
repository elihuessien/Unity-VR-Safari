using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SM;

public class Idle : State<AI>
{
    private static Idle instance;
    private Idle()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public static Idle Instance
    {
        get
        {
            if (instance == null)
            {
                new Idle();
            }
            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        Debug.Log("Idle");
    }

    public override void ExitState(AI owner)
    {
    }

    public override void UpdateState(AI owner)
    {
        //If in danger Run
        Vector3 dir = new Vector3(0, 0, 1);
        owner.Move(dir);

        foreach (GameObject g in owner.objects)
        {
            dir = g.transform.position - owner.transform.position;

            //if it is the player
            if (g.name == "Player")
            {
                //for now if in inner range run;
                if (dir.magnitude < owner.innerRange)
                {
                    owner.SwitchState(Run.Instance);
                }


                //if we can see it run
                if (Vector3.Dot(dir, owner.forward) < owner.vissionThreshold)
                {
                    owner.SwitchState(Run.Instance);
                }
            }
        }
    }
}
