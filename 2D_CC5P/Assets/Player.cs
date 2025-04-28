using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;

    private Vector2 movementInputs;
    private Rigidbody2D rb;

    private bool IsWalking;
    private bool IsOnTheGround;

    // Configuração para estados "IsOnTheAir"
    private int IsOnTheAirValue;

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
        if (rb.linearVelocity.magnitude > 0) // Está andando...
            IsWalking = true;
        else // Está parado...
            IsWalking = false;

        // Atualização aérea
        if (rb.linearVelocity.y > 0.1f && !IsOnTheGround)
            IsOnTheAirValue = 1;
        else if (rb.linearVelocity.y < -0.1f && !IsOnTheGround)
            IsOnTheAirValue = -1;
        else
            IsOnTheAirValue = 0;

        // Atualiza os parâmetros no Animator, certifique-se de tê-los criado
        animator.SetBool("IsWalking", IsWalking);
        animator.SetInteger("IsOnTheAir", IsOnTheAirValue);
    }

    private void UpdateOrientation()
    {
        // Via Rotation...
        if (movementInputs.x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if(movementInputs.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Certifique-se de criar a tag e atribuir ao nosso objeto "Ground"
        if(collision.gameObject.CompareTag("Ground"))
        {
            // Se o personagem colide com o chão, então atualiza a variável;
            IsOnTheGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Se o personagem deixa de colidir com o chão, então atualiza a variável;
            IsOnTheGround = false;
        }
    }
}
