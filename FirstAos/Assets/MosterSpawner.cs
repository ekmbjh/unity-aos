using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterSpawner : MonoBehaviour
{
    public static Transform[] points;
    public float coolTime = 3f;
    public GameObject enemy;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    coolTime -= Time.deltaTime;
    //    if (coolTime <= 0)
    //    {
    //        Instantiate(enemy, transform.position, transform.rotation);
    //        coolTime = 3f;
    //    }
    //}
}
