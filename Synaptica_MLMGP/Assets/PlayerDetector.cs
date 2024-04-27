using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerDetector : MonoBehaviour
{
    public Transform firePoint; // The point where bullets get instantiated
    public GameObject bulletPrefab; // The bullet object to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash effect to play
    public float detectionDistance = 100f;
    public float bulletDamage = 10f; // The damage this bullet will do to the player
    public float bulletSpeed = 10f; // The speed of the bullet

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // Create a raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            // Check if the raycast hit a player
            if (hit.collider.CompareTag("Player"))
            {
                // Call the shoot function
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Instantiate bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Pass the damage and speed information to the bullet
        TurretProjectile turretProjectile = bullet.GetComponent<TurretProjectile>();
        if (turretProjectile != null)
        {
            turretProjectile.damage = bulletDamage;
            turretProjectile.speed = bulletSpeed;
            turretProjectile.dir = transform.forward;
           

        }

        // Instantiate muzzle flash at the fire point
        if (muzzleFlashPrefab != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(muzzleFlash, 0.1f); // Remove muzzle flash after a short time
        }
    }
}
