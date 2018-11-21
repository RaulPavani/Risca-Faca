using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayerStart : MonoBehaviour {

    public GameObject player;

    public Vector3 insPos;
    
    
	
	void Start () {
        insPos = GameObject.Find("StageManager").GetComponent<StageManager>().InstantiatePos;
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        Instantiate(player, insPos, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
