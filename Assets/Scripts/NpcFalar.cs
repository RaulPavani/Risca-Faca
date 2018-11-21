using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcFalar : MonoBehaviour {

    public GameObject textoFala;
    public Animator animTexto;
    public TextMesh texto;
    public string[] falasBR, falasEN;


    private Language languageManager;

	void Start () {
        GameObject.Find("LanguageController").GetComponent<Language>();
        animTexto.SetBool("playerPerto", false);
    }
	
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (languageManager.language == 1)
            {
                texto.text = falasBR[Random.Range(0, falasBR.Length)];
            }

            if(languageManager.language == 2)
            {
                texto.text = falasEN[Random.Range(0, falasEN.Length)];
            }
            animTexto.SetBool("playerPerto", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            animTexto.SetBool("playerPerto", false);
        }
    }
    

}
