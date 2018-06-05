using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*All scene managment scripts were adapted from: 
 https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/game-state?playlist=44381*/
public abstract class Saver : MonoBehaviour {
    public SaveData saveData;
    private SceneController sceneController;

	// Use this for initialization
	private void Awake () {
        sceneController = FindObjectOfType<SceneController>();

        //if doesn't exist
        if (!sceneController)
            throw new UnityException(
                "Scene Controller could not be found, Please make sure it exists");
    }
	

	private void OnEnable () {
        sceneController.BeforeSceneUnload +=Save;
	}
    private void OnDisable()
    {
        sceneController.BeforeSceneUnload -= Save;
    }


    protected abstract void Save();
}
