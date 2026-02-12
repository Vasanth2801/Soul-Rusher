using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerMovement Settings")]
    [SerializeField] float speed = 5f;            
    [SerializeField] float jumpForce = 9f;
    [SerializeField] int facingDirection = 1;
    float moveInputX;                    

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] bool isGrounded = false;

    [Header("Attack Settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    public LayerMask playerLayer;
    [SerializeField] int attackDamage = 10;

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");

        Jump();

        if (moveInputX > 0 && transform.localScale.x < 0 || moveInputX < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        HandleAnimations();
    }

    void FixedUpdate()
    {
        Move();

        Attack();
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

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, checkRadius, playerLayer);

            foreach(Collider2D hit in hitEnemies)
            {
                var eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(10);
                    Debug.Log("Damage done to enemy ");
                }
            }
        }
    }

    void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInputX) > 0.1f && isGrounded;

        animator.SetBool("isIdle",!isMoving && isGrounded);
        animator.SetBool("isRunning",isMoving && isGrounded);

        animator.SetBool("isJumping",rb.linearVelocity.y > 0.1);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
    }
}