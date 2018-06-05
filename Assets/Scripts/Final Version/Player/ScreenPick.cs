using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=bQayHTts7HI
public class ScreenPick : MonoBehaviour {
    int picIndex = 0;
    int points = 0;
    Texture2D pic;
    Texture2D border;
    bool ShotTaken = false;
    public bool CameraUp = false;
    int wpadding, hpadding, width, height, borderPadding;
    float reloadTime;
    //https://answers.unity.com/questions/568112/how-to-make-a-gui-texture-image-transparent.html
    public float fadeTime = 1;
    float fade;

    //UI variables
    public Image cameraUI;
    public RawImage pictureImage;
    public Text pointText;

    PointManager pointManager;

    // Use this for initialization
    void Start () {
        //define padings
        wpadding = Screen.width / 4;
        hpadding = Screen.height / 4;
        width = Screen.width / 2;
        height = Screen.height / 2;
        borderPadding = 2;

        pointManager = GetComponent<PointManager>();

        //define picture details
        pic = new Texture2D(width, height, TextureFormat.RGB24, false);

        //define border details
        border = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        border.Apply();


        //prepare for first pic
        reloadTime = 1;

        //initialize canvas
        cameraUI.GetComponent<CanvasGroup>().alpha = 0;
        fade = fadeTime;
    }
	
	// Update is called once per frame
	void Update () {
        //trigger camerra UI
        if (Input.GetAxisRaw("Fire1") > 0 || Input.GetAxisRaw("Oculus_GearVR_LIndexTrigger") > 0)
        {
            CameraUp = true;
        }

        if(Input.GetAxisRaw("Cancel") > 0)
        {
            CameraUp = false;
            reloadTime = 1;
        }

        if (CameraUp)
        {
            //Display UI
            cameraUI.GetComponent<CanvasGroup>().alpha = 1;

            if ((Input.GetAxisRaw("Fire1") > 0 || Input.GetAxisRaw("Oculus_GearVR_LIndexTrigger") > 0) && reloadTime < 0)
            {
                StartCoroutine("TakeShot");
                reloadTime = 1;
            }


            //reload camera
            if (reloadTime >= 0)
                reloadTime -= Time.deltaTime;
        }
        else
        {
            //reset
            cameraUI.GetComponent<CanvasGroup>().alpha = 0;
            reloadTime = 1;
        }




        //when picture is taken display picture for a period of time
        if(ShotTaken)
        {
            //Set text
            pointText.text = "+ " + points;
            //set image
            pictureImage.GetComponent<RawImage>().texture = pic;
            
            if (fade > 0)
                fade -= Time.deltaTime;

            pictureImage.GetComponent<CanvasGroup>().alpha = Map(fade, 0, fadeTime, 0, 1);


            //end clause
            if(fade < 0)
            {
                pictureImage.GetComponent<CanvasGroup>().alpha = 0;
                fade = fadeTime;
                ShotTaken = false;
            }
        }
	}
    /*
    private void OnGUI()
    {
        if (ShotTaken)
        {
            //set camera color fading GUI image
            float alpha = Map(fadeTime, 0, 1, 0, 255);
            GUI.color = new Color(255, 255, 255, alpha);

            if(fadeTime > 0 )
                fadeTime -= Time.deltaTime;
            //display shot
            GUI.DrawTexture(new Rect(10, 10, 80, 80), pic, ScaleMode.StretchToFill); //top

            GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
            myStyle.alignment = TextAnchor.MiddleCenter;
            GUI.TextField(new Rect(10, 10, 80, 80), "+ " + points, myStyle);


            //end clause
            if (alpha <= 0)
            {
                ShotTaken = false;
                fadeTime = 1;
            }
        }

        //draw details for pick
        if (CameraUp)
        {
            GUI.DrawTexture(new Rect(wpadding, hpadding, width, borderPadding), border, ScaleMode.StretchToFill); //top
            GUI.DrawTexture(new Rect(wpadding, hpadding + height, width, borderPadding), border, ScaleMode.StretchToFill); //bottom
            GUI.DrawTexture(new Rect(wpadding, hpadding, borderPadding, height), border, ScaleMode.StretchToFill); //left
            GUI.DrawTexture(new Rect(wpadding + width, hpadding, borderPadding, height), border, ScaleMode.StretchToFill); //right

            //crosshairs
            GUI.DrawTexture(new Rect(width, height - 5, 5, 5 * 3), border, ScaleMode.StretchToFill); //vert
            GUI.DrawTexture(new Rect(width - 5, height, 5 * 3, 5), border, ScaleMode.StretchToFill); //horiz
        }
    }*/

    IEnumerator TakeShot ()
    {
        yield return new WaitForEndOfFrame();

        //dissable reticle for the pic
        //cameraUI.GetComponent<CanvasGroup>().alpha = 0;

        //take picture
        pic.ReadPixels(new Rect(wpadding, hpadding, width, height), 0, 0);
        pic.Apply();

        //enable reticle for the pic
        cameraUI.GetComponent<CanvasGroup>().alpha = 1;


        //Save picture as png
        //https://www.youtube.com/watch?v=hkpO_T6RNMM
        byte[] bytes = pic.EncodeToJPG();

        //update file picture idex
        //If a file already exsists in this spot move to another spot
        while (File.Exists(Application.dataPath + "/Pictures/Picture" + picIndex + ".png"))
        {
            picIndex++;
        }
        File.WriteAllBytes(Application.dataPath + "/Pictures/Picture" + picIndex + ".png", bytes);
        picIndex++;

        //update points
        points = pointManager.GetPoints();
        ShotTaken = true;
    }

    float Map( float num, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;

        float value = (num / oldRange) * newRange;
        return (newMin + value);
    }
}
