using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject globDaddy;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;


    private CinemachineVirtualCamera CVC;

    public Player globDaddySc;

    private void Start()
    {
        CVC = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(globDaddy, respawnPoint.position, Quaternion.identity);
            CVC.m_Follow = playerTemp.transform;
            globDaddySc = playerTemp.GetComponent<Player>();
            respawn = false;
        }
    }
}
