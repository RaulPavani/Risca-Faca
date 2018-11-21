using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour {


    public Animator animImage;

    public void CutsceneFinalMatou()
    {
        animImage.SetTrigger("Matou");
    }

    public void CutsceneFinalJuntou()
    {
        animImage.SetTrigger("Juntou");
    }

}
