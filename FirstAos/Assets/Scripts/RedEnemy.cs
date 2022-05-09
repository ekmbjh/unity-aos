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
            
            /* ť�� ���̸� ����;
             * Ÿ���� bluead, blueap, bluecanon�� �ƴϸ� ��ť�ؼ� Ÿ�� ����
             * */
            if (target == null)
            {
                target = redQueue.Dequeue();
                return;
            }
        }
    }
}
