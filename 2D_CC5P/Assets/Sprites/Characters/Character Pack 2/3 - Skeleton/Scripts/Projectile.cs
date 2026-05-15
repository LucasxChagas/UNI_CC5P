using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private EnemyController enemyController;
    private Coroutine returnToPullRoutine;
    private float projectileDirection = 1;
    // Update is called once per frame
    void OnEnable()
    {
        returnToPullRoutine = StartCoroutine(ReturnToPoolAfterDelay());
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        enemyController.ReturnProjectileToPool(this);
    }

    void Update()
    {
        transform.Translate((Vector2.right * projectileDirection) * speed * Time.deltaTime);
    }

    public void SetEnemyReference(EnemyController enemyController)
    {
        this.enemyController = enemyController;
        projectileDirection = enemyController.transform.localScale.x;
    }

    void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if(playerCollider.CompareTag("Player"))
        {
            StopCoroutine(returnToPullRoutine); // Stop the coroutine to prevent it from returning to the pool after a delay
            enemyController.ReturnProjectileToPool(this);
            //Causar Dano ao Player
            playerCollider.GetComponent<Player>().TakeDamage(this.transform.position - playerCollider.transform.position);
        }
    }
}
