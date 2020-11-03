using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer rend;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10;
        } else
        {
            moveSpeed = 5;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x != 0)
        {
            animator.SetInteger("LastInput", 3);
        } else if (movement.y < 0)
        {
            animator.SetInteger("LastInput", 1);
        } else if (movement.y > 0)
        {
            animator.SetInteger("LastInput", 2);
        }

        if (movement.x < 0) {
            rend.flipX = true;
        } else if (movement.x > 0)
        {
            rend.flipX = false;
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
