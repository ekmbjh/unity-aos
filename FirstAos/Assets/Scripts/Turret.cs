using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Bullet & Laser")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public LineRenderer lineRenderer;
    
    [Header("Unity Setip Field")]
    public string enemyTag = "Enemy";
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject neareestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                neareestEnemy = enemy;
            }

            if (neareestEnemy != null && shortestDistance <= range)
            {
                target = neareestEnemy.transform;
                targetEnemy = neareestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Laser();

        //if (fireCountdown <= 0f)
        //{
        //    Shoot();
        //    fireCountdown = 3f;
        //}
        //fireCountdown -= Time.deltaTime;
        //LockOnTarget();
    }

    //void Shoot()
    //{
    //    GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //    Bullet bullet = bulletGo.GetComponent<Bullet>();

    //    if (bullet != null)
    //    {
    //        bullet.Seek(target);
    //    }
    //}

    //void LockOnTarget()
    //{
    //    Vector3 dir = target.position - transform.position;
    //    Quaternion lookRotation = Quaternion.LookRotation(dir);
    //    Vector3 rotation = Quaternion.Lerp()
    //}

    void Laser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
