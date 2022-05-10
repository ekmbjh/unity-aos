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
        // Waypoint.cs 에서 할당한 배열을 불러와서 target에 할당
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
        // enemy가 이동할 방향
        Vector3 dir = target.position - transform.position;
        // enemy를 이동
        // Space.World(월드 좌표 기준), Space.Self(오브젝트의 좌표 기준)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // 목표로 하는 Waypoint와의 거리가 1이하라면 다음 이동 목표 할당
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
                // return을 안시키면 하단의 명령을 실행하여 Null Reference Exception발생
                return;
            }
            // Waypoint 배열의 마지막에 도착하면 목표물 삭제
            // 배열의 마지막 Index는 총길이 - 1(0부터 시작하므로)
            // 다음 목표물 할당을 위한 Index 증가(+1)
            wavepointIndex++;
            // 증가된 Index를 활용하여 target에 다음 목표 할당
            target = Waypoint.points[wavepointIndex];
        }
        else if (transform.tag == "Blue")
        {
            if (wavepointIndex >= WaypointBlue.bluePoints.Length - 1)
            {
                Destroy(gameObject);
                // return을 안시키면 하단의 명령을 실행하여 Null Reference Exception발생
                return;
            }
            // Waypoint 배열의 마지막에 도착하면 목표물 삭제
            // 배열의 마지막 Index는 총길이 - 1(0부터 시작하므로)
            // 다음 목표물 할당을 위한 Index 증가(+1)
            wavepointIndex++;
            // 증가된 Index를 활용하여 target에 다음 목표 할당
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
