using UnityEngine;
using Unity.Cinemachine;

public class PlayerScript : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Referências")]
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private Animator anim;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 playerInputs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Pega os inputs
        GetInputs();

        // Calcula a direção do movimento
        CalculateMoveDirection();

        //Atualiza Animator
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // Move o jogador
        MovePlayer();
    }

        private void GetInputs()
    {
        playerInputs = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void CalculateMoveDirection()
    {
        // Pega a direção da câmera
        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0; // Ignora a altura da câmera
        cameraForward.Normalize();

        // Pega a direção do movimento
        Vector3 cameraRight = virtualCamera.transform.right;
        cameraRight.y = 0; // Ignora a altura da câmera
        cameraRight.Normalize();

        moveDirection = (cameraForward * playerInputs.z + cameraRight * playerInputs.x).normalized;

        // Rotaciona o jogador na direção do movimento
        RotatePlayer(moveDirection);
    }

    private void MovePlayer()
    {
        // Aplica movimento usando velocity
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.linearVelocity.y; // Mantém a velocidade vertical (gravidade)
        rb.linearVelocity = velocity;
    }

    private void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        if(rb.linearVelocity.magnitude > 0.1f)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
}