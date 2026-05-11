using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;

    private Vector2 movementInputs;
    private Rigidbody2D rb;

    private bool IsWalking = false;
    private bool IsJumping = false;
    private bool IsFalling = false;
    private bool IsOnTheGround = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
        UpdateAnimator();
        UpdateOrientation();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInputs()
    {
        // Mudamos os Inputs para mover somente na horizontal, e mantemos a velocidade do personagem em Y;
        movementInputs = new Vector2(Input.GetAxisRaw("Horizontal"), rb.linearVelocity.y);
    }

    private void Move()
    {
        // Acrescentamos velocidade somente em X, e mantemos a velocidade atual do personagem em Y;
        rb.linearVelocity = new Vector2(movementInputs.x * movementSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Mantemos a velocidade atual do personagem em X, e definimos a velocidade em Y com a força do pulo;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void UpdateAnimator()
    {
        if(rb.linearVelocity.y > 0.1f)
        {
            IsJumping = true;
            IsFalling = false;
        }
        else if(rb.linearVelocity.y < -0.1f)
        {
            IsJumping = false;
            IsFalling = true;
        }
        else
        {
            IsJumping = false;
            IsFalling = false;

            if(Mathf.Abs(rb.linearVelocity.x) > 0.1f)
            {
                IsWalking = true;
            }
            else
            {
                IsWalking = false;
            }
        }

        animator.SetBool("IsWalking", IsWalking);
        animator.SetBool("IsJumping", IsJumping);
        animator.SetBool("IsFalling", IsFalling);
    }

    private void UpdateOrientation()
    {
        // Via Rotation...
        if (movementInputs.x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if(movementInputs.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
