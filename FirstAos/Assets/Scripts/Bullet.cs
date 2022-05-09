using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed = 7f;

    public void Seek(Transform _target)
    {
        Debug.Log("≈∏∞Ÿ»Æ¿Œ");
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

        //if (dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            Destroy(gameObject);
        }
    }

    //void HitTarget()
    //{
    //    GameObject effectIns
    //}
}
