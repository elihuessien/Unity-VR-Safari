using UnityEngine;

public class Animal : MonoBehaviour {
    public float health;
    public float hunger;
    public string food;
    public int hungerLossRate;
    public float speed = 0f;

    Transform player;
    float range;
    static Animator anm;
    CharacterController c;

    Vector3 gravity = new Vector3(0,-0.5f,0);


    // Use this for initialization
    void Start () {
        hunger = 100;
        anm = GetComponent<Animator>();
        c = GetComponent<CharacterController>();
        player = GameObject.Find("Player").transform;

        range = GameObject.Find("Terrain").GetComponent<terrain>().worldSize;
        c.SimpleMove(new Vector3(0,0.1f,0));
    }

    void Update()
    {
        //if out of the range of the player die
        if (Vector3.Distance(transform.position, player.position) > range)
            Die();

        //hungrier as time goes by
        hunger = Mathf.Clamp(hunger - Time.deltaTime * hungerLossRate, 0 , 100);

        //if no food loose health
        if(hunger <=0)
            health = Mathf.Clamp(health - Time.deltaTime * 10, 0, 100);

        //if no health then die
        if (health == 0)
        {
            Die();
        }
        else
        {
            Animate();
        }

        //apply gravity
        c.Move(gravity);
    }

    void Die ()
    {
        //ANIMATION maybe
        Destroy(gameObject);
    }

    void Animate ()
    {
        //if I'm not food I can do stuf
        if(transform.tag != "Veg" && anm != null)
        {
            float speed = c.velocity.magnitude;
            if(speed > 0)
            {
                anm.SetBool("isWalking", true);
                anm.SetBool("isRunning", false);
            }
            else if(speed > 5)
            {
                anm.SetBool("isWalking", false);
                anm.SetBool("isRunning", true);
            }
            else
            {
                anm.SetBool("isWalking", false);
                anm.SetBool("isRunning", false);
            }
        }
    }
}
