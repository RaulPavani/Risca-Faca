using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlhaParaPlayer : MonoBehaviour {

    private GameObject player;
	
	void Start () {
        player = GameObject.FindWithTag("Player");
    }
	
	
	void Update () {
        transform.LookAt(-player.transform.position);

	}
}
