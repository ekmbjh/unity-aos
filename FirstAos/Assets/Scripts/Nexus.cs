using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : MonoBehaviour
{
    public float health = 100f;
    public float maxhealth = 100f;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health / maxhealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDamage(float Damage)
    {
        health -= Damage;
    }
}
