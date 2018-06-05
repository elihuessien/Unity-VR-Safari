using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour {
    GameObject menu;
    int counter;
    float interval;
    string currentSceneName;

    bool sceneChanging = false;


    // Use this for initialization
    void Start () {
        menu = GameObject.Find("Menu");
        counter = 0;

        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        currentSceneName = currentScene.name;
        

        //set the first menu option active
        menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 1;
        interval = 1;
    }
	
	// Update is called once per frame
	void Update () {
        interval -= Time.deltaTime;

        //if there is no next menu then load next Scene
        if (counter+1  == menu.transform.childCount && interval < 0 && !sceneChanging)
        {  
            if (currentSceneName == "Start")//start options
            {
                if(Input.GetAxisRaw("Fire1") > 0)
                {
                    sceneChanging = true;
                    SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
                    sc.FadeAndLoadScene("Main");
                }
            }
            else //end options
            {
                if(Input.GetAxisRaw("Fire1") > 0)
                {
                    sceneChanging = true;
                    SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
                    sc.FadeAndLoadScene("Main");
                }
                else if (Input.GetAxisRaw("Cancel") > 0)
                {
                    Debug.Log("Quitting!");
                    Application.Quit();
                }
            }
        }
        else if (Input.GetAxisRaw("Fire1") > 0 && interval < 0 && !sceneChanging) //remove current menu and bring up the next one
        {
            interval = 0.5f;
            menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 0;
            counter++;
            menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (Input.GetAxisRaw("Cancel") > 0 && interval < 0 && !sceneChanging && counter > 1)
        {
            interval = 0.5f;
            menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 0;
            counter--;
            menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 1;
        }
	}
}
