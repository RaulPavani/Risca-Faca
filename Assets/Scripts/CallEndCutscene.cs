using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEndCutscene : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void ChangeScene()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().StartGame();
    }
}
