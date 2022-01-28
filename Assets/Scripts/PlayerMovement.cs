using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Rigidbody rb;
    Vector3 movement;
    Vector3 move;

    // Start is called before the first frame update
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.z = Input.GetAxisRaw("Vertical") * speed;



        move = transform.right * movement.x + transform.forward * movement.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}
