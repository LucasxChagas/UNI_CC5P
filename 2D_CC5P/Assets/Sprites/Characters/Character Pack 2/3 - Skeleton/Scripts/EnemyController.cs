using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private EnemyAnimatorController enemyAnimatorController;
    [SerializeField] private float attackInterval = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InstantiateBone()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            enemyAnimatorController.Attack();
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
