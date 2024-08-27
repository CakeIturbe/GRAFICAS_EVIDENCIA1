using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float currentHealth;
    public TextMeshProUGUI healthText;
     // Referencia al texto UI para mostrar vida
    public GameObject GameOverPanel; // Panel to show when player dies

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        
        UpdateHealthUI();
    }

    private void Die()
    {
        // Lógica para cuando el jugador muere
        Debug.Log("Jugador ha muerto");

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }

    }

    // Method to heal
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Character healed by " + amount + ", current health: " + currentHealth);
    }
    
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Player HP: " + currentHealth;
        }
    }

    // Detect collisions with enemy bullets
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }


}
