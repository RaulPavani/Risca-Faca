using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boudLayout : MonoBehaviour {

	private SpriteRenderer sprite;
	private EdgeCollider2D col;
	private List<Vector2> newVertices = new List<Vector2>(); 

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		col = GetComponent<EdgeCollider2D> ();

		Vector2 cantoSuperiorDireito = new Vector2 (transform.position.x - (sprite.size.x / 2), transform.position.y + sprite.size.y / 2);
		Vector2 cantoInferiorDireito = new Vector2 (transform.position.x - (sprite.size.x / 2), transform.position.y - sprite.size.y / 2);
		Vector2 cantoInferiorEsquerdo = new Vector2 (transform.position.x + (sprite.size.x / 2), transform.position.y - sprite.size.y / 2);
		Vector2 cantoSuperiorEsquerdo = new Vector2 (transform.position.x + (sprite.size.x / 2), transform.position.y + sprite.size.y / 2);

		col.points = new Vector2[5] {
			cantoSuperiorDireito,
			cantoInferiorDireito,
			cantoInferiorEsquerdo,
			cantoSuperiorEsquerdo,
			cantoSuperiorDireito
		};


//
//		col.points [0] = new Vector2 (transform.position.x - (sprite.size.x / 2), transform.position.y + sprite.size.y / 2);
//		col.points [1] = new Vector2 (transform.position.x - (sprite.size.x / 2), transform.position.y - sprite.size.y / 2);
//		col.points [2] = new Vector2 (transform.position.x + (sprite.size.x / 2), transform.position.y - sprite.size.y / 2);
//		col.points [3] = new Vector2 (transform.position.x + (sprite.size.x / 2), transform.position.y + sprite.size.y / 2);
//		col.points [4] = new Vector2 (transform.position.x - (sprite.size.x / 2), transform.position.y + sprite.size.y / 2);
////
//		newVertices.Add (new Vector2 (transform.position.x - (sprite.size.x/2), transform.position.y + sprite.size.y / 2));
//		newVertices.Add (new Vector2 (transform.position.x - (sprite.size.x/2), transform.position.y - sprite.size.y / 2));
//		newVertices.Add (new Vector2 (transform.position.x + (sprite.size.x/2), transform.position.y - sprite.size.y / 2));
//		newVertices.Add (new Vector2 (transform.position.x + (sprite.size.x/2), transform.position.y + sprite.size.y / 2));
//		newVertices.Add (new Vector2 (transform.position.x - (sprite.size.x/2), transform.position.y + sprite.size.y / 2));

//		col.points[0] = newVertices [0]; 
//		col.points[1] = newVertices [1]; 
//		col.points[2] = newVertices [2]; 
//		col.points[3] = newVertices [3]; 
//		col.points[4] = newVertices [4]; 
	}
	

	void Update () {
		
	}
}
