using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerMovement Settings")]
    [SerializeField] float speed = 5f;            
    [SerializeField] float jumpForce = 9f;
    [SerializeField] int facingDirection = 1;
    float moveInputX;                    

    [Header("References")]
    private Rigidbody2D rb;                          
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] bool isGrounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                        
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");

        Jump();

        if (moveInputX > 0 && transform.localScale.x < 0 || moveInputX < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Move();                      
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
    }
}