using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SM;

public class AI : MonoBehaviour {
    //basic variables
    public float health;
    public Vector3 forward;

    //movement speeds
    public float speed;

    //radiuses
    public int vision;
    public int innerRange;
    public float vissionThreshold;

    //awareness variables
    public List<GameObject> objects;
    public StateMachine<AI> stateMachine { get; set; }
    // Use this for initialization
    void Start() {
        health = 100;
        objects = new List<GameObject>();
        vision = 10;
        innerRange = 5;
        SphereCollider c = GetComponent<SphereCollider>();
        c.radius = vision;
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(Idle.Instance);
        vissionThreshold = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        stateMachine.Update();
        forward = Vector3.forward;
	}

    public void Move(Vector3 dir)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(dir.normalized * speed );
    }

    public void OnTriggerEnter(Collider other)
    {
        //if not the floor add it
        if (other.name != "Plane")
        {
            Debug.Log("Found you");
            objects.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Lost you");
        objects.Remove(other.gameObject);
    }

    public void SwitchState(State<AI> newState)
    {
        stateMachine.ChangeState(newState);
    }
}
