using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 3f;
    public CharacterController controller;
    public Vector3 movePoint;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        camera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, movePoint * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                movePoint = hitInfo.point;
            }
        }

        if (movePoint != null)
        {
            Vector3 dir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        }

        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            Move();
        }

    }

    void Move()
    {

        Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;

        controller.SimpleMove(thisUpdatePoint);
    }
}
