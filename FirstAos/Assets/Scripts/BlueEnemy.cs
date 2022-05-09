using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlueEnemy : Enemy
{
    public string enemyTag = "Red";
    public float range = 5f;
    GameObject[] otherEnemies;
    public float shortestDistance = Mathf.Infinity;
    public float distanceToEnemy;
    GameObject nearestEnemy = null;
    public int enemiesIndex;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon")
        {
            isChase = true;
        }
        otherEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in otherEnemies)
        {
            distanceToEnemy = Vector3.Distance(otherEnemy.transform.position, transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = otherEnemy;
                enemiesIndex = Array.IndexOf(otherEnemies, otherEnemy);
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        //if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon")
        //{
        //    isChase = true;
        //    blueQueue.Enqueue(other.transform);
        //    //target = other.transform;
        //    if (target == null)
        //    {
        //        target = blueQueue.Dequeue();
        //        //return;
        //    }
        //    if (target.tag != "RedAd" || target.tag != "RedAp" || target.tag != "RedCanon")
        //    {
        //        target = redQueue.Dequeue();
        //    }
        //}
    }
    
    public override void AttackPlayer()
    {
        if (transform.GetChild(0).tag == "BlueAd")
        {
            RedEnemy enemyhealth = target.GetComponent<RedEnemy>();
            enemyhealth.OnDamage(damage);
            //PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
            Debug.Log("AdEnemy Attack");
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
            Debug.Log("ApEnemy Bullet Instantiate");
        }
    }

    public override void FindNewTarget()
    {
        enemiesIndex++;
        target = otherEnemies[enemiesIndex].transform;
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
