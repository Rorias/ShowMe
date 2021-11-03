using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public GameObject prefabEraser;

    private Rigidbody2D rb2d;
    private BoxCollider2D groundCollider;
    private SpriteRenderer sprite;

    public float moveSpeed;
    private float baseMoveSpeed;

    private float dashTimer = 0.0f;
    private float maxDashTime = 0.25f;

    private float dashCooldownTimer = 0.0f;
    public float maxDashCooldownTime;

    private bool grounded = false;
    private bool jumped = false;
    private bool doubleJumped = false;
    private bool dashing = false;
    private bool dashed = false;
    private bool airDashed = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        groundCollider = GetComponents<BoxCollider2D>().First(x => x.isTrigger);

        baseMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        CheckGroundCollision();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            StartDash();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopMove();
        }
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            dashTimer += Time.fixedDeltaTime;

            Dash();

            if (dashTimer >= maxDashTime)
            {
                dashTimer = 0.0f;
                StopDash();
            }
        }

        if (dashed)
        {
            dashCooldownTimer += Time.fixedDeltaTime;

            if (dashCooldownTimer >= maxDashCooldownTime)
            {
                dashCooldownTimer = 0.0f;
                dashed = false;
            }
        }
    }

    private void CheckGroundCollision()
    {
        ContactFilter2D cf2d = new ContactFilter2D();
        List<Collider2D> collisions = new List<Collider2D>();

        grounded = groundCollider.OverlapCollider(cf2d, collisions) > 1;

        if (grounded)
        {
            airDashed = false;
            doubleJumped = false;
            jumped = false;
        }

        Debug.Log(grounded);
    }

    private void Jump()
    {
        if (grounded && !jumped)
        {
            jumped = true;
            rb2d.AddForce(new Vector2(0, 800));
        }
        else if (!grounded && !doubleJumped)
        {
            doubleJumped = true;
            airDashed = false;
            Instantiate(prefabEraser, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0, 700));
        }
    }

    private void StartDash()
    {
        if (grounded && !dashing && !dashed)
        {
            dashing = true;
            moveSpeed *= 3f;
        }
        else if (!grounded && !dashing && !airDashed)
        {
            dashing = true;
            airDashed = true;
            moveSpeed *= 4f;
        }
    }

    private void Dash()
    {
        if (sprite.flipX)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    private void StopDash()
    {
        dashed = true;
        dashing = false;

        moveSpeed = baseMoveSpeed;

        StopMove();
    }

    private void MoveLeft()
    {
        if (!dashing)
        {
            sprite.flipX = false;
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
    }

    private void MoveRight()
    {
        if (!dashing)
        {
            sprite.flipX = true;
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
    }

    private void StopMove()
    {
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
}
