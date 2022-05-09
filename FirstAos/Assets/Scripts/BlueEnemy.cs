using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "RedAd" || other.tag == "RedAp" || other.tag == "RedCanon")
        {
            isChase = true;
            print("find red");
            target = other.transform;
        }
    }
}
