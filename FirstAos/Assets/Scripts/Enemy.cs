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
    public GameObject[] enemies;
    public Rigidbody rigidbody;
    public bool isDestroyed = false;

    // Start is called before the first frame update
    public void Start()
    {
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
        if (target == null)
        {
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

    public void TurretDestory(bool destroy)
    {
        isDestroyed = destroy;
    }
    //void OnTriggerEnter(Collider other)
    //{
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        isChase = true;
    //    }

    //}

    //void OnTriggerExit(Collider other)
    //{
    //    //isChase = false;
    //    //if (transform.tag == "RedAd" || transform.tag == "RedAp" || transform.tag == "RedCanon")
    //    //{
    //    //    target = Waypoint.points[wavepointIndex];
    //    //}
    //    //else if (transform.tag == "BlueAd" || transform.tag == "BlueAp" || transform.tag == "BlueCanon")
    //    //{
    //    //    target = WaypointBlue.bluePoints[wavepointIndex];
    //    //}
    //}

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
        //if (transform.tag == "RedAd" || transform.tag == "BlueAd")
        //{
        //    Enemy enemyhealth = target.GetComponent<Enemy>();
        //    enemyhealth.health -= damage;

        //}
        //else if (transform.tag == "RedAp" || transform.tag == "RedCanon" || transform.tag == "BlueAp" || transform.tag == "BlueCanon")
        //{
        //    Enemy enemyhealth = target.GetComponent<Enemy>();
        //    enemyhealth.health -= damage;
        //    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        //    GameObject enemybullet = (GameObject)Instantiate(bullet, firePosition.transform.position, firePosition.transform.rotation);
        //    EnemyBullet enemybul = enemybullet.GetComponent<EnemyBullet>();
        //    enemybul.Seek(target);
        //}
    }

    void FollowTheLoad()
    {
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
