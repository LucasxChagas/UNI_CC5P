using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private EnemyAnimatorController enemyAnimatorController;
    [SerializeField] private float attackInterval = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Attack Range Area")]
    [SerializeField] private float attackRangeRadius = 3f;
    [SerializeField] private LayerMask playerLayer;
    private bool playerInRange = false;

    public void InstantiateBone()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        PlayerRangeDetection();
    }

    private void PlayerRangeDetection()
    {
        // Verificamos se o jogador está dentro do raio de ataque usando um círculo de verificação
        playerInRange = Physics2D.OverlapCircle(this.transform.position, attackRangeRadius, playerLayer);
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if(playerInRange)
            {
                enemyAnimatorController.Attack();
            }
            
            yield return new WaitForSeconds(attackInterval);
        }
    }

    void OnDrawGizmos()
    {
        if(playerInRange)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);
    }
}
