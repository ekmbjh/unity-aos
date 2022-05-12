using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Bullet & Laser")]
    public GameObject bulletPrefab;
    private float fireCountdown = 0f;
    public LineRenderer lineRenderer;

    [Header("Unity Setip Field")]
    public string enemyTag = "AdEnemy";
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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
        if (health <= 0f)
        {
            RedEnemy redenemy = TurretDesScriptRed.GetComponent<RedEnemy>();
            BlueEnemy blueenemy = TurretDesScriptBlue.GetComponent<BlueEnemy>();

            //    //blueenemy.TurretDestroy(true);
            //    //redenemy.TurretDestroy(true);
            //    //Enemy enemy = TurretDesScript.GetComponent<Enemy>();
            //    //enemy.TurretDestory(true);
            //    //enemy.isChase = false;

            redenemy.DetroyTurret(false);
            blueenemy.DetroyTurret(false);

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

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
    void Laser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
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
