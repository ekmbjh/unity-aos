using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlueAd" || other.tag == "BlueAp" || other.tag == "BlueCanon")
        {
            isChase = true;
            redQueue.Enqueue(other.transform);
            //target = other.transform;
            
            /* 큐가 널이면 리턴;
             * 타겟이 bluead, blueap, bluecanon이 아니면 디큐해서 타겟 변경
             * */
            if (target == null)
            {
                target = redQueue.Dequeue();
                return;
            }
        }
    }
}
