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
    public Transform nearestEnemy = null;
    public int enemiesIndex;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon" || other.tag == "BlueTower" || other.tag == "BlueNexus")
        {
            isChase = true;
            enemies = other.gameObject.GetComponents<Transform>();
            if (nearestEnemy == null)
            {
                shortestDistance = Mathf.Infinity;
            }
            target = other.transform;
            if (agent.enabled)
                agent.SetDestination(target.position);
            //foreach (Transform otherEnemy in enemies)
            //{
            //    distanceToEnemy = Vector3.Distance(otherEnemy.position, transform.position);
            //    if (distanceToEnemy < shortestDistance)
            //    {
            //        shortestDistance = distanceToEnemy;
            //        nearestEnemy = otherEnemy;
            //        enemiesIndex = Array.IndexOf(enemies, otherEnemy);
            //    }
            //}
            //if (nearestEnemy != null && shortestDistance <= range)
            //{
            //    target = nearestEnemy.transform;
            //    //targetEnemy = nearestEnemy.GetComponent<Enemy>();
            //}
        }

    }
    void OnTriggerExit(Collider other)
    {
        // 플레이어가 멀어지면 타겟을 웨이포인트로 변경하여 기존 이동경로로 복귀
    }

    public override void AttackAction()
    {
        if (target == null || !isChase)
            return;
        Vector3 dir = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(dir);
        animator.SetBool("isAttack", true);
        if (transform.GetChild(0).tag == "RedAd")
        {
            if (target.tag == "BlueTower")
            {
                Turret turret = target.GetComponentInParent<Turret>();
                turret.OnDamage(damage);
            }
            else if (target.tag == "BlueNexus")
            {
                Nexus nexus = target.GetComponentInParent<Nexus>();
                nexus.OnDamage(damage);
            }
            else
            {
                BlueEnemy enemyhealth = target.GetComponentInParent<BlueEnemy>();
                enemyhealth.OnDamage(damage);
            }
            //PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
        }
        else if (transform.GetChild(0).tag == "RedAp" || transform.GetChild(0).tag == "RedCanon")
        {
            string enemybulletTag = "Red";
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
    public void DetroyTurret(bool isDestroy)
    {
        isChase = isDestroy;
    }
}
