using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLayer : MonoBehaviour {


	void Start () {
		gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Default";
		gameObject.GetComponent<MeshRenderer> ().sortingOrder = 1000;
	}
	

	void Update () {
		
	}
}
