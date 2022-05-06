using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float Damage = 10f;

    Camera cam;

    string enemyTag = "Enemy";
    public static Transform myposition;

    private float attackCntDown;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myposition = this.transform;
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
                    {
                        if (attackCntDown <= 0f)
                        {
                            Attack(hitInfo.collider);
                            attackCntDown = 2f;
                        }
                    }
                }
            }
        }
        attackCntDown -= Time.deltaTime;
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
