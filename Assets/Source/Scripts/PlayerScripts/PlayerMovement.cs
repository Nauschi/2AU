using Assets.Source.GameSettings;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer rend;
    public bool isFrozen = false;
    public float freezeTime = 0f;

    Vector2 movement;

    public enum PlayerDirection
    {
        DOWN = 1,
        UP = 2,
        SIDE = 3,
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            if(freezeTime + GameConstants.TRAP_FREEZETIME > Time.time)
            {
                return;
            }
            else
            {
                isFrozen = false;
                freezeTime = 0f;
                FreezetimeCountdown cntDown = gameObject.GetComponent<FreezetimeCountdown>();
                cntDown.textDisplay.SetActive(false);
                Debug.Log("Freezetime over!");
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10;
        } else
        {
            moveSpeed = 5;
        }

        animator.SetFloat(AnimatorConstant.Horizontal.Name, movement.x);
        animator.SetFloat(AnimatorConstant.Vertical.Name, movement.y);
        animator.SetFloat(AnimatorConstant.Speed.Name, movement.sqrMagnitude);

        if(movement.x != 0)
        {
            animator.SetInteger(AnimatorConstant.LastInput.Name, (int) PlayerDirection.SIDE);
        } else if (movement.y < 0)
        {
            animator.SetInteger(AnimatorConstant.LastInput.Name, (int) PlayerDirection.DOWN);
        } else if (movement.y > 0)
        {
            animator.SetInteger(AnimatorConstant.LastInput.Name, (int) PlayerDirection.UP);
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
        if (!isFrozen)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetFloat(AnimatorConstant.Speed.Name, 0f);
            rb.MovePosition(rb.position);
        }

    }
}
