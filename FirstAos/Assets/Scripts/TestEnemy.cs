using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public GameObject goal;

    private bool isChase = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChase)
        {
            agent.SetDestination(goal.transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isChase = true;
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        isChase = false;
        agent.SetDestination(goal.transform.position);
    }
}
