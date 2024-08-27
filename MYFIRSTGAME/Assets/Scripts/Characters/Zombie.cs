using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 2f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime;
    
    private Animator animator; // Assuming you have animations

    private EnemyManager enemyManager; // Reference to the BulletManager


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.IncrementEnemyCount();
        }
    }

    void Update()
    {
        if (player != null)
        {
            FollowPlayer();

            if (IsPlayerInRange())
            {
                AttackPlayer();
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep movement on the horizontal plane
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        transform.position += transform.forward * speed * Time.deltaTime;

        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    private void AttackPlayer()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            // Assuming the player has a script with a TakeDamage method
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }

            if (animator != null)
            {
                animator.SetTrigger("attack");
            }
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void DestroyZombie()
    {
        if (enemyManager != null)
        {
            enemyManager.DecrementEnemyCount();
        }
        Destroy(gameObject);
    }
}
