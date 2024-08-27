using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{ 
    public float moveSpeed = 5f;
    public float slowSpeed = 2f; // Speed when moving slower
    public KeyCode slowDownKey = KeyCode.LeftShift; // Key to toggle slower movement

    private Rigidbody rb;
    private float currentMoveSpeed;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float fireTimer;
    private BulletManager bulletManager; // Reference to the BulletManager


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletManager = FindObjectOfType<BulletManager>();
        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        // Check if the slow down key is pressed
        if (Input.GetKey(slowDownKey))
        {
            currentMoveSpeed = slowSpeed;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        Move();

        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * currentMoveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    private void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        
        if (bulletManager != null)
        {
            bulletManager.IncrementBulletCount();
        }
    }

}
