using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineOfSightEnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        enemy.Warp(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Vector3.Distance(transform.position, player.position) <= 20f || Physics.Raycast(transform.position, player.position - transform.position, out hit) && hit.transform.tag == "Player")
            enemy.SetDestination(player.position);
        else
            enemy.SetDestination(transform.position);
    }
}
