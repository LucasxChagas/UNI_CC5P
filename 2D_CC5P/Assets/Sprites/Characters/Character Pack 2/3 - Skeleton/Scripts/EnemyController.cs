using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private EnemyAnimatorController enemyAnimatorController;
    [SerializeField] private float attackInterval = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Attack Range Area")]
    [SerializeField] private float attackRangeRadius = 3f;
    [SerializeField] private LayerMask playerLayer;
    private bool playerInRange = false;

    [Header("Pooling Settings")]
    public int poolSize = 5;
    private Queue<Projectile> projectilePool = new Queue<Projectile>();

    private void Start() 
    {
        StartCoroutine(AttackRoutine());

        // Initialize the projectile pool
        for (int i = 0; i < poolSize; i++)
        {
            Projectile projectile = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.SetEnemyReference(this); // Set the reference to this EnemyController
            projectile.gameObject.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    public void InstantiateBone()
    {
        GetProjectileFromPool();
    }

    private void GetProjectileFromPool()
    {
        Projectile projectile;
        if (projectilePool.Count > 0)
        {
            projectile = projectilePool.Dequeue();
            projectile.gameObject.SetActive(true);
        }
        else
        {
            projectile = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.SetEnemyReference(this);
        }
        projectile.transform.position = transform.position; // Posiciona o projétil na posição do inimigo
    }

    public void ReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePool.Enqueue(projectile);
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
