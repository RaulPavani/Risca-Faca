using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossSpeech_Boss1 : MonoBehaviour {

    public GameObject textSpeech;
    public float waitForFirstSpeech, waitBetweenSpeech;

    public GameObject[] enemys;
    private bool[] EnemyDead;
    private bool ThreeDead;

    private GameObject player;
    public GameObject cam;

    private bool isDead;

    private Language managerLangague;


    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;



    void Start () {

        managerLangague = GameObject.Find("LanguageController").GetComponent<Language>();

        gameObject.GetComponent<EnemyBehabiour>().CannotTakeDamage();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyDead = new bool[7];
        StartCoroutine(FirstSpeech());

        GameObject.Find("StageManager").GetComponent<StageManager>().music.Stop();

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyBehabiour>().CannotTakeDamage();
            //enemys[i].GetComponent<EnemyBehabiour>().enabled = false;
            enemys[i].GetComponent<Pathfinding.AIPath>().enabled = false;
            enemys[i].GetComponent<Rigidbody2D>().mass = 500000;
        }

        gameObject.GetComponent<EnemyBehabiour>().DesactiveHpBar();
        gameObject.GetComponent<Animator>().SetBool("isDead", false);


        GameObject.Find("StageManager").GetComponent<StageManager>().music.Play();

    }


    bool canSpeechFinal = true;

    void Update() {

       

        for (int i = 0; i < 7; i++)
        {
            if (enemys[i].GetComponent<EnemyBehabiour>().isDead)
            {
                EnemyDead[i] = true;
                //enemys[i].GetComponent<EnemyBehabiour>().enabled = false;
                enemys[i].GetComponent<Pathfinding.AIPath>().enabled = false;
                enemys[i].GetComponent<SpriteRenderer>().sortingOrder = -1000;
            }
        }

        if (EnemyDead[0] && EnemyDead[1] && EnemyDead[2])
        {
            ActiveLastEnemys();
        }

        if (EnemyDead[0] && EnemyDead[1] && EnemyDead[2] && EnemyDead[3] && EnemyDead[4] && EnemyDead[5] && EnemyDead[6]) //&& Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 10
        {
            if (canSpeechFinal)
            {
                StartCoroutine(FinalDialogue());
                canSpeechFinal = false;
            }
        }

        isDead = gameObject.GetComponent<EnemyBehabiour>().isDead;


        if (player.GetComponent<CharacterMoverPro>().currentLife <= 0){
            gameObject.GetComponent<EnemyBehabiour>().DesactiveHpBar();
        }

    }


        IEnumerator FirstSpeech()
    {
        if (managerLangague.language == 1)
        {
            textSpeech.GetComponent<TextMesh>().text = "OLHA O CABRA QUE CORREU";
            yield return new WaitForSeconds(waitForFirstSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "BORA ACABAR COM A RAÇA DELE";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            
        }

        if (managerLangague.language == 2) // INGLES
        {
            textSpeech.GetComponent<TextMesh>().text = "LOOK! THE DUDE WHO RAN AWAY";
            yield return new WaitForSeconds(waitForFirstSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "LETS KICK HIS ASS";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);

        }



        ActiveFirstEnemys();
    }


    private void ActiveFirstEnemys()
    {
        for (int i = 0; i < 3; i++)
        {
            enemys[i].GetComponent<EnemyBehabiour>().CanTakeDamage();
            enemys[i].GetComponent<EnemyBehabiour>().enabled = true;
            enemys[i].GetComponent<Pathfinding.AIPath>().enabled = true;
            enemys[i].GetComponent<EnemyBehabiour>().isLocked = false;
            enemys[i].GetComponent<Rigidbody2D>().mass = 1;
        }
    }

    private void ActiveLastEnemys()
    {
        for (int i = 3; i < 7; i++)
        {
            enemys[i].GetComponent<EnemyBehabiour>().CanTakeDamage();
            enemys[i].GetComponent<EnemyBehabiour>().enabled = true;
            enemys[i].GetComponent<Pathfinding.AIPath>().enabled = true;
            enemys[i].GetComponent<EnemyBehabiour>().isLocked = false;
            enemys[i].GetComponent<Rigidbody2D>().mass = 1;
        }
    }

    private void ActiveBoss()
    {
        if (!isDead)
        {
            gameObject.GetComponent<EnemyBehabiour>().enabled = true;
            gameObject.GetComponent<Pathfinding.AIPath>().enabled = true;
            gameObject.GetComponent<EnemyBehabiour>().CanTakeDamage();
            gameObject.GetComponent<EnemyBehabiour>().ActiveHpBar();
            gameObject.GetComponent<EnemyBehabiour>().isLocked = false;
        }
    }


    IEnumerator FinalDialogue()
    {
        myBrain.m_DefaultBlend = someCustomBlend;
        newCamera.gameObject.SetActive(true);
        currentCamera.gameObject.SetActive(false);
        //yield return new WaitForSeconds(1);
        //GameObject cam = GameObject.Find("CM vcam1");
        //cam.GetComponent<GetPlayerCineMachine>().LosePlayer();
        player.GetComponent<CharacterMoverPro>().WaitForCutscene(6.5f);

        if (managerLangague.language == 1)
        {

            textSpeech.GetComponent<TextMesh>().text = "CÊ DERROTO ESSES BUNDA MOLE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "VAMO RESOLVE ISSO NA PEXERA";
            yield return new WaitForSeconds(0.5f);
            //cam.GetComponent<GetPlayerCineMachine>().GetPlayer();
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);

            GameObject.Find("StageManager").GetComponent<StageManager>().music.Play();

            yield return new WaitForSeconds(3);
            newCamera.gameObject.SetActive(false);
            currentCamera.gameObject.SetActive(true);
            currentCamera.gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
            currentCamera.gameObject.GetComponent<Animator>().SetTrigger("FinalCutscene");
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        }

 if (managerLangague.language == 2)
 {
     textSpeech.GetComponent<TextMesh>().text = "YOU ENDED THESE BUMS";
     yield return new WaitForSeconds(1);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
     yield return new WaitForSeconds(2);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
     yield return new WaitForSeconds(1);

     textSpeech.GetComponent<TextMesh>().text = "LETS SOLVE THIS WITH KNIVES";
     yield return new WaitForSeconds(0.5f);
     //cam.GetComponent<GetPlayerCineMachine>().GetPlayer();
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);

     GameObject.Find("StageManager").GetComponent<StageManager>().music.Play();

     yield return new WaitForSeconds(3);
     newCamera.gameObject.SetActive(false);
     currentCamera.gameObject.SetActive(true);
     currentCamera.gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
     currentCamera.gameObject.GetComponent<Animator>().SetTrigger("FinalCutscene");
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
 }

     ActiveBoss();
 gameObject.GetComponent<EnemyBehabiour>().isLocked = false;
}

