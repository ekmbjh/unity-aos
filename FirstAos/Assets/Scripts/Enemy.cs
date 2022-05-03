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
        // Waypoint.cs ���� �Ҵ��� �迭�� �ҷ��ͼ� target�� �Ҵ�
        target = Waypoint.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        // enemy�� �̵��� ����
        Vector3 dir = target.position - transform.position;
        // enemy�� �̵�
        // Space.World(���� ��ǥ ����), Space.Self(������Ʈ�� ��ǥ ����)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // ��ǥ�� �ϴ� Waypoint���� �Ÿ��� 0.4���϶�� ���� �̵� ��ǥ �Ҵ�
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
        //agent.SetDestination(target.position);
    }

    void GetNextWayPoint()
    {
        // Waypoint �迭�� �������� �����ϸ� ��ǥ�� ����
        // �迭�� ������ Index�� �ѱ��� - 1(0���� �����ϹǷ�)
        if (wavepointIndex >= Waypoint.points.Length - 1)
        {
            Destroy(gameObject);
            // return�� �Ƚ�Ű�� �ϴ��� ����� �����Ͽ� Null Reference Exception�߻�
            return;
        }
        // ���� ��ǥ�� �Ҵ��� ���� Index ����(+1)
        wavepointIndex++;
        // ������ Index�� Ȱ���Ͽ� target�� ���� ��ǥ �Ҵ�
        target = Waypoint.points[wavepointIndex];
    }
}
