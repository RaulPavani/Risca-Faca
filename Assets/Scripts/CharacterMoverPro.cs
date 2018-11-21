using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMoverPro : MonoBehaviour {

    public float velocidade;
    private float velocidadeAtual;
    private Rigidbody2D rb;
    private AudioSource aud;

    private Animator anim;

    private float hMovimento;
    private float yMovimento;

    private bool canWalk;
    private bool canArmed;
    private bool isArmed;
    private bool onCutscene;

    private bool canAttack, canShoot;
    public LayerMask enemiesLayer;
    public float damage;
    public GameObject attackPos;
    public float radiusAttack;
    public Transform shootPos;
    public GameObject projetil;
    public float projectileSpeed;
    public float timeShootDelay;

    public GameObject particulaFumaca;
    public Transform posicaoFumaca;
    private bool podeSoltarParticula;

    private bool canRotate = false;
    private bool isAttacking = false;

    private StageManager stageManager;

    public float maxLife = 20;
    public float currentLife;
    public Image lifeBar;
    private GameObject deadScreen;
    private bool dead = false;
    public ParticleSystem particleDead;
    private GameObject btnManager;

    public AudioClip WalkSound;
    public AudioClip Walk2Sound;
    public AudioClip AttackSound;
    public AudioClip ShootSound;
    public AudioClip OuchSound;

    private float globalVolumeSfx;


    void Start() {

        globalVolumeSfx = GameObject.Find("StageManager").GetComponent<StageManager>().volumeSfx;
        aud = GetComponent<AudioSource>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        deadScreen = GameObject.Find("DeathImage");
        btnManager = GameObject.Find("BtnManager");
        stageManager.GetPlayer(gameObject);

        var emission = particleDead.emission;
        emission.enabled = false;

        //currentLife = maxLife;
        GetLifeBar();
        currentLife = stageManager.currentLife;

        //timeShootDelay = 4f;
        podeSoltarParticula = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("isDead", false);

        canAttack = true;
        //canWalk = true;
        canShoot = true;
        canRotate = false;
        isArmed = false;


        if(SceneManager.GetActiveScene().name == "Cs_HouseStart")
        {
            canAttack = false;
            canWalk = false;
            canArmed = false;
            canShoot = false;
            canRotate = false;
            StartCoroutine(podeAndar(6.5f));

            currentLife = maxLife;
            
        }

        else if (SceneManager.GetActiveScene().name == "Cs_Boss1")
        {
            canAttack = false;
            canWalk = false;
            canArmed = false;
            canShoot = false;
            canRotate = false;
            StartCoroutine(podeAndar(7f));
        }
        else {
            canWalk = true;
            canAttack = true;
            canArmed = true;
            canShoot = true;
            canRotate = true;
        }
    



    }

    float lastHInput, lastYInput;

    void Update() {
       hMovimento = Input.GetAxisRaw("Horizontal");
       yMovimento = Input.GetAxisRaw("Vertical");


        if (canRotate && !onCutscene && !isAttacking)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                lastYInput = -1;
                anim.SetFloat("yInput", lastYInput);
                anim.SetFloat("xInput", 0);
            }

            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                lastYInput = 1;
                anim.SetFloat("yInput", lastYInput);
                anim.SetFloat("xInput", 0);
            }



            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                lastHInput = 1;
                anim.SetFloat("xInput", lastHInput);
                anim.SetFloat("yInput", 0);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                lastHInput = -1;
                anim.SetFloat("xInput", lastHInput);
                anim.SetFloat("yInput", 0);
            }
        }
     

        //if (canWalk)
        //{
        //    anim.SetFloat("yInput", lastYInput);
        //    anim.SetFloat("xInput", lastHInput);
        //}

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (canArmed && !onCutscene)
            {
                isArmed = true;
                velocidadeAtual = velocidade - 1;
                anim.SetBool("isArmed", isArmed);
            }
        }
        else
        {
            isArmed = false;
            velocidadeAtual = velocidade;
            anim.SetBool("isArmed", isArmed);
        }



        //--------------------------------------Barra de vida

        lifeBar.fillAmount = currentLife / maxLife;
        stageManager.GetCurrentLife(currentLife);

        if (dead)
        {
            transform.position = Vector3.Slerp(transform.position, Camera.main.transform.position, 3f * Time.deltaTime);
        }




        

    }

    void FixedUpdate()
    {

        //MOVIMENTO    
        if (hMovimento != 0 || yMovimento != 0)
        {
            Walk();
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isWalking", false);
       
            
        }

        //ATAQUE FACADA
        if (Input.GetKeyDown(KeyCode.Space) && !isArmed)
        {
            Attack();
        }
        //ATAQUE TIRO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }

    void Walk()
    {
        if (canWalk && !onCutscene)
        {
            if (hMovimento != 0 && yMovimento != 0)
            {
                rb.velocity = new Vector2(hMovimento, yMovimento).normalized * velocidadeAtual;
                anim.SetBool("isWalking", true);

            }
            else
            {
                rb.velocity = new Vector2(hMovimento * velocidadeAtual, yMovimento * velocidadeAtual);
                anim.SetBool("isWalking", true);
            }
            if (podeSoltarParticula)
            {
                GameObject part = (GameObject)Instantiate(particulaFumaca, posicaoFumaca.position, Quaternion.Euler(0,0,Random.Range(0,360)));
                Destroy(part, 0.5f);
                StartCoroutine(contadorParticula());
                podeSoltarParticula = false;
            }

        }

        

    }


    void Attack()
    {
        if (canAttack && !onCutscene) {

            isAttacking = true;
            anim.SetTrigger("isAttacking");
            canRotate = false;

            // rb.velocity = Vector3.zero; //parar de andar

            //Vector3 velocity = Vector3.zero;
            //velocity = rb.velocity;

            rb.velocity *= 1.5f; //ARRUMAR ISSO DEPOIS, BUG SUPER VELOCIDADE
     
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.transform.position, radiusAttack, enemiesLayer);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyBehabiour>().TakeDamage(damage);
            }

            canAttack = false;
            canWalk = false;
            
            StartCoroutine(WaitForDamage(0.1f));

        }

    }

    void Shoot()
    {
        if ((isArmed && canShoot) && !onCutscene)  //ANTES TAVA SEM CANATTACK
        {
            ShootSfx();
            GameObject projectile;

            projectile = (GameObject)Instantiate(projetil, shootPos.position, shootPos.rotation);
            Vector3 target = shootPos.position - transform.position;
            projectile.GetComponent<Rigidbody2D>().velocity = target.normalized * projectileSpeed;
            Destroy(projectile, 3);
            canShoot = false;
            StartCoroutine(timeReloadGun());

            
        }
    }


    IEnumerator WaitForDamage(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
        canWalk = true;
        canShoot = true;
        canRotate = true;
        //isAttacking = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, radiusAttack);
    }


    public void TriggerCanWalk() // trigger que a animação chama para voltar a andar
    {
        canWalk = true;
        isAttacking = false;
    }

    public void TriggerCanRotate() // trigger que a animação chama para não rodar tanto atacando
    {
        isAttacking = false;
    }



    public void TakeDamage(float damage)
    {
       
            currentLife -= damage;
            GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(backNormalColor());

            if (currentLife <= 0)
            {
            //animação morte
            gameObject.GetComponent<DepthObject>().Dead();
            Dead();
            //gameObject.SetActive(false);
                
                //Destroy(gameObject);
            }

        aud.clip = OuchSound;
        aud.Play();

    }


    private void Dead()
    {
        //Time.timeScale = 0;
        anim.SetBool("isDead", true);
        StartCoroutine(DeadWait());
        onCutscene = true;
        canWalk = false;
        canAttack = false;
        canArmed = false;
        canShoot = false;
        canRotate = false;
        anim.SetFloat("yInput", -1);
        anim.SetFloat("xInput", 0);
        var emission = particleDead.emission;
        emission.enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = false;

        //transform.position = Camera.main.transform.position
        deadScreen.GetComponent<Animator>().SetTrigger("Dead");
        lifeBar.gameObject.GetComponent<Animator>().SetTrigger("Dead");

        //aud.clip = DeadSound;
        //aud.Play();

    }

    IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(1.2f);
        btnManager.GetComponent<BtnManager>().ActiveBtns();
        dead = true;
    }

    public void RestartLife()
    {
        currentLife = maxLife;
        //restart balas da arma;
    }


    IEnumerator contadorParticula()
    {
        yield return new WaitForSeconds(0.35f);
        podeSoltarParticula = true;
    }
    
    IEnumerator podeAndar(float time)
    {
        anim.SetFloat("yInput", -1);
        anim.SetFloat("xInput", 0);
        yield return new WaitForSeconds(time);
        canWalk = true;
        canAttack = true;
        canArmed = true;
        canShoot = true;
        canRotate = true;
    }


    IEnumerator backNormalColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void WaitForCutscene(float time)
    {
        StartCoroutine(corWaitForCutscene(time));
    }

    public IEnumerator corWaitForCutscene(float time)
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isArmed", false);
        anim.SetFloat("yInput", lastYInput);
        anim.SetFloat("xInput", lastHInput);
        lastHInput = 0; 
        lastYInput = 0;

        rb.velocity = Vector3.zero;
        onCutscene = true;
        canWalk = false;
        canAttack = false;
        canArmed = false;
        canShoot = false;
        canRotate = false;
        yield return new WaitForSeconds(time);
        canWalk = true;
        canAttack = true;
        canArmed = true;
        canShoot = true;
        canRotate = true;
        onCutscene = false;
    }


    IEnumerator timeReloadGun()
    {
        yield return new WaitForSeconds(timeShootDelay);
        canShoot = true;
    }


    private void GetLifeBar()
    {
        lifeBar = GameObject.Find("BrraDeVida").GetComponent<Image>();
    }

    public void Pause()
    {
        canRotate = false;
        canArmed = false;
        canAttack = false;
    }

    public void Continue()
    {
        canRotate = true;
        canArmed = true;
        canAttack = true;
    }


    public void WalkSfx()
    {
        if (!isAttacking)
        {
            if (!aud.isPlaying)
            {
                aud.clip = WalkSound;
                if (isArmed)
                {
                    aud.volume = 0.2f;
                }
                else
                {
                    aud.volume = 0.3f;
                }
                aud.pitch = Random.Range(0.8f, 1.1f);
                aud.Play();
            }
        }
    }

    public void Walk2Sfx()
    {
        if (!isAttacking)
        {
            if (!aud.isPlaying)
            {
                aud.clip = Walk2Sound;
                if (isArmed)
                {
                    aud.volume = 0.2f;
                }
                else
                {
                    aud.volume = 0.3f;
                }
                aud.pitch = Random.Range(0.8f, 1.1f);
                aud.Play();
            }
        }
    }

    public void AttackSfx()
    {
        aud.Stop();
        aud.clip = AttackSound;
        aud.volume = 0.08f;
        aud.pitch = Random.Range(0.85f, 1.1f);
        aud.Play();
    }

    public void ShootSfx()
    {
        print("PROW");
        aud.clip = ShootSound;
        aud.volume = 0.3f;
        aud.pitch = Random.Range(0.9f, 1.1f);
        aud.Play();
    }

    public void OuchSfx()
    {
        aud.pitch = Random.Range(0.85f, 1.1f);
        aud.volume = 0.4f;
        aud.clip = OuchSound;
        aud.Play();
    }




}
