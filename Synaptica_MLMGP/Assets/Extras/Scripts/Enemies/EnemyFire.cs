using System.Collections;
using UnityEngine;

namespace cowsins
{
    public class EnemyFire : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 50f;
        public Transform firePoint;  // The new fire point
        [SerializeField] private float bulletDamage = 10f;


        public void Shoot()
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;
            TurretProjectile turretProjectile = projectilePrefab.GetComponent<TurretProjectile>();
            turretProjectile.damage = bulletDamage;
            Destroy(projectile, 4.0f); 

            Debug.Log("Shot");
        }
    }
}