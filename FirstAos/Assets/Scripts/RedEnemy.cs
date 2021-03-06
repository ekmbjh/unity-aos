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
        if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon" || other.tag == "BlueTower" || other.tag == "BlueNexus" || other.tag == "BluePlayer")
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
        }

        if (other.tag == "BluePlayer" && isDie)
        {
            if (expUpDone)
            {
                return;
            }
            expUpDone = true;
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.exp += enemyExp;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "BluePlayer")
        {
            target = null;

        }
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
            else if (target.tag == "BluePlayer")
            {
                PlayerStats playerStats = target.GetComponentInParent<PlayerStats>();
                playerStats.OnDamage(damage);
            }
            else
            {
                BlueEnemy enemyhealth = target.GetComponentInParent<BlueEnemy>();
                enemyhealth.OnDamage(damage);
            }

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

    }
    public void DetroyTurret(bool isDestroy)
    {
        isChase = isDestroy;
    }
}
