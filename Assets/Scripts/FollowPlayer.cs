using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("animTutorial");
        }
    }
}
