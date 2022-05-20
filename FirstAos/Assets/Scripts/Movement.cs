using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 3f;
    public CharacterController controller;
    public Vector3 movePoint;
    public Camera veiw;
    public Animator animator;

    public Vector3 updateMovePoint;

    public bool isTracking = false;
    public float attackCount = 0;
    public float damage = 50f;

    public GameObject meteo;

    public float skillCount = 0;
    public Button skillMeteo;
    public Text skilCountText;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        veiw = Camera.main;
        controller = GetComponent<CharacterController>();
        movePoint = transform.position;
        animator = transform.GetComponent<Animator>();
        PlayerStats stats = GetComponent<PlayerStats>();
        skillMeteo.interactable = false;
        skilCountText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats stats = transform.GetComponent<PlayerStats>();
        Ray ray = veiw.ScreenPointToRay(Input.mousePosition);

        if (stats.isDead) { return; }

        if (Vector3.Distance(new Vector3(movePoint.x, transform.position.y, movePoint.z), transform.position) < 0.5f)
        {
            animator.SetBool("isRun", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isRun", true);
            Debug.DrawRay(ray.origin, movePoint * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                movePoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                if (hitInfo.transform.tag == "Red")
                {
                    if (Vector3.Distance(movePoint, transform.position) < 2f)
                    {
                        if (attackCount <= 0)
                        {
                            isTracking = true;
                            movePoint = transform.position;
                            StartCoroutine(AttackGo(hitInfo.transform));
                        }
                    }
                    else
                    {
                        //movePoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                    }
                }
                else
                {

                    //movePoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);


                }
            }
        }
        attackCount -= Time.deltaTime;


        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            if (!isTracking)
            {
                Move();

            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && skillCount <= 0f)
        {
            skillMeteo.interactable = false;
            skilCountText.enabled = true;
            GameObject me = Instantiate(meteo, transform.position + new Vector3(0, 20f, 0), transform.rotation);
            meteo Meteo = me.GetComponent<meteo>();
            skillCount = 10f;
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.DrawLine(transform.position + new Vector3(0, 20f, 0), hitInfo.point, Color.yellow, 1.5f);
                //Vector3 dir = meteo.transform.position - hitInfo.point;
                Meteo.moveMeteo(hitInfo.point);
            }
        }
        skilCountText.text = skillCount.ToString("0");
        if (skillCount <= 0f && stats.level >= 2f)
        {
            skillMeteo.interactable = true;
            skilCountText.enabled = false;
        }
        skillCount -= Time.deltaTime;
    }
    IEnumerator AttackGo(Transform _enemy)
    {
        attackCount = 2f;
        RedEnemy enemy = _enemy.GetComponent<RedEnemy>();
        //if (enemy.health <= 0)
        //{
        //    Debug.Log("health 0");
        //    stats.exp += enemy.exp;
        //}
        Vector3 dir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;
        transform.LookAt(_enemy);
        //if (dir != Vector3.zero)
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        animator.SetBool("isAttack", true);
        enemy.OnDamage(damage);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false);
        isTracking = false;

    }

    void Move()
    {
        updateMovePoint = (movePoint - transform.position).normalized * speed;
        //transform.Translate(updateMovePoint.normalized * speed * Time.deltaTime, Space.World);

        Vector3 dir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        controller.SimpleMove(updateMovePoint);

    }
}
