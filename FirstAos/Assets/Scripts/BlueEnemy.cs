using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlueEnemy : Enemy
{
    public string enemyTag = "Red";
    public float range = 5f;
    GameObject[] otherEnemies;
    float shortestDistance = Mathf.Infinity;
    float distanceToEnemy;
    Transform nearestEnemy = null;
    public int enemiesIndex;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon" || other.tag == "RedTower")
        {
            isChase = true;
            enemies = other.gameObject.GetComponents<Transform>();
            foreach (Transform otherEnemy in enemies)
            {
                distanceToEnemy = Vector3.Distance(otherEnemy.position, transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = otherEnemy;
                    enemiesIndex = Array.IndexOf(enemies, otherEnemy);
                }
            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                //targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
        }
    }

    public override void AttackPlayer()
    {
        if (target == null || !isChase)
            return;
        if (transform.GetChild(0).tag == "BlueAd")
        {
            if (target.tag == "RedTower")
            {
                Turret turret = target.GetComponentInParent<Turret>();
                turret.OnDamage(damage);
            }
            else
            {
                print(target);
                RedEnemy enemyhealth = target.GetComponentInParent<RedEnemy>();
                enemyhealth.OnDamage(damage);
            }
            //PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
        }
        else if (transform.GetChild(0).tag == "BlueAp" || transform.GetChild(0).tag == "BlueCanon")
        {
            //RedEnemy enemyhealth = target.GetComponent<RedEnemy>();
            //enemyhealth.OnDamage(damage);
            string enemybulletTag = "Blue";
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            GameObject enemybullet = (GameObject)Instantiate(bullet, firePosition.transform.position, firePosition.transform.rotation);
            EnemyBullet enemybul = enemybullet.GetComponent<EnemyBullet>();
            enemybul.Seek(target, enemybulletTag);
        }
    }

    
    public override void FindNewTarget()
    {
        enemiesIndex++;
        target = enemies[enemiesIndex].transform;
        //foreach (GameObject otherEnemy in otherEnemies)
        //{
        //    distanceToEnemy = Vector3.Distance(otherEnemy.transform.position, transform.position);
        //    if (distanceToEnemy < shortestDistance)
        //    {
        //        shortestDistance = distanceToEnemy;
        //        nearestEnemy = otherEnemy;
        //    }
        //}
        //target = nearestEnemy.transform;

        //if (nearestEnemy != null && shortestDistance <= range)
        //{
        //    target = nearestEnemy.transform;
        //}
    }

    public void DetroyTurret(bool turret)
    {
        print("success");
        isChase = turret;
    }
}

//void Start()
//{
//    InvokeRepeating("UpdateTarget", 0f, 0.5f);
//}

//void UpdateTarget()
//{
//    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
//    float shortestDistance = Mathf.Infinity;
//    GameObject nearestEnemy = null;
//    foreach (GameObject enemy in enemies)
//    {
//        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
//        if (distanceToEnemy < shortestDistance)
//        {
//            shortestDistance = distanceToEnemy;
//            nearestEnemy = enemy;
//        }
//    }
//    if (nearestEnemy != null && shortestDistance <= range)
//    {
//        target = nearestEnemy.transform;
//        targetEnemy = nearestEnemy.GetComponent<Enemy>();
//    }
//    else
//    {
//        target = null;
//    }
