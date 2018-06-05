using UnityEngine;


/*Notes taken from: https://www.youtube.com/watch?v=RlXxDfiy-J8*/
public class FirstPersonController : MonoBehaviour
{
    //sensitivity speeds for player movements
    public float walkspeed = 10, creepspeed = 5, runspeed = 15, jumpSpeed = 10;
    public bool isCreeping = false;
    CharacterController controller;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        //check if you are creeping
        if (Input.GetAxisRaw("Creep") == 1)
        {
            isCreeping = true;
            speed = creepspeed;
        }
        else
        {
            isCreeping = false;
            speed = walkspeed;
        }


        //get input and move
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if(controller.isGrounded)
            jump = Input.GetAxis("Jump");

        Vector3 dir = (this.transform.forward * vert * speed) + (this.transform.right * hor * speed) + (this.transform.up * jump * jumpSpeed);
        controller.SimpleMove(dir);

    }
}


