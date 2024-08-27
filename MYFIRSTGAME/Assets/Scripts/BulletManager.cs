using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this if you are using UI elements

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Counter")]
    public TextMeshProUGUI bulletCounterText; // Reference to the UI Text component

    private int activeBulletCount = 0; // Counter for active bullets

    // Method to increment the bullet count
    public void IncrementBulletCount()
    {
        activeBulletCount++;
        UpdateBulletCounter();
    }

    // Method to decrement the bullet count
    public void DecrementBulletCount()
    {
        activeBulletCount--;
        UpdateBulletCounter();
    }

    // Method to update the UI Text with the current bullet count
    private void UpdateBulletCounter()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Active Bullets: " + activeBulletCount;
        }
    }
}

