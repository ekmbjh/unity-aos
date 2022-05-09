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
        if (enemyBulletTag == "Red")
        {
            if (target == null || target.tag != "Blue")
            {
                Destroy(gameObject);
                return;
            }
        }
        else if (enemyBulletTag == "Blue")
        {
            if (target == null || target.tag != "Red")
            {
                Destroy(gameObject);
                return;
            }
        }
        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            if (enemyBulletTag == "Blue")
            {
                RedEnemy enemy = target.GetComponent<RedEnemy>();
                enemy.OnDamage(damage);
            }
            else if (enemyBulletTag == "Red")
            {
                BlueEnemy enemy = target.GetComponent<BlueEnemy>();
                enemy.OnDamage(damage);
            }
            
            //PlayerStats playerHealth = target.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
            Debug.Log("AdEnemy Attack");
            Destroy(gameObject);
        }
    }
}
