using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour {

    public GameObject btnMenu, btnRestart;
    private GameObject stageManager;
    private GameObject player;

    public string nameSceneReload;
    public Vector3 reloadPlace;

    public GameObject btnMatar, btnJuntar;
    

	void Start () {
        btnMenu.SetActive(false);
        btnRestart.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        stageManager = GameObject.Find("StageManager");
    }
	
	void Update () {
		
	}


    public void ActiveBtns()
    {
        btnMenu.SetActive(true);
        btnRestart.SetActive(true);
    }


    public void GoToMenu()
    {
        
        stageManager.GetComponent<StageManager>().ChangeScene("Menu", Vector3.zero);
    }


    public void Restart()
    {
        player.GetComponent<CharacterMoverPro>().RestartLife();
        stageManager.GetComponent<StageManager>().ChangeScene(nameSceneReload, reloadPlace);
    }




    public void Matar()
    {
        GameObject.Find("AnimTrigger").GetComponent<AnimTrigger>().CutsceneFinalMatou();
        btnJuntar.SetActive(false);
        btnMatar.SetActive(false);
    }



    public void SeJuntar()
    {
        GameObject.Find("AnimTrigger").GetComponent<AnimTrigger>().CutsceneFinalJuntou();
        btnJuntar.SetActive(false);
        btnMatar.SetActive(false);
    }

}
