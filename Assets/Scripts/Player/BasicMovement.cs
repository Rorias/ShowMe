using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public GameObject prefabEraser;
    public GameObject prefabDashGhost;

    private Rigidbody2D rb2d;
    private BoxCollider2D groundCollider;
    private SpriteRenderer sprite;

    public float moveSpeed;
    private float baseMoveSpeed;

    private float prevDashGhostPosX;
    private float dashTimer = 0.0f;
    private float maxDashTime = 0.3f;
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
        StopMove();

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
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            Dash();
            SetDashGhost();

            dashTimer += Time.fixedDeltaTime;

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

    private void SetDashGhost()
    {
        if (sprite.flipX)
        {
            if (prevDashGhostPosX + (transform.localScale.x / 2) <= transform.position.x)
            {
                CreateDashGhost(new Vector2(prevDashGhostPosX + (transform.localScale.x / 2), transform.position.y));
            }
        }
        else
        {
            if (prevDashGhostPosX - (transform.localScale.x / 2) >= transform.position.x)
            {
                CreateDashGhost(new Vector2(prevDashGhostPosX - (transform.localScale.x / 2), transform.position.y));
            }
        }
    }

    private void CreateDashGhost(Vector2 _pos)
    {
        GameObject ghost = Instantiate(prefabDashGhost, _pos, Quaternion.identity);
        ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
        prevDashGhostPosX = _pos.x;

        SetDashGhost();
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
            Instantiate(prefabEraser, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0, 700));
        }
    }

    private void StartDash()
    {
        if (grounded && !dashing && !dashed)
        {
            dashing = true;
            moveSpeed *= 2f;
            prevDashGhostPosX = transform.position.x;
        }
        else if (!grounded && !dashing && !airDashed)
        {
            dashing = true;
            prevDashGhostPosX = transform.position.x;
            airDashed = true;
            moveSpeed *= 3f;
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
