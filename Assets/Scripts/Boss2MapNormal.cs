using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2MapNormal : MonoBehaviour {

	void Start () {
        if (GameObject.Find("StageManager").GetComponent<StageManager>().lastBossUnlocked)
        {
            gameObject.GetComponent<RespawnManager>().m_NextSceneName = "Boss2";
        }

    }
	
	void Update () {
		
	}
}
