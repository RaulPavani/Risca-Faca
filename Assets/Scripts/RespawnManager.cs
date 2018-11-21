using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RespawnManager : MonoBehaviour {


  
	public string m_NextSceneName;
    public Vector3 m_LocationRespawn;

    [SerializeField]
    private Animator m_ImgAnim;
    [SerializeField]
    private Image m_TransitionImage;



    void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
            StartCoroutine(FadeIn());
		}
	}


    IEnumerator FadeIn()
    {
        m_ImgAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => m_TransitionImage.color.a == 1);
    
        StageManager.instance.ChangeScene(m_NextSceneName, m_LocationRespawn);
        
    }

}
