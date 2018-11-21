using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTransition : MonoBehaviour {

    [SerializeField]
    private GameObject image;

	void Start () {
        image.SetActive(true);
	}
	
	
	void Update () {
		
	}
}
