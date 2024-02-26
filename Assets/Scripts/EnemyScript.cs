using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float range = 20;

    public Transform player;

    public void SetTarget(Transform target){
        player = target;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(player.position, transform.position);
        if(dist < range) agent.destination = player.position;
    }
}
