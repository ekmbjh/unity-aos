using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon")
        {
            isChase = true;
            blueQueue.Enqueue(other.transform);
            //target = other.transform;
            if (target == null)
            {
                target = blueQueue.Dequeue();
                return;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
    }

}
