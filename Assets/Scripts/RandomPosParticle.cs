using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosParticle : MonoBehaviour {


	
	void Start () {
        StartCoroutine(WaitForRandomPos());
	}
	
	
	void Update () {
		
	}

    IEnumerator WaitForRandomPos()
    {
        yield return new WaitForSeconds(2);
        RandomPos();
    }

    private void RandomPos()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(4f, -13f));
        StartCoroutine(WaitForRandomPos());
    }

}
