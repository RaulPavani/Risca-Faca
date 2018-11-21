using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshTextLanguage : MonoBehaviour {
    //TEXTO UI
    public string textoBr;
    public string textoEn;

    public TextMesh textoAtual;

    private Language lenControl;

	void Start () {
        
        lenControl = GameObject.Find("LanguageController").GetComponent<Language>();
	}
	
	void Update () {
		if(lenControl.language == 1)
        {
           // textoAtual.text = textoBr;
        }

        if(lenControl.language == 2){
            //textoAtual.text = textoEn;
        }
	}
}
