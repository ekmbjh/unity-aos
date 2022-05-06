using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform target;
    private int wavepointIndex = 0;

    public float health = 100f;
    private float damage = 10f;
    public Transform player;
    public bool isChase = false;
    private float attackCntDown;
    public float attackRange;

    void Awake()
    {
        player = PlayerStats.myposition;
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
        if (!isChase)
        {
            FollowTheLoad();
        }
        else
        {
            MoveToPlayer();
        }
        attackCntDown -= Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isChase = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        isChase = false;
        target = Waypoint.points[wavepointIndex];
    }

    void MoveToPlayer()
    {
        target = player.transform;
        Vector3 direction = target.transform.position - transform.position;
        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            if (attackCntDown <= 0f)
            {
                AttackPlayer();
                attackCntDown = 2f;
            }
        }
        else
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attack Player");
    }

    void FollowTheLoad()
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


    public void OnDamage(float playerDamage)
    {
        health -= playerDamage;
    }
}
//if (Physics.SphereCast(transform.position, 5f, Vector3.up, out RaycastHit hitinfo, 0.1f, LayerMask.GetMask("Player")))
//{
//    Debug.Log("Find Player");
//    StopCoroutine(FollowTheLoad());
//    //StartCoroutine(MoveToPlayer());
//}
//else
//{
//    StartCoroutine(FollowTheLoad());
//}
