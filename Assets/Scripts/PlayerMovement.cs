using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Rigidbody2D body;

    private Animator animator;

    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            this.grounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player horizontal
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-.5f, .5f, 1);
        }

        if (CanJump())
        {
            Jump();
        }

        //set animation parameters
        this.animator.SetBool("run", horizontalInput != 0);
        this.animator.SetBool("grounded", this.grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        this.animator.SetTrigger("jump");
        this.grounded = false;
    }

    private bool CanJump()
    {
        return Input.GetKey(KeyCode.Space) && this.grounded;
    }

}
