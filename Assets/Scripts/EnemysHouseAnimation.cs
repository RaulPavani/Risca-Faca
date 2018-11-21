using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysHouseAnimation : MonoBehaviour {

	public float velocidade;
	private Rigidbody2D rb;

	private Animator anim;
	public float waitForStart;

	private GetPlayerCineMachine camOnPlayer;

    public bool isBoss;


	public GameObject textoFala;


	void Start () {
		if (textoFala) {
			textoFala.SetActive (false);
		}
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		StartCoroutine (waitForSeconds (waitForStart));
		StartCoroutine (Speech());
	}
	

	void Update () {
		if (transform.position.x > 26.5f) {
			gameObject.SetActive (false);
		}
	}


	IEnumerator waitForSeconds(float seconds){
		yield return new WaitForSeconds (seconds);
		anim.SetFloat("xInput", 1);
        if(isBoss)
		    anim.SetBool("isWalkingCs", true);
        else{
            anim.SetBool("isWalking", true);
        }
		rb.velocity = Vector2.right * velocidade;
		yield return new WaitForSeconds (2);
	}

	IEnumerator Speech(){
		yield return new WaitForSeconds (0.3f);
		if (textoFala) {
			textoFala.SetActive (true);
			textoFala.GetComponent<Animator> ().SetBool ("playerPerto", true);

		}

	}

}
