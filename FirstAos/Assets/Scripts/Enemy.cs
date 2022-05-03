using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    private int wavepointIndex = 0;
    //NavMeshAgent agent;

    void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Waypoint.cs 에서 할당한 배열을 불러와서 target에 할당
        target = Waypoint.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        // enemy가 이동할 방향
        Vector3 dir = target.position - transform.position;
        // enemy를 이동
        // Space.World(월드 좌표 기준), Space.Self(오브젝트의 좌표 기준)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // 목표로 하는 Waypoint와의 거리가 0.4이하라면 다음 이동 목표 할당
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
        //agent.SetDestination(target.position);
    }

    void GetNextWayPoint()
    {
        // Waypoint 배열의 마지막에 도착하면 목표물 삭제
        // 배열의 마지막 Index는 총길이 - 1(0부터 시작하므로)
        if (wavepointIndex >= Waypoint.points.Length - 1)
        {
            Destroy(gameObject);
            // return을 안시키면 하단의 명령을 실행하여 Null Reference Exception발생
            return;
        }
        // 다음 목표물 할당을 위한 Index 증가(+1)
        wavepointIndex++;
        // 증가된 Index를 활용하여 target에 다음 목표 할당
        target = Waypoint.points[wavepointIndex];
    }
}
