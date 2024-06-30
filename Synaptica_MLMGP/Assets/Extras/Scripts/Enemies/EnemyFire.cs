using System.Collections;
using UnityEngine;

namespace cowsins
{
    public class EnemyFire : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public GameObject AoePrefab;
        public GameObject AoeVFX;
        public float projectileSpeed = 50f;
        public Transform firePoint; // The new fire point
        public Transform playerPos;
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
        public void AoeShoot()
        {
            StartCoroutine(AoeShootCoroutine());
        }

        private IEnumerator AoeShootCoroutine()
        {
            if (playerPos != null)
            {
                Vector3 initialPlayerPosition = playerPos.position;
                Quaternion initialPlayerRotation = playerPos.rotation;
                Quaternion fixedRotation = Quaternion.Euler(90, 0, 0);
                // Instantiate projectileVFX
                GameObject projectileVFX = Instantiate(AoeVFX, playerPos.position , fixedRotation);

                // Add logic for projectileVFX if needed

                // Wait for 1 second
                yield return new WaitForSeconds(0.8f);

                // Instantiate projectile
                GameObject projectile = Instantiate(AoePrefab, initialPlayerPosition, initialPlayerRotation);

                // Add logic for projectile if needed

                // Destroy the projectile after 1 second
                Destroy(projectile, 1.0f);
                Destroy(projectileVFX, 1.0f);
                Debug.Log("AOE Shot");
            }
        }
    }

}
