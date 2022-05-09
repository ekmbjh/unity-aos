using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedEnemy : Enemy
{
    public string enemyTag = "Blue";
    public float range = 5f;
    GameObject[] otherEnemies;
    public float shortestDistance = Mathf.Infinity;
    public float distanceToEnemy;
    GameObject nearestEnemy = null;
    public int enemiesIndex;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon")
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

        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon")
        //    {
        //        isChase = true;
        //        redQueue.Enqueue(other.transform);
        //        //target = other.transform;
        //        /* 큐가 널이면 리턴;
        //         * 타겟이 bluead, blueap, bluecanon이 아니면 디큐해서 타겟 변경
        //         * */
        //        if (target == null)
        //        {
        //            target = redQueue.Dequeue();
        //            //return;
        //        }
        //        if (target.tag != "BlueAd" || target.tag != "BlueAp" || target.tag != "BlueCanon")
        //        {
        //            target = redQueue.Dequeue();
        //        }
        //    }
        //}
    }
    public override void AttackPlayer()
    {
        if (transform.GetChild(0).tag == "RedAd")
        {
            BlueEnemy enemyhealth = target.GetComponent<BlueEnemy>();
            enemyhealth.OnDamage(damage);
            //PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
            Debug.Log("AdEnemy Attack");
        }
        else if (transform.GetChild(0).tag == "RedAp" || transform.GetChild(0).tag == "RedCanon")
        {
            //BlueEnemy enemyhealth = target.GetComponent<BlueEnemy>();
            //enemyhealth.OnDamage(damage);
            string enemybulletTag = "Red";
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
