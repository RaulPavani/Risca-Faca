using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

	public static StageManager instance = null;
    public AudioSource music;

    public AudioClip musicNormal;
    public AudioClip musicBoss;

	public GameObject m_Player;

    public Vector3 InstantiatePos;

    public float currentLife;

    public bool lastBossUnlocked;


    public float volumeSfx, volumeMusica;
    private Slider sliderSfx, sliderMusica;

    public void FPlayer()
    {
        Pathfinding.TargetMover targetM = m_Player.GetComponent<Pathfinding.TargetMover>();
        targetM.FindPlayer();
    }


    void Start(){


        lastBossUnlocked = false;


        currentLife = 20;

        //InstantiatePlayer(new Vector3(-11f, -6f, 0f));
        //Instantiate(m_Player, new Vector2(-11f, -6f), Quaternion.identity);



        if (instance == null) {
			instance = this;
		}else if(instance != null){
			Destroy (gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
		//DontDestroyOnLoad (m_Player);

	}

    private void Update()
    {

    }

    public void ChangeScene(string nextScene, Vector3 respawnLocation){
		SceneManager.LoadScene (nextScene);
        InstantiatePos = respawnLocation;

         //InstantiatePlayer(respawnLocation);
         m_Player = GameObject.FindGameObjectWithTag("Player");


        //StartCoroutine(waitForSeconds(1));
        //m_Player.transform.position = respawnLocation;
        

        string lastSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("lastScene", lastSceneName);
        PlayerPrefs.Save();

        if(nextScene == "Cs_Boss1" || nextScene == "Cs_Boss2" || nextScene == "Cs_FinalBoss")
        {
            music.clip = musicBoss;
            music.volume = 0.4f;
            music.Play();
        }
        else
        {
            music.clip = musicNormal;
            music.volume = 0.3f;
            music.Play();
        }

	}


	IEnumerator waitForSeconds(float seconds){
		yield return new WaitForSeconds (seconds);
	}


    private void InstantiatePlayer(Vector3 respPos)
    {
        Instantiate(m_Player, respPos, Quaternion.identity);
    }

    public void GetCurrentLife(float curLife)
    {
        currentLife = curLife;
    }

    public void RestartLife()
    {
        currentLife = 20;
        //restart balas da arma;
    }


    public void GetVol(Slider sfx, Slider musica)
    {
        sliderSfx = sfx;
        sliderMusica = musica;
    }

    public void GetPlayer(GameObject player)
    {
        m_Player = player;
    }

}
