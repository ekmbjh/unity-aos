using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon")
        {
            isChase = true;
            print("find Blue");
            target = other.transform;
        }
    }
}
