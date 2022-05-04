using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float Damage = 10f;
    Camera cam;
    private Vector3 attackRange;
    string enemyTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                //&& 
                if (hitInfo.collider.tag == enemyTag)
                {
                    Vector3 dir = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
                    if (Vector3.Distance(dir, hitInfo.point) < 3f)
                        Attack(hitInfo.collider);
                }
            }
        }
    }

    //public static void OnDamage(float enemyDamage)
    //{
    //    health -= enemyDamage;
    //}

    void Attack(Collider enemy)
    {
        Enemy _enemy = enemy.GetComponent<Enemy>();
        _enemy.OnDamage(Damage);
        Debug.Log("Attack");
    }
}
