using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumperPower = 6f;
    private bool isFacingRight = false;
    public Animator animator;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30;


    public GameObject player;
    public Transform respawnPosition;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isFacingRight)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + new Vector3(.5f, 0, 0), bulletSpawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position - new Vector3(.5f, 0, 0), bulletSpawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletSpawnPoint.right * -1 * bulletSpeed, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumperPower);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            player.transform.position = respawnPosition.position;
        }
        if (collision.gameObject.CompareTag("EnemySkull"))
        {
            player.transform.position = respawnPosition.position;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = 4f;
                    rb.velocity = velocity;
                    Destroy(collision.gameObject);

                }
                else
                {
                    player.transform.position = respawnPosition.position;
                }
            } 
        } 
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Kill()
    {
        player.transform.position = respawnPosition.position;
    }
}
