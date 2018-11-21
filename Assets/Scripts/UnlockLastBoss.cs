using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLastBoss : MonoBehaviour {

	void Start () {
        if (GameObject.Find("StageManager").GetComponent<StageManager>().lastBossUnlocked)
        {
            gameObject.SetActive(false);
        }
	}
	
	void Update () {
		
	}
}
