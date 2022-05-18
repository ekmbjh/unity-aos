using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteo : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 dir;

    public float damage = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
        Debug.Log(dir);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        

    }
    public void moveMeteo(Vector3 target)
    {
        
        dir = new Vector3(target.x, 0, target.z) - transform.position;
        Debug.LogWarning(dir);
        transform.LookAt(target);

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
