using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForActiveEnemy : MonoBehaviour {


    public float timeWait;

    public GameObject[] enemys;
	
	void Start () {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
        }
        
        StartCoroutine(WaitForActive(timeWait));
	}
	
	
	void Update () {
		
	}

    IEnumerator WaitForActive(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
        }
        
    }

}
