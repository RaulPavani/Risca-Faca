using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    private bool isPaused;

    public Image pauseImage;
    private GameObject player;
    public GameObject btnContinue, btnMenu;

    // public Slider musicaVol, sfxVol;

    void Start () {

        if (SceneManager.GetActiveScene().name == "Cs_HouseStart")
        {
            Time.timeScale = 1.2f;
        }

        // GameObject.Find("StageManager").GetComponent<StageManager>().GetVol(sfxVol, musicaVol);
        //musicaVol.value = GameObject.Find("StageManager").GetComponent<StageManager>().volumeMusica;
        // sfxVol.value = GameObject.Find("StageManager").GetComponent<StageManager>().volumeSfx;
        isPaused = false;
        pauseImage.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        btnContinue.SetActive(false);
        btnMenu.SetActive(false);
        //musicaVol.gameObject.SetActive(false);
        // sfxVol.gameObject.SetActive(false);
    }

    void Update () {
		if(Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape)){
            if (Time.timeScale == 0)
            {
                Contiue();                
            }
            else
            {
                Paused();
            }
        }
	}

    public void Contiue()
    {
        player.GetComponent<CharacterMoverPro>().Continue();
        pauseImage.gameObject.SetActive(false);
        Time.timeScale = 1.2f;
        btnContinue.SetActive(false);
        btnMenu.SetActive(false);
       // musicaVol.gameObject.SetActive(false);
        //sfxVol.gameObject.SetActive(false);
    }

    public void Paused()
    {
        player.GetComponent<CharacterMoverPro>().Pause();
        pauseImage.gameObject.SetActive(true);
        Time.timeScale = 0;
        btnContinue.SetActive(true);
        btnMenu.SetActive(true);
        //musicaVol.gameObject.SetActive(true);
        //sfxVol.gameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }


}
