using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Notes taken from: https://www.youtube.com/watch?v=RlXxDfiy-J8*/
public class CameraMovement : MonoBehaviour {
    public float mouseSensitivity;
    public Transform playerTransform;



    float xAxisClamp = 0.0f;

	// Use this for initialization
	void Start () {
        mouseSensitivity = 2;
	}

    // Update is called once per frame
    private void Update()
    {
        //get mouse access
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //aply sensitivity
        float Xrot = mouseX * mouseSensitivity;
        float Yrot = mouseY * mouseSensitivity;

        //for clamp
        xAxisClamp -= Yrot;

        //get vectors to rotate
        Vector3 targetRotBody = playerTransform.rotation.eulerAngles;
        Vector3 targetRotCam = transform.rotation.eulerAngles;

        //inverted to account for way rotation works
        targetRotCam.x -= Yrot;
        targetRotCam.z = 0;
        targetRotCam.y = targetRotBody.y;
        targetRotBody.y += Xrot;

        //clamp the camera movement
        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }


        playerTransform.rotation = Quaternion.Euler(targetRotBody);
        transform.rotation = Quaternion.Euler(targetRotCam);
	}
    
}
