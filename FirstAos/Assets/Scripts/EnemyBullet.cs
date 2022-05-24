using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform target;

    public float speed = 7f;
    public float damage = 10f;
    public string enemyBulletTag;

    public void Seek(Transform _target, string _enemyBulletTag)
    {
        target = _target;
        enemyBulletTag = _enemyBulletTag;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position + new Vector3(0, 1.2f, 0);
        float distanceThisFrame = speed * Time.deltaTime;

        transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        float rangeToAnemy = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z));
        if (rangeToAnemy < 0.5f)
        {
            print("target near");   
            //if (target.tag != "Red" || target.tag != "Blue") return;
            if (enemyBulletTag == "Blue")
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
                    RedEnemy enemy = target.GetComponentInParent<RedEnemy>();
                    enemy.OnDamage(damage);

                }
            }
            else if (enemyBulletTag == "Red")
            {
                print("tag red in");
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
                    print("attack and damage");
                    BlueEnemy enemy = target.GetComponentInParent<BlueEnemy>();
                    enemy.OnDamage(damage);
                }
            }

            print(" destroy");
            Destroy(gameObject);
        }
    }
}
