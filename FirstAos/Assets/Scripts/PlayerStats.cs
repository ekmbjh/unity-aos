using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public int level = 1;
    public float exp = 0;

    public Slider slider;
    //Camera cam;

    public static Transform myposition;

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
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health / maxHealth;
        if (isDead) { return; }
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Invoke("Die", 3f);
        }
        if (exp >= 100f)
        {
            level += 1;
            exp = 0;
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
}
