using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        veiw = Camera.main;
        controller = GetComponent<CharacterController>();
        movePoint = transform.position;
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(movePoint.x, transform.position.y, movePoint.z), transform.position) < 0.5f)
        {
            animator.SetBool("isRun", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isRun", true);
            Ray ray = veiw.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, movePoint * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                movePoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                if (hitInfo.transform.tag == "Red")
                {
                    print(hitInfo.transform);
                    isTracking = true;
                    if (Vector3.Distance(movePoint, transform.position) < 2f)
                    {
                        print(attackCount);
                        if (attackCount <= 0)
                        {
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
                    isTracking = false;
                    //movePoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);


                }
            }
        }
        attackCount -= Time.deltaTime;

        IEnumerator AttackGo(Transform _enemy)
        {
            attackCount = 2f;
            Enemy enemy = _enemy.GetComponent<RedEnemy>();
            Vector3 dir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;

            if (dir != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

            animator.SetBool("isAttack", true);
            enemy.OnDamage(damage);
            yield return new WaitForSeconds(1f);
            animator.SetBool("isAttack", false);

        }

        //if (movePoint != null)
        //{
        //    Vector3 dir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;
        //    if (dir != Vector3.zero)
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        //}

        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            Move();
        }
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
