using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private float direction = 1;
    private bool poweredUp = false;
    private bool isFacingRight = true;
    private Animator anim;

    public Transform bulletSpawnLocation;
    public GameObject bulletPrefab;

    // references
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        // jumping mechanic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            Jumping();
        }

        Running();

        Jumping();

        ShootBullets();

        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power Up"))
        {
            poweredUp = true;
        }
    }



    // ground checker
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }



    // running animation
    private void Running()
    {
        if (horizontal > 0f)
        {
            anim.SetBool("Running", true);
        }

        else if (horizontal < 0f)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

    }

    private void Jumping()
    {
        if (IsGrounded())
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }
    }

    // flipping sprite
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            direction *= -1;
        }

    }

    // bullet mechanics
    void ShootBullets()
    {
        if (poweredUp == true)
        {


            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().speed *= direction;
                Destroy(bullet, 3f);
            }
        }

    }
}
