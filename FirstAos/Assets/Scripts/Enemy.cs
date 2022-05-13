using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform target;
    private int wavepointIndex = 0;

    public float health = 100f;
    public float maxhealth = 100f;
    public float damage = 10f;
    public Transform player;
    public bool isChase = false;
    private float attackCntDown;
    public float attackRange = 3f;

    public GameObject bullet;
    public GameObject firePosition;
    public Transform[] enemies;
    public Rigidbody rigidbody;
    public bool isDestroyed = false;
    public NavMeshAgent agent;

    public Animator animator;
    public bool isDie = false;

    public Slider slider;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponentInChildren<Rigidbody>();
        if (transform.tag == "Red")
        {
            target = Waypoint.points[0];
        }
        else if (transform.tag == "Blue")
        {
            target = WaypointBlue.bluePoints[0];
        }
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        slider.value = health / maxhealth;
        rigidbody.velocity = Vector3.zero;
        if (isDie)
        {
            return;
        }

        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
        if (target == null && !isChase)
        {
            //isChase = false;
            print("BackToLoad");
            BackToLoad();
        }
        //else if (target == null && enemies != null)
        //{
        //    isChase = false;
        //    print("target null , enemy null");
        //    FindNewTarget();
        //}
        if (!isChase)
        {
            FollowTheLoad();
        }
        else
        {
            MoveToTarget();
        }
        attackCntDown -= Time.deltaTime;
    }

    public void BackToLoad()
    {
        animator.SetBool("isAttack", false);
        agent.enabled = true;
        if (transform.tag == "Red")
        {
            target = Waypoint.points[wavepointIndex];
        }
        else if (transform.tag == "Blue")
        {
            target = WaypointBlue.bluePoints[wavepointIndex];
        }
        agent.SetDestination(target.position);
    }
    public void TurretDestory(bool destroy)
    {
        isDestroyed = destroy;
    }

    void MoveToTarget()
    {
        try
        {
            Vector3 direction = target.transform.position - transform.position;
            if (Vector3.Distance(target.position, transform.position) <= attackRange)
            {
                if (attackCntDown <= 0f && !isDie)
                {
                    agent.enabled = false;
                    animator.SetBool("isAttack", true);
                    //AttackPlayer();
                    attackCntDown = 2f;
                }
            }
        }
        catch
        {
            isChase = false;
        }
        //else
        //{
        //    print("move else");
        //    agent.SetDestination(direction);
        //transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        //}
    }

    public virtual void AttackAction()
    {

    }

    void FollowTheLoad()
    {
        Vector3 enemydir = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(enemydir);
        //try
        //{
        //    Vector3 enemydir = new Vector3(target.position.x, transform.position.y, target.position.z);
        //    transform.LookAt(enemydir);
        //}
        //catch
        //{
        //    isChase = false;
        //}
        // enemy가 이동할 방향
        //Vector3 dir = transform.position - target.position;
        //dir.y = 0f;
        // enemy를 이동
        // Space.World(월드 좌표 기준), Space.Self(오브젝트의 좌표 기준)
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);


        // 목표로 하는 Waypoint와의 거리가 1이하라면 다음 이동 목표 할당
        if (Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            GetNextWayPoint();
        }
        if (agent.enabled)
            agent.SetDestination(target.position);
        //if (agent.enabled)
        //{
        //    //Debug.DrawLine(transform.position, target.position);
        //}
    }
    void GetNextWayPoint()
    {
        if (transform.tag == "Red")
        {
            if (wavepointIndex >= Waypoint.points.Length - 1)
            {
                agent.ResetPath();
                agent.enabled = false;
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
                agent.ResetPath();
                agent.enabled = false;
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

    public void OnDamage(float Damage)
    {
        health -= Damage;
    }

    IEnumerator Die()
    {
        //if (isDie)
        //{
        //    //agent.ResetPath();
        //    //agent.isStopped = true;
        //    //agent.updatePosition = false;
        //    //agent.updateRotation = false;
        //    //agent.velocity = Vector3.zero;
        //}
        isDie = true;
        animator.SetBool("doDie", true);
        agent.enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
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
