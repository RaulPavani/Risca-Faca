using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageBtn : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}


    public void LanguageToPt()
    {
        GameObject.Find("LanguageController").GetComponent<Language>().language = 1;
    }

    public void LanguageToEn()
    {
        GameObject.Find("LanguageController").GetComponent<Language>().language = 2;
    }

}
