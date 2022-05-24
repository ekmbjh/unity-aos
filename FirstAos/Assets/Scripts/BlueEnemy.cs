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
    public Transform nearestEnemy = null;
    public int enemiesIndex;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon" || other.tag == "RedTower" || other.tag == "RedNexus")
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
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "RedPlayer")
        {
            target = null;

        }
    }

    public override void AttackAction()
    {
        if (target == null) // || !isChase
            return;
        Vector3 dir = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(dir);
        //animator.SetBool("isAttack", true);
        if (transform.GetChild(0).tag == "BlueAd")
        {
            if (target.tag == "RedTower")
            {
                Turret turret = target.GetComponentInParent<Turret>();
                turret.OnDamage(damage);
            }
            else if (target.tag == "RedNexus")
            {
                Nexus nexus = target.GetComponentInParent<Nexus>();
                nexus.OnDamage(damage);
            }
            else
            {
                RedEnemy enemyhealth = target.GetComponentInParent<RedEnemy>();
                enemyhealth.OnDamage(damage);
            }
        }
        else if (transform.GetChild(0).tag == "BlueAp" || transform.GetChild(0).tag == "BlueCanon")
        {
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
    }

    public void DetroyTurret(bool isDestroy)
    {
        isChase = isDestroy;
    }
}
