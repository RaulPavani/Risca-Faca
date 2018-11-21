using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float damage;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyBehabiour>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (CompareTag("Obstacle"))
        {
            //ISTANCIAR FUMACINHA
            Destroy(gameObject);
        }
    }

}
