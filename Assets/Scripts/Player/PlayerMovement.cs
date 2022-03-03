
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private const string HORIZONTAL_AXIS = "Horizontal";
    [SerializeField] private float speed;

    [SerializeField] private float jumpPower;

    [SerializeField] private float climbPower;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private LayerMask wallLayerMask;


    private float wallJumpCoolDown;
    private float horizontalInput;

    private Rigidbody2D rigidBody2D;

    private Animator animator;

    private BoxCollider2D boxCollider2D;


    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        this.horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
        rigidBody2D.velocity = new Vector2(this.horizontalInput * speed, rigidBody2D.velocity.y);

        //Flip player horizontal
        if (this.horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else if (this.horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1);
        }


        //set animation parameters
        this.animator.SetBool("run", this.horizontalInput != 0);
        this.animator.SetBool("grounded", this.IsGrounded());

        //Wall jump logic
        if (wallJumpCoolDown > 0.2f)
        {


            rigidBody2D.velocity = new Vector2(this.horizontalInput * speed, rigidBody2D.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                this.rigidBody2D.gravityScale = 0;
                this.rigidBody2D.velocity = Vector2.zero;
            }
            else
            {
                this.rigidBody2D.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            this.wallJumpCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpPower);
            this.animator.SetTrigger("jump");
        }
        else if (OnWall() && !IsGrounded())
        {

            if (horizontalInput == 0)
            {

                this.rigidBody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 20, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                this.rigidBody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * climbPower, climbPower * 2);
            }

            this.wallJumpCoolDown = 0;

        }
    }

    private bool IsGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider2D.bounds.center, this.boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayerMask);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider2D.bounds.center, this.boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayerMask);
        return raycastHit.collider != null;
    }
    public bool CanAttack()
    {
        return this.horizontalInput == 0 && IsGrounded() && !OnWall();
    }

}
