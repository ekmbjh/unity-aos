using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    private int wavepointIndex = 0;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = MosterSpawner.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
            print("distance closed");
        }
    }

    void GetNextWayPoint()
    {
        print("NextWayPoint");
        if (wavepointIndex >= MosterSpawner.points.Length - 1)
        {
            Destroy(gameObject);
        }
        wavepointIndex++;
        target = MosterSpawner.points[wavepointIndex];
    }
}
