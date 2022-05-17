using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteo : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 dir;

    public float damage = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
        transform.LookAt(dir);
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void moveMeteo(Vector3 target)
    {
        dir = new Vector3(target.x, -1, target.z) - transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red") //Ad" || other.tag == "RedAp" || other.tag == "RedCanon"
        {
            RedEnemy enemy = other.GetComponent<RedEnemy>();
            enemy.OnDamage(damage);
        }
    }
}