public void TriggerDeadDialogue()
{
 StartCoroutine(DeadDialogue());
}

IEnumerator DeadDialogue()
{
 gameObject.GetComponent<SpriteRenderer>().sortingOrder = -900;
 myBrain.m_DefaultBlend = someCustomBlend;
 newCamera.gameObject.SetActive(true);
 currentCamera.gameObject.SetActive(false);
 newCamera.gameObject.GetComponent<Animator>().SetBool("isDead", true);
 gameObject.gameObject.GetComponent<EnemyBehabiour>().DesactiveHpBar();
 player.GetComponent<CharacterMoverPro>().WaitForCutscene(11.5f);

 if (managerLangague.language == 1)
 {
     textSpeech.GetComponent<TextMesh>().text = "SEU MALDITO, CÊ DEU SORTE";
     yield return new WaitForSeconds(1);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
     yield return new WaitForSeconds(2);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
     yield return new WaitForSeconds(1);

     textSpeech.GetComponent<TextMesh>().text = "CÊ DERROTOU MEUS CABRA";
     yield return new WaitForSeconds(1);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
     yield return new WaitForSeconds(2);
     textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
     yield return new WaitForSeconds(1);

     textSpeech.GetComponent<TextMesh>().text = "MAS QUERO VER DERROTAR O BANDO DA PINGA FOGO";
 }


if (managerLangague.language == 2)
{
    textSpeech.GetComponent<TextMesh>().text = "YOU IDIOT, YOU WERE LUCKY";
    yield return new WaitForSeconds(1);
    textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
    yield return new WaitForSeconds(2);
    textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
    yield return new WaitForSeconds(1);

    textSpeech.GetComponent<TextMesh>().text = "YOU ENDED MY GANG";
    yield return new WaitForSeconds(1);
    textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
    yield return new WaitForSeconds(2);
    textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
    yield return new WaitForSeconds(1);

    textSpeech.GetComponent<TextMesh>().text = "BUT I BET YOU CAN'T DEFEAT THE PINGA FOGOS'S GANG";
}

    yield return new WaitForSeconds(0.5f);
//cam.GetComponent<GetPlayerCineMachine>().GetPlayer();
textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
yield return new WaitForSeconds(3);
newCamera.gameObject.SetActive(false);
currentCamera.gameObject.SetActive(true);
currentCamera.gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
}

}
