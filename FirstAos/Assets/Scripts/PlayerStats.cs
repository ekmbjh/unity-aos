using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public float Damage = 10f;


    public Slider slider;
    Camera cam;

    string enemyTag = "Enemy";
    public static Transform myposition;

    private float attackCntDown;

    public bool isDead = false;
    public Animator animator;

    void Awake()
    {
        myposition = this.transform;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health / maxHealth;
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //    {
        //        //&& 
        //        if (hitInfo.collider.tag == enemyTag)
        //        {
        //            Vector3 dir = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        //            if (Vector3.Distance(dir, hitInfo.point) < 3f)
        //            {
        //                if (attackCntDown <= 0f)
        //                {
        //                    Attack(hitInfo.collider);
        //                    attackCntDown = 2f;
        //                }
        //            }
        //        }
        //    }
        //}
        //attackCntDown -= Time.deltaTime;
        if (isDead) { return; }
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Invoke("Die", 3f);
            
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnDamage(float enemyDamage)
    {
        health -= enemyDamage;
    }

    //void Attack(Collider enemy)
    //{
    //    Enemy _enemy = enemy.GetComponent<Enemy>();
    //    _enemy.OnDamage(Damage);
    //    Debug.Log("Attack");
    //}
}
