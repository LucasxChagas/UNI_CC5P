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

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask groundLayer; 

    [Header("Health Controller")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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

    private bool IsGrounded()
    {
        // Verificamos se o personagem está tocando o chão usando um círculo de verificação
        return Physics2D.OverlapCircle(this.transform.position + groundCheckOffset, groundCheckRadius, groundLayer);
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
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + groundCheckOffset, groundCheckRadius);
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        Debug.Log($"Player recebeu {amount} de dano. Vida Atual: {currentHealth}");
    }
}
