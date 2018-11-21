using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class BossSpeech_Boss3 : MonoBehaviour {

    public GameObject textSpeech;
    public float waitForFirstSpeech, waitBetweenSpeech;

    public GameObject[] enemys;
    private bool[] EnemyDead;
    private bool ThreeDead;

    private GameObject player;
    public GameObject cam;

    private bool isDead;

    public Button btnMatar;
    public Button btnDeixarViver;


    private Language managerLangague;


    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;



    void Start () {
        managerLangague = GameObject.Find("LanguageController").GetComponent<Language>();
        gameObject.GetComponent<EnemyBehabiour>().CannotTakeDamage();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyDead = new bool[9];
        StartCoroutine(FirstSpeech());

        GameObject.Find("StageManager").GetComponent<StageManager>().music.Stop();

        btnMatar.gameObject.SetActive(false);
        btnDeixarViver.gameObject.SetActive(false);

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


        for (int i = 0; i < 9; i++)
        {
            if (enemys[i].GetComponent<EnemyBehabiour>().isDead)
            {
                EnemyDead[i] = true;
                //enemys[i].GetComponent<EnemyBehabiour>().enabled = false;
                enemys[i].GetComponent<Pathfinding.AIPath>().enabled = false;
                enemys[i].GetComponent<SpriteRenderer>().sortingOrder = -1000;
            }
        }

        if (EnemyDead[0] && EnemyDead[1] && EnemyDead[2] && EnemyDead[3])
        {
            ActiveLastEnemys();
        }

        if (EnemyDead[0] && EnemyDead[1] && EnemyDead[2] && EnemyDead[3] && EnemyDead[4] && EnemyDead[5] && EnemyDead[6] && EnemyDead[7] && EnemyDead[8]) //&& Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 10
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
            textSpeech.GetComponent<TextMesh>().text = "ENTÃO O CABRA DA PESTE APARECEU, OUVI DIZE QUE OCÊ TA ATRÁS DE SE VINGA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "E UM CABRA QUANDO QUER VINGANÇA FICA CEGO";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = " É LUTAR OU MORRER, VAMO PRA LUTA MEUS PARCERO!";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        }

        if (managerLangague.language == 2)
        {
            textSpeech.GetComponent<TextMesh>().text = "SO THE GUY SHOWED UP, I HEARD YOU ARE LOOKING FOR REVENGE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "AND A MAN WHO WANTS REVENGE, GETS BLIND";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "IT'S FIGHT OR DEATH, LETS GO FIGHT MY MEN!";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(2);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        }

            ActiveFirstEnemys();
    }


    private void ActiveFirstEnemys()
    {
        for (int i = 0; i < 4; i++)
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
        for (int i = 4; i < 9; i++)
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
        player.GetComponent<CharacterMoverPro>().WaitForCutscene(5.5f);

        if (managerLangague.language == 1)
        {
            textSpeech.GetComponent<TextMesh>().text = "AGORA SÓ RESTA EU, VAMO VE SE OCÊ É UM CABRA BÃO DE VERDADE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        }

        if (managerLangague.language == 2)
        {
            textSpeech.GetComponent<TextMesh>().text = "NOW THERE'S ONLY ME, LETS SEE IF YOU ARE A REAL TOUGH DUDE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(3);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        }

            GameObject.Find("StageManager").GetComponent<StageManager>().music.Play();

        //cam.GetComponent<GetPlayerCineMachine>().GetPlayer();
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
        player.GetComponent<CharacterMoverPro>().WaitForCutscene(11.5f);
        if (managerLangague.language == 1)
        {

            textSpeech.GetComponent<TextMesh>().text = "VOCÊ QUERIA TANTO SE VINGA QUE FICO CEGO PELA SUA RAIVA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "FOI TUDO UM ENGANO, NÓIS CANGACERO NUNCA\nIRIA MATA CABRA NENHUM SE NÃO FOSSE POR JUSTIÇA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(5);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "MAS AS VEZ NO MEIO DA LUTA NOIS ACABA COMETENO\nALGUNS ERRO SEM NEM SI DA CONTA,\n PRA PROTEJE NOSSOS COMPANHERO";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "TUDO QUE NÓIS PROCURA É JUSTIÇA\nNOSSOS BANDO SÃO NOSSAS FAMÍLIA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "E VOCÊ MATOU TODOS OS BANDO QUE ENCONTRO NO SEU CAMINHO";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(5);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "SE TORNO AQUILO QUE MAIS ODIAVA,\nUM DE NÓIS, FEZ SUA PRÓPRIA JUSTIÇA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "UM CABRA COMO OCÊ PODERIA SE JUNTA A UM BANDO\n, PRA ACHA UM NOVO RUMO PRA SUA VIDA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "SE OCÊ POUPAR MINHA VIDA AGORA\nPOSSO TE AJUDA";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "O QUE OCÊ VAI FAZE AFINAL?";
            yield return new WaitForSeconds(1);

        }

        if (managerLangague.language == 2)
        {

            textSpeech.GetComponent<TextMesh>().text = "YOU WANTED SO MUCH TO TAKE REVENGE, THAT YOU GOT BLIND BY YOUR OWN ANGER";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "BUT IT WAS ALL A BIG MISTAKE, WE, CANGACEIROS\nWOULD NEVER KILL ANYONE IF IT WASN'T FOR JUSTICE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(5);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "BUT SOMETIMES IN THE MIDDLE OF THE FIGHT, WE END UP MAKING\nMISTAKES WITHOUT EVEN NOTICING, TO PROTECT OUR PARTNERS";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "ALL THAT WE ARE LOOKING FOR, IT'S JUSTICE,\nOUR GANG IS OUR FAMILY";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "AND YOU KILLED EVERY GANG THAT CROSSED YOUR PATH";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(5);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "YOU BECAME THAT THING YOU HATED THE MOST,\nONE OF US, TRYING TO MAKE YOUR OWN JUSTICE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "A GUY LIKE YOU COULD JOIN A GANG,\nTO FIND A NEW PURPOSE FOR YOUR LIFE";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(6);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "IF YOU HAVE MERCY ON ME, I CAN HELP YOU";
            yield return new WaitForSeconds(1);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
            yield return new WaitForSeconds(4);
            textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
            yield return new WaitForSeconds(1);

            textSpeech.GetComponent<TextMesh>().text = "WHAT YOU GONNA DO?";
            yield return new WaitForSeconds(1);

        }


            btnMatar.gameObject.SetActive(true);
        btnDeixarViver.gameObject.SetActive(true);

        textSpeech.GetComponent<Animator>().SetBool("playerPerto", true);
        yield return new WaitForSeconds(2);
        textSpeech.GetComponent<Animator>().SetBool("playerPerto", false);
        yield return new WaitForSeconds(1);




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
