using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    private BulletManager bulletManager; // Reference to the BulletManager

    // Start is called before the first frame update
    void Start()
    {
        // Find the BulletManager in the scene
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (spawnerType == SpawnerType.Spin) 
        {
            transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        if (timer >= firingRate) 
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire() 
{
    if (bullet) 
    {
        spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<Bullet>().speed = speed;
        spawnedBullet.transform.rotation = transform.rotation;

        // Notify the BulletManager to increment the active bullet count
        if (bulletManager != null)
        {
            bulletManager.IncrementBulletCount();
        }
    }
}

}

