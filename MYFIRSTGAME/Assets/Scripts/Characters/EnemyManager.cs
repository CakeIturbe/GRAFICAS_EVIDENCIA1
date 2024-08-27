using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this if you are using UI elements

public class EnemyManager : MonoBehaviour{

    public TextMeshProUGUI enemyCounterText; // Reference to the UI Text component

    private int activeEnemyCount = 0; // Counter for active Enemys

    // Method to increment the Enemy count
    public void IncrementEnemyCount()
    {
        activeEnemyCount++;
        UpdateEnemyCounter();
    }

    // Method to decrement the Enemy count
    public void DecrementEnemyCount()
    {
        activeEnemyCount--;
        UpdateEnemyCounter();
    }

    // Method to update the UI Text with the current Enemy count
    private void UpdateEnemyCounter()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Active Enemys: " + activeEnemyCount;
        }
    }
}


