using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLanguage : MonoBehaviour {


    public Language managerLangague;
    private Image sprit;

    public Sprite imgBr, imgEn;


    void Start () {
        sprit = GetComponent<Image>();
        managerLangague = GameObject.Find("LanguageController").GetComponent<Language>();
        
    }


    void Update () {
        
        if (managerLangague.language == 1)
        {
            sprit.sprite = imgBr;
        }
        if (managerLangague.language == 2)
        {
            sprit.sprite = imgEn;
        }
    }
}
