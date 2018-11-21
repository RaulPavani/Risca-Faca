using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour {


	[SerializeField]
	private Transform m_Target;

	[SerializeField]
	private Transform[] m_EndOfLayout;

    public float dampTime = 0.2f;


    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private bool xLockedLeft, xLockedRight, yLockedUp, yLockedDown;
    private float yLast, xLast;

    private Vector3 scrPos;
    private bool seekPlayer;

    float leftSideOfScreen, rightSideOfScreen, topOfScreen, botOfScreen;

    void Start () {

        seekPlayer = true;
        leftSideOfScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        rightSideOfScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;

        topOfScreen = Camera.main.transform.position.y + Camera.main.orthographicSize * Screen.width / Screen.height;
        botOfScreen = Camera.main.transform.position.y - Camera.main.orthographicSize * Screen.width / Screen.height;

        m_Target = GameObject.FindGameObjectWithTag ("Player").transform;
        scrPos = Camera.main.WorldToScreenPoint(transform.position);
        offset = new Vector3 (0, 0, -10);
              
		transform.position = m_Target.position + offset;

        xLockedLeft = false;
        xLockedRight = false;
        yLockedUp = false;
        yLockedDown = false;

        SeekPlayer();


    }

    void SeekPlayer()
    {
        if(rightSideOfScreen >= 25.75)
        {
            transform.position = new Vector3(15f, transform.position.y, transform.position.z);
            xLockedRight = true;
        }
        if (leftSideOfScreen <= -25.75f)
        {
            transform.position = new Vector3(-15f, transform.position.y, transform.position.z);
            xLockedLeft = true;
        }

        if (topOfScreen >= 20.15f)
        {
            transform.position = new Vector3(transform.position.x, 9.5f, transform.position.z);
            yLockedUp = true;
        }

        if (botOfScreen <= -17.75f)
        {
            transform.position = new Vector3(transform.position.x, -7.2f, transform.position.z);
            yLockedDown = true;
        }
    }

    void Update() {
        

        leftSideOfScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        rightSideOfScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;

        topOfScreen = Camera.main.transform.position.y + Camera.main.orthographicSize * Screen.width / Screen.height;
        botOfScreen = Camera.main.transform.position.y - Camera.main.orthographicSize * Screen.width / Screen.height;



        if (rightSideOfScreen >= 25.75)
        {
            xLockedRight = true;
            xLast = transform.position.x;
        }
        else
        {
            xLockedRight = false;
        }

        if (leftSideOfScreen <= -25.75f)
        {
            xLockedLeft = true;
            xLast = transform.position.x;
        }
        else
        {
            xLockedLeft = false;
        }



        if (topOfScreen >= 20.15f)
        {

            yLockedUp = true;
            yLast = transform.position.y;
        }
        else
        {
            yLockedUp = false;
        }

        if(botOfScreen <= -17.75f)
        {
            yLockedDown = true;
            yLast = transform.position.y;
        }
        else
        {
            yLockedDown = false;
        }


        //DESLOCA CAMERA


        if (xLockedLeft)
        {
           if(m_Target.position.x >= transform.position.x){
                xLockedLeft = false;
            }
        }

        if (xLockedRight)
        {
            if (m_Target.position.x <= transform.position.x)
            {
                xLockedRight = false;
            }
        }


        if (yLockedUp)
        {
            if (m_Target.position.y <= transform.position.y)
            {
                yLockedUp = false;
            }
        }
        if (yLockedDown)
        {
            if (m_Target.position.y >= transform.position.y)
            {
                yLockedDown = false;
            }
        }

    }


    void LateUpdate()
    {

        if ((!xLockedLeft && !xLockedRight) && (!yLockedUp && !yLockedDown))
        {
            //transform.position = Vector3.SmoothDamp(transform.position, m_Target.position + offset, ref velocity, dampTime);
            transform.position = m_Target.position + offset;
        }

        else if((xLockedLeft || xLockedRight) && (!yLockedUp && !yLockedDown))
        {
			//transform.position = Vector3.SmoothDamp(transform.position, m_Target.position + offset, ref velocity, dampTime);

            transform.position = new Vector3(xLast, m_Target.position.y, -10);
        }

        else if ((!xLockedLeft && !xLockedRight) && (yLockedUp || yLockedDown))
        {
			//transform.position = Vector3.SmoothDamp(transform.position, m_Target.position + offset, ref velocity, dampTime);

            transform.position = new Vector3(m_Target.position.x, yLast, -10);
        }
    }

	void OnLevelLoad(){
		Start ();
	}


}
/*
        // N S L O
        //if((m_Target.position.y > m_EndOfLayout[0].position.y || m_Target.position.y < m_EndOfLayout[1].position.y) && transform.position != m_Target.position)
        {
            yLocked = true;
            yLast = transform.position.y;
        }
        else
        {
            yLocked = false;;
        }

        //if ((m_Target.position.x > m_EndOfLayout[3].position.x || m_Target.position.x < m_EndOfLayout[2].position.x) && transform.position != m_Target.position)
        if (scrPos.x > Screen.width)
        {
            xLocked = true;
            xLast = transform.position.x;
        }
        else
        {
            xLocked = false;
        }
        */

