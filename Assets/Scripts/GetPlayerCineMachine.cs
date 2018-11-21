using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetPlayerCineMachine : MonoBehaviour
{

    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;


    // Use this for initialization
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.LookAt = tFollowTarget;
                vcam.Follow = tFollowTarget;
            }
        }
    }


	public void GetPlayer(){
		this.enabled = true;
	}

    public void LosePlayer()
    {
        tPlayer = GameObject.Find("Chefe");
        tFollowTarget = tPlayer.transform;
        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;
    }

}

