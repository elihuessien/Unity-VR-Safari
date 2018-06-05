using UnityEngine;

public class SavePoints : Saver {
    PointManager pm;
    WorldPoints wp;

    void Start()
    {
        pm = GetComponent<PointManager>();
        wp = GameObject.Find("SceneManager").GetComponent<WorldPoints>();
    }

    protected override void Save()
    {
        wp.setPoints(pm.points);
    }
}
