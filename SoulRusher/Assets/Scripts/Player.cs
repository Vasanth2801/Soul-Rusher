using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] bool isJumping = false;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    PlayerController controller;

    [Header("Inputs")]
    Vector2 moveInput;

    void Awake()
    {
        controller = new PlayerController();
        Movement();
    }

    void Movement()
    {
        controller.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controller.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        controller.Player.Enable();
    }

    void OnDisable()
    {
        controller.Player.Disable();
    }

    
    

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 move = rb.position + moveInput * speed * Time.deltaTime;
        rb.MovePosition(move);
    }
}