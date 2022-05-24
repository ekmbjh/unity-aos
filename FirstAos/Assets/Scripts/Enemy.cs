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

    public float enemyExp;

    public bool expUpDone = false;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        if (transform.tag == "Red")
        {
            target = Waypoint.points[0];
        }
        else if (transform.tag == "Blue")
        {
            target = WaypointBlue.bluePoints[0];
        }
        animator = transform.GetComponent<Animator>();

        if (transform.GetChild(0).tag == "RedAd")
        {
            enemyExp = 20;
        }
        else if (transform.GetChild(0).tag == "RedAp")
        {
            enemyExp = 30;
        }
        else if (transform.GetChild(0).tag == "RedCanon")
        {
            enemyExp = 50;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        slider.value = health / maxhealth;
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
            BackToLoad();
        }

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

    }

    public virtual void AttackAction()
    {

    }

    void FollowTheLoad()
    {
        Vector3 enemydir = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(enemydir);
        if (Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            GetNextWayPoint();
        }
        if (agent.enabled)
            agent.SetDestination(target.position);
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

        isDie = true;
        animator.SetBool("doDie", true);
        agent.enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
