using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpStrength = 7;
    float moveInput;

    public int airJumps = 1;
    private int jumpsLeft;

    bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    Rigidbody2D rb;
    Animator anim;

    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        jumpsLeft = airJumps;
    }

    private void Update()
    {
        if (GameManager.gameIsOn)
        {
            if (GameManager.playerAbleToMove)
            {
                moveInput = Input.GetAxisRaw("Horizontal");

                if (Input.GetKeyDown(KeyCode.W) && isOnGround == true)
                {
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.W) && jumpsLeft > 0)
                {
                    Jump();
                    jumpsLeft -= 1;
                }
            }
            else
            {
                moveInput = 0;
            }
            
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
 
            if (isOnGround == true)
            {
                jumpsLeft = airJumps;
                anim.SetBool("Jumping", false);
            }
            else
            {
                anim.SetBool("Jumping", true);
            }

            if (transform.position.y <= -10)
            {
                GameManager.Instance.TakeLives(1);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameIsOn)
        {
            isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
       } 
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);

        anim.SetBool("Jumping", true);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
