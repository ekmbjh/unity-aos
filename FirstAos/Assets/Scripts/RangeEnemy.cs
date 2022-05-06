using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public GameObject bullet;
    public GameObject firePosition;
 
    public override void AttackPlayer()
    {
        Debug.Log("업데이트");
        GameObject enemybullet = (GameObject)Instantiate(bullet, firePosition.transform.position, firePosition.transform.rotation);

        Vector3 dir = target.position - enemybullet.transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        enemybullet.transform.LookAt(target);
        enemybullet.transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        print("range attack");
    }

    void FixedUpdate()
    {
        
    }
}
