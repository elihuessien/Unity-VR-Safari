using UnityEngine;
using TMPro;

public class UpdatePointsText : MonoBehaviour {

    TextMeshProUGUI pointsText;
	// Use this for initialization
	void Start () {
        WorldPoints savedPoints = GameObject.Find("SceneManager").GetComponent<WorldPoints>();
        int points = savedPoints.getPoints();

        pointsText = GetComponent<TextMeshProUGUI>();
        pointsText.text = "You got " +points + " Points!";

        savedPoints.resetPoints();
    }
}
