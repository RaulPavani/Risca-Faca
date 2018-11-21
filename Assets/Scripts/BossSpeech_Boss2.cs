using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossSpeech_Boss2 : MonoBehaviour {

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
            textSpeech.GetComponent<TextMesh>().text = "ENTÃO É VOCÊ O CABRA QUE ANDA ATRÁS DOS CANGACERO";
            yield return new WaitForSeconds(waitForFirstSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "SE OCÊ TA QUERENO VINGANÇA, ESPERO QUE SEJA PELOS MOTIVO CERTO";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "POIS NÓIS CANGACERO SÓ MATAMO POR JUSTIÇA";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "VAMO VÊ SE OCÊ PASSA PELOS MEUS PARCERO!";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);
        }


        if (managerLangague.language == 2)
        {
            textSpeech.GetComponent<TextMesh>().text = "SO YOU ARE THE DUDE CHASING THE CANGACEIROS";
            yield return new WaitForSeconds(waitForFirstSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "IF YOU WANT REVENGE, I HOPE IT'S FOR THE RIGHT REASONS";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "BECAUSE WE ONLY KILL FOR JUSTICE";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "LET'S SEE IF YOU CAN GET THROUGH MY GANG!";
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);
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
        player.GetComponent<CharacterMoverPro>().WaitForCutscene(11.5f);

        if (managerLangague.language == 1)
        {
            textSpeech.GetComponent<TextMesh>().text = "PARECE QUE SUA VONTADE DE SE VINGA TE FEZ UM CABRA DA PESTE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "MAS NINGUÉM MATA MEU BANDO E NÃO SOFRE AS CONSEQUÊNCIA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "VAMO VE SE OCÊ AGUENTA MINHA PEXERA LIGERA";
            yield return new WaitForSeconds(1f);
        }

        if (managerLangague.language == 2)
        {
            textSpeech.GetComponent<TextMesh>().text = "LOOKS LIKE YOUR SEEK FOR REVENGE HAS MADE YOU A TOUGH DUDE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "BUT NO ONE KILLS MY GANG AND DON'T SUFFER THE CONSEQUENCES";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(waitBetweenSpeech);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "LETS SEE IF YOU CAN HANDLE MY SLIGHT KNIFE";
            yield return new WaitForSeconds(1f);
        }
           

            GameObject.Find("StageManager").GetComponent<StageManager>().music.Play();

        textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
        yield return new WaitForSeconds(3);
        newCamera.gameObject.SetActive(false);
        currentCamera.gameObject.SetActive(true);
        currentCamera.gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
        currentCamera.gameObject.GetComponent<Animator>().SetTrigger("FinalCutscene");
        textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        
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
        player.GetComponent<CharacterMoverPro>().WaitForCutscene(12.0f);

        if (managerLangague.language == 1)
        {
            textSpeech.GetComponent<TextMesh>().text = "VOCÊ DESTRUIU MEU BANDO, QUE ERAM MEUS ÚNICO AMIGO";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "SE ESSE É O TIPO DE VINGAÇA QUE OCÊ QUER...";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "VAI TE QUE ENCONTRA O BANDO DO DIABO LOIRO";

        }

        if (managerLangague.language == 2)
        {
            textSpeech.GetComponent<TextMesh>().text = "YOU DESTROYED MY GANG, MY ONLY FRIENDS";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "IF THIS IS THE REVENGE YOU WANT...";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "YOU GOTTA FIND THE DIABO LOIRO'S GANG";
        }

            yield return new WaitForSeconds(0.5f);
        //cam.GetComponent<GetPlayerCineMachine>().GetPlayer();
        textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
        yield return new WaitForSeconds(3);
        newCamera.gameObject.SetActive(false);
        currentCamera.gameObject.SetActive(true);
        currentCamera.gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
        textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        GameObject.Find("StageManager").GetComponent<StageManager>().lastBossUnlocked = true;
    }

}
