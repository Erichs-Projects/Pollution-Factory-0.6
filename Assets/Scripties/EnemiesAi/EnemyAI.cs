using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public PlayerStats playerStats;
    public float checkRate = 0.01f;
    public float daySpeed = 3f;
    public float nightSpeed = 4f;

    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent myNavmesh;
    float nextCheck;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myNavmesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        FollowPlayer();
        if (playerStats.isEvening) myNavmesh.speed = nightSpeed;
        else myNavmesh.speed = daySpeed;
    }

    void FollowPlayer()
    {
        myNavmesh.transform.LookAt(playerTransform);
        myNavmesh.destination = playerTransform.position;
    }



}
