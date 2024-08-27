using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Boss : MonoBehaviour
{
    public enum BossPhase { Phase0, Phase1, Phase2, Phase3 }
    public BossPhase currentPhase = BossPhase.Phase1;
    public float maxHealth = 1000f;
    public float currentHealth;
    private Animator animator;

    public TextMeshProUGUI healthText;
    public GameObject WinGamePanel; // Panel to show when player dies

    private EnemyManager enemyManager; // Reference to the BulletManager

    public GameObject phase0Objects; // Objects to activate in Phase 1

    public GameObject phase1Objects; // Objects to activate in Phase 1
    public GameObject phase2Objects; // Objects to activate in Phase 2
    public GameObject phase3Objects; // Objects to activate in Phase 3


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = gameObject.GetComponent<Animator>(); // Get the Animator component
        UpdateHealthUI();

        if (animator == null){
            Debug.LogWarning("Animator component not found!");

        }  

        if (WinGamePanel != null)
        {
            WinGamePanel.SetActive(false);
        }

        enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.IncrementEnemyCount();
        }
        animator.SetTrigger("Spawn");

    }


    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Character took " + damage + " damage, current health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthUI();
    }

    // Detect collisions with enemy bullets
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Monster HP: " + currentHealth;
        }
    }
    
    // Method called when health reaches 0
    private void Die()
{
    if (animator != null)
    {
        Debug.Log("Character died.");
        
        if (enemyManager != null)
        {
            enemyManager.DecrementEnemyCount();
        }

        animator.SetTrigger("Die");

        // Start the coroutine to handle post-death actions
        StartCoroutine(HandleDeathAfterAnimation());
    }
}

    // Coroutine to wait for the animation to finish before activating the panel
    private IEnumerator HandleDeathAfterAnimation()
    {
        // Wait for the length of the current animation state on layer 0
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        if (WinGamePanel != null)
        {
            WinGamePanel.SetActive(true);
        }
    }


    public void ToggleState()
    {
        animator.SetTrigger("Phase");

        switch (currentPhase)
        {

            case BossPhase.Phase0:
                ActivatePhase0();
                currentPhase = BossPhase.Phase1;
                break;
            case BossPhase.Phase1:
                ActivatePhase1();
                currentPhase = BossPhase.Phase2;
                break;
            case BossPhase.Phase2:
                ActivatePhase2();
                currentPhase = BossPhase.Phase3;
                break;
            case BossPhase.Phase3:
                ActivatePhase3();
                currentPhase = BossPhase.Phase0;
                break;
        }
    }

    private void ActivatePhase0()
    {
        // Activate Phase 1 objects
        phase0Objects.SetActive(true);
        phase1Objects.SetActive(false);
        phase2Objects.SetActive(false);
        phase3Objects.SetActive(false);

        Debug.Log("Boss entered Phase 1");
    }
    private void ActivatePhase1()
    {
        // Activate Phase 1 objects
        phase0Objects.SetActive(false);

        phase1Objects.SetActive(true);
        phase2Objects.SetActive(false);
        phase3Objects.SetActive(false);

        Debug.Log("Boss entered Phase 1");
    }

    private void ActivatePhase2()
    {
        // Activate Phase 2 objects
        phase0Objects.SetActive(false);
        phase1Objects.SetActive(false);
        phase2Objects.SetActive(true);
        phase3Objects.SetActive(false);

        Debug.Log("Boss entered Phase 2");
    }

    private void ActivatePhase3()
    {
        // Activate Phase 3 objects
        phase0Objects.SetActive(false);
        phase1Objects.SetActive(false);
        phase2Objects.SetActive(false);
        phase3Objects.SetActive(true);

        Debug.Log("Boss entered Phase 3");
    }

}
