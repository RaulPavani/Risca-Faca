using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehabiour : MonoBehaviour {

	enum StateBehaviour{Seek};

	StateBehaviour currentState;

	public float velocidade;
	private Rigidbody2D rb;
    private AudioSource aud;

	private Animator anim;

	private float xMovimento;
	private float yMovimento;

	private GameObject playerTarget;

    public float maxLife;
    private float life = 10;//
    public bool isDead;
    public Image lifeBar;
    public Image lifeBarBg;

    private bool canTakeDamage;
    private bool canAttack;
    public float damage;
    public float waitForAttack;
    public GameObject attackPos;
    public LayerMask playerLayer;
    public float radiusAttack;

    public bool isBoss;
    public int bossNumber;
    public GameObject[] cantNExtLevel;

    public bool isLocked;


    public AudioClip WalkSound;
    public AudioClip Walk2Sound;
    public AudioClip AttackSound;
    public AudioClip ShootSound;
    public AudioClip OuchSound;

    void Start () {
        isLocked = true;
        life = maxLife;
        canAttack = true;
        rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
        aud = GetComponent<AudioSource>();

        anim.SetBool("isDead", false);

        playerTarget = GameObject.FindWithTag ("Player");

		currentState = StateBehaviour.Seek;
	}
	

	void Update () {

		AnimController ();

		if (currentState == StateBehaviour.Seek) {
			Seek ();
		}


        if(Vector3.Distance(transform.position, playerTarget.transform.position) < 2)
        {

            Attack();

        }




        //if (life <= 0)
        
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        

        if(isBoss)
            lifeBar.fillAmount = life / maxLife;


        if (isLocked)
        {
            xMovimento = 0;
            yMovimento = -1;
        }




        if (isDead)
        {
            rb.velocity = Vector3.zero;
        }

    }


	void Seek(){
        if (!isLocked)
        {
            anim.SetBool("isWalking", true);
        }
		///Vector3 desiredVelocity = playerTarget.transform.position - transform.position;
        //rb.velocity = Vector3.Normalize(desiredVelocity) * velocidade;

	}

    void AnimController()
    {

        if (!isLocked)
        {

            if (transform.position.y < playerTarget.transform.position.y)
            {
                yMovimento = 1;
            }
            else
            {
                yMovimento = -1;
            }

            if (transform.position.x < playerTarget.transform.position.x)
            {

                xMovimento = 1;

                if (transform.position.y + 3f >= playerTarget.transform.position.y)
                {
                    yMovimento = 0;
                }

            }
            else
            {

                xMovimento = -1;

                if (transform.position.y - 3f <= playerTarget.transform.position.y)
                {
                    yMovimento = 0;
                }

            }
            

        }

        anim.SetFloat("yInput", yMovimento);
        anim.SetFloat("xInput", xMovimento);

    }


    public void TakeDamage(float damage)
    {
        if (!isLocked)
        {
            if (canTakeDamage)
            {
                OuchSfx();
                life -= damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(backNormalColor());
                if (life <= 0)
                {
                    Dead();
                }
            }

            //TRIGGA ANIM MORTE
            yMovimento = 0;
            xMovimento = 0;

        }
    }


    private void Dead()
    {
        if (!isLocked)
        {
            if (gameObject.GetComponent<DepthObject>())
            {
                gameObject.GetComponent<DepthObject>().enabled = false;
            }
            if (isBoss)
            {
                foreach (GameObject obstacle in cantNExtLevel)
                {
                    obstacle.SetActive(false);
                }
                if (bossNumber == 1)
                {
                    GetComponent<BossSpeech_Boss1>().TriggerDeadDialogue();
                }

                if (bossNumber == 2)
                {
                    GetComponent<BossSpeech_Boss2>().TriggerDeadDialogue();
                }

                if (bossNumber == 3)
                {
                    GetComponent<BossSpeech_Boss3>().TriggerDeadDialogue();
                }
                canAttack = false;
                gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;
            }

            //Destroy(gameObject);
            var col = gameObject.GetComponents<Collider2D>();
            foreach (var item in col)
            {
                item.enabled = false;
            }
            anim.SetBool("isDead", true);
            isDead = true;
            
        }
    }


    private void Attack()
    {
        if (canAttack && !isDead)
        {
            anim.SetTrigger("isAttacking");
            Collider2D colPlayer = Physics2D.OverlapCircle(attackPos.transform.position, radiusAttack, playerLayer);
            if (colPlayer)
            {
                colPlayer.GetComponent<CharacterMoverPro>().TakeDamage(damage);
            }
            canAttack = false;
            StartCoroutine(WaitForAttack());
        }
    }


    IEnumerator backNormalColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void CanTakeDamage()
    {
        canTakeDamage = true;
    }


    public void CannotTakeDamage()
    {
        canTakeDamage = false;
    }


    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(waitForAttack);
        canAttack = true;
    }



    public void ActiveHpBar()
    {
        if (isBoss)
        {
            lifeBar.gameObject.SetActive(true);
            lifeBarBg.gameObject.SetActive(true);
        }
    }

    public void DesactiveHpBar()
    {
        if (isBoss)
        {
            lifeBarBg.gameObject.GetComponent<Animator>().SetBool("isDead", true);
            lifeBar.gameObject.GetComponent<Animator>().SetBool("isDead", true);
        }
    }
    



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, radiusAttack);
    }








    public void WalkSfx()
    {
        if (WalkSound)
        {
            // if (!isAttacking)
            //{
            if (!aud.isPlaying)
            {
                aud.clip = WalkSound;
                aud.volume = 0.3f;

                aud.pitch = Random.Range(0.8f, 1.1f);
                aud.Play();
            }
        }
        //}
    }

    public void Walk2Sfx()
    {
        // if (!isAttacking)
        if (Walk2Sound)
        {
            if (!aud.isPlaying)
            {
                aud.clip = Walk2Sound;
                aud.volume = 0.3f;

                aud.pitch = Random.Range(0.8f, 1.1f);
                aud.Play();
            }
        }
        //}
    }

    public void AttackSfx()
    {
        if (AttackSound)
        {
            aud.Stop();
            aud.clip = AttackSound;
            aud.volume = 0.3f;
            aud.pitch = Random.Range(0.85f, 1.1f);
            aud.Play();
        }
    }

    public void ShootSfx()
    {
        if (ShootSound)
        {
            aud.clip = ShootSound;
            aud.pitch = Random.Range(0.9f, 1.1f);
            aud.Play();
        }
    }

    public void OuchSfx()
    {
        if (OuchSound)
        {
            aud.pitch = Random.Range(0.85f, 1f);
            aud.volume = 0.18f;
            aud.clip = OuchSound;
            aud.Play();
        }
    }


}
