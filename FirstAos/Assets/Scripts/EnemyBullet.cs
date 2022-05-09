using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform target;

    public float speed = 7f;
    public float damage = 10f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.health -= damage;
            //PlayerStats playerHealth = target.GetComponent<PlayerStats>();
            //playerHealth.health -= damage;
            Debug.Log("AdEnemy Attack");
            Destroy(gameObject);
        }
    }
}
