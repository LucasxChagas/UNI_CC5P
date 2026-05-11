using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float impactJumpForce = 10f;

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

    [Header("Invencibility Settings")]
    [SerializeField] private float invencibilityDuration = 2f;
    [SerializeField] private float flashInterval = 0.2f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInvencible = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForceX = 10f;
    [SerializeField] private float KnockbackForceY = 12.5f;
    [SerializeField] private float knockbackDuration = 0.5f;
    private bool isKnockedback = false;
    
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
        if(!isKnockedback)
        {
            Move();
        }
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

    public void TakeDamage(Vector2 knockbackDirection, int amount = 1)
    {
        if (!isInvencible)
        {
            currentHealth -= amount;
            isKnockedback = true;
            StartCoroutine(Knockback(knockbackDirection.magnitude > 0 ? Vector3.one : -Vector3.one));
            StartCoroutine(BecomeInvencible());
        }
    }

    private IEnumerator Knockback(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero; // Reseta a velocidade atual para evitar acumulo de forças
        rb.linearVelocity = new Vector2(-direction.x * knockbackForceX, direction.y * KnockbackForceY);
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedback = false;
    }

    private IEnumerator BecomeInvencible()
    {
        isInvencible = true;

        float elapsedTime = 0f;
        while (elapsedTime < invencibilityDuration)
        {
            // Flash Effect
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        isInvencible = false;
        spriteRenderer.enabled = true;
    }

    private void ImpactJumpEffect()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, impactJumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyHurtArea") && IsFalling)
        {

            collision.GetComponent<EnemyHurtArea>().TakeDamage();
            ImpactJumpEffect();
        }
    }
}
