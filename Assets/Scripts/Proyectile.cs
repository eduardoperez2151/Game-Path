using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{

    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private float lifeTime;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hit)
        {
            return;
        }

        float movementSpeed = this.speed * Time.deltaTime * this.direction;
        transform.Translate(movementSpeed, 0, 0);

        this.lifeTime += Time.deltaTime;

        if (this.lifeTime > 5)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.hit = true;
        this.boxCollider2D.enabled = false;
        this.animator.SetTrigger("explode");
    }

    public void SetDirection(float direction)
    {
        this.lifeTime = 0;
        this.direction = direction;
        gameObject.SetActive(true);
        this.hit = false;
        this.boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
