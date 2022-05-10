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
    public float damage = 10f;
    public Transform player;
    public bool isChase = false;
    private float attackCntDown;
    public float attackRange = 2f;

    public GameObject bullet;
    public GameObject firePosition;
    public Transform[] enemies;
    public Rigidbody rigidbody;
    public bool isDestroyed = false;
    public GameObject[] blueWayPoints;
    public GameObject[] redWayPoints;
    public int wayPointIndex = 0;

    public void Start()
    {
        blueWayPoints = GameObject.FindGameObjectsWithTag("BlueWay");
        redWayPoints = GameObject.FindGameObjectsWithTag("RedWay");
        rigidbody = GetComponentInChildren<Rigidbody>();
        if (transform.tag == "Red")
        {
            target = Waypoint.points[0];
        }
        else if (transform.tag == "Blue")
        {
            target = WaypointBlue.bluePoints[0];
        }
        // Waypoint.cs ���� �Ҵ��� �迭�� �ҷ��ͼ� target�� �Ҵ�
        //player = PlayerStats.myposition;
    }

    // Update is called once per frame
    public void Update()
    {
        rigidbody.velocity = Vector3.zero;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
        if (target == null && enemies.Length == 1)
        {
            isChase = false;
            print("BackToLoad");
            BackToLoad();
        }
        else if (target == null && enemies != null)
        {
            isChase = false;
            print("target null , enemy null");
            FindNewTarget();
        }
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

    public void BackToLoad()
    {
        if (transform.tag == "Blue")
        {
            GameObject nearWay = null;
            float shortDistance = Mathf.Infinity;
            foreach (GameObject way in blueWayPoints)
            {
                float distance = Vector3.Distance(transform.position, way.transform.position);
                if (distance <= shortDistance)
                {
                    shortDistance = distance;
                    nearWay = way;
                }
            }
            target = nearWay.transform;
        }
        else if (transform.tag == "Red")
        {
            GameObject nearWay = null;
            float shortDistance = Mathf.Infinity;
            foreach (GameObject way in redWayPoints)
            {
                float distance = Vector3.Distance(transform.position, way.transform.position);
                if (distance <= shortDistance)
                {
                    shortDistance = distance;
                    nearWay = way;
                }
            }
            target = nearWay.transform;
        }
    }
    public void TurretDestory(bool destroy)
    {
        isDestroyed = destroy;
    }

    void MoveToPlayer()
    {
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

    public virtual void AttackPlayer()
    {

    }

    void FollowTheLoad()
    {
        //if (wayPointIndex != 0)
        //{
        //    if (transform.tag == "Blue")
        //    {
        //        target = WaypointBlue.bluePoints[wayPointIndex];
        //        wayPointIndex++;
        //    }
        //    else if (transform.tag == "Red")
        //    {
        //        target = Waypoint.points[wayPointIndex];
        //        wayPointIndex++;
        //    }
        //}
        // enemy�� �̵��� ����
        Vector3 dir = target.position - transform.position;
        // enemy�� �̵�
        // Space.World(���� ��ǥ ����), Space.Self(������Ʈ�� ��ǥ ����)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // ��ǥ�� �ϴ� Waypoint���� �Ÿ��� 1���϶�� ���� �̵� ��ǥ �Ҵ�
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            GetNextWayPoint();
        }

    }
    void GetNextWayPoint()
    {
        if (transform.tag == "Red")
        {
            if (wavepointIndex >= Waypoint.points.Length - 1)
            {
                Destroy(gameObject);
                // return�� �Ƚ�Ű�� �ϴ��� ����� �����Ͽ� Null Reference Exception�߻�
                return;
            }
            // Waypoint �迭�� �������� �����ϸ� ��ǥ�� ����
            // �迭�� ������ Index�� �ѱ��� - 1(0���� �����ϹǷ�)
            // ���� ��ǥ�� �Ҵ��� ���� Index ����(+1)
            wavepointIndex++;
            // ������ Index�� Ȱ���Ͽ� target�� ���� ��ǥ �Ҵ�
            target = Waypoint.points[wavepointIndex];
        }
        else if (transform.tag == "Blue")
        {
            if (wavepointIndex >= WaypointBlue.bluePoints.Length - 1)
            {
                Destroy(gameObject);
                // return�� �Ƚ�Ű�� �ϴ��� ����� �����Ͽ� Null Reference Exception�߻�
                return;
            }
            // Waypoint �迭�� �������� �����ϸ� ��ǥ�� ����
            // �迭�� ������ Index�� �ѱ��� - 1(0���� �����ϹǷ�)
            // ���� ��ǥ�� �Ҵ��� ���� Index ����(+1)
            wavepointIndex++;
            // ������ Index�� Ȱ���Ͽ� target�� ���� ��ǥ �Ҵ�
            target = WaypointBlue.bluePoints[wavepointIndex];
        }
    }

    public virtual void FindNewTarget()
    {

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
