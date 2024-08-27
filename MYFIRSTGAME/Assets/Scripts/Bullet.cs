using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    public float damage = 10f; // Example damage value


    private BulletManager bulletManager; // Reference to the BulletManager
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
        // Find the BulletManager in the scene
        bulletManager = FindObjectOfType<BulletManager>();
    }

    private void Update()
    {
        // Move the bullet forward according to its rotation
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        // Notify the BulletManager that this bullet is being destroyed
        if (bulletManager != null)
        {
            bulletManager.DecrementBulletCount();
        }
        Destroy(gameObject);
    }
}
