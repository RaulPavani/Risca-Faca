using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthObject : MonoBehaviour {

    private bool canChangeDepth;
    private SpriteRenderer spriteRend;

	
	void Start () {
       canChangeDepth = true;
       spriteRend = GetComponent<SpriteRenderer>();
    }
	
	
	void Update () {
        
        if(canChangeDepth)
            spriteRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(this.transform.position).y * -1;

        
    }

    public void Dead()
    {
        canChangeDepth = false;
        spriteRend.sortingOrder = 6000;
    }

}
