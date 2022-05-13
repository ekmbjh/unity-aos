using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    public bool isDestroy = false;

    public GameObject TurretDesScript;
    public GameObject TurretDesScriptRed;
    public GameObject TurretDesScriptBlue;

    [Header("General")]
    public float range = 15f;
    public float health = 100f;
    public float maxhealth = 100f;

    [Header("Bullet & Laser")]
    public GameObject bulletPrefab;
    private float fireCountdown = 0f;
    public LineRenderer lineRenderer;
    GameObject[] enemies;

    [Header("Unity Setip Field")]
    public Transform firePoint;
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        if (transform.tag == "Red")
        {
            enemies = GameObject.FindGameObjectsWithTag("Blue");

        }
        else if (transform.tag == "Blue")
        {
            enemies = GameObject.FindGameObjectsWithTag("Red");

        }
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        hpSlider.value = health / maxhealth;
        if (health <= 0f)
        {
            
            //RedEnemy redenemy = TurretDesScriptRed.GetComponent<RedEnemy>();
            //BlueEnemy blueenemy = TurretDesScriptBlue.GetComponent<BlueEnemy>();

            //    //blueenemy.TurretDestroy(true);
            //    //redenemy.TurretDestroy(true);
            //    //Enemy enemy = TurretDesScript.GetComponent<Enemy>();
            //    //enemy.TurretDestory(true);
            //    //enemy.isChase = false;

            //redenemy.DetroyTurret(false);
            //blueenemy.DetroyTurret(false);

            //    redenemy.target = null;
            //    blueenemy.target = null;

            //    //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if (target == null)
        {
            return;
        }
        else
        {
            Laser();
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 2f;
        }
        fireCountdown -= Time.deltaTime;

    }
    void Laser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0, 0.6f, 0.2f));
    }


    void Shoot()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy.isDie)
        {
            return;
        }
        
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void OnDamage(float Damage)
    {
        health -= Damage;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
