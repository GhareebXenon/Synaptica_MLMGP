using System.Collections;
using UnityEngine;

namespace cowsins
{
    public class EnemyFire : MonoBehaviour
    {
        [SerializeField, Header("References")] private bool displayGizmos = true;
        [SerializeField] private Animator animator;
        //[SerializeField, Tooltip("The part of the turret that rotates.")] private Transform turretHead;


        [SerializeField, Tooltip("Detection range for the player."), Header("Basic Settings")] private float detectionRange = 10f;
        [SerializeField, Tooltip("Enable vertical movement.")] private bool allowVerticalMovement = false;
        [SerializeField, Tooltip("Speed of rotation interpolation.")] private float lerpSpeed = 5f;


        private bool canShoot = false;
        [SerializeField, Header("Projectile Settings")] private GameObject projectilePrefab;
        [SerializeField, Min(0)] private float projectileSpeed, projectileDamage, projectileDuration;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject muzzleFlash; 
        [SerializeField, Tooltip("Shots per second."), Header("Shooting")] private float fireRate = 2f;
        private float fireCooldown = 0f;

        private Transform player;


        void Start()
        {
            // Gather initial references
            player = GameObject.FindWithTag("Player").transform;

            // Show a warning to the user. 
            if (player == null)
            {
                Debug.LogError("No player found. Make sure to tag the player object with 'Player' tag.");
            }
        }
        Vector3 targetDirection;
        Quaternion targetRotation;
        void Update()
        {
            if (player == null) return;

            // Calculate direction towards the player
            targetDirection = player.position - transform.position;
            if (!allowVerticalMovement) targetDirection.y = 0f;

            // Calculate the angle between the turret's forward direction and the direction towards the player
            float angle = Vector3.Angle(transform.forward, targetDirection);

            // Perform a Raycast towards the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection, out hit, detectionRange))
            {
                // Check if the raycast hit a player and the player is in front of the turret
                if (hit.collider.CompareTag("Player") && angle <= 45.0f)  // 45 degrees can be replaced with your desired angle range
                {
                    canShoot = true;
                    //targetRotation = Quaternion.LookRotation(targetDirection);
                    //turretHead.rotation = Quaternion.Lerp(turretHead.rotation, targetRotation, lerpSpeed * Time.deltaTime);
                    fireCooldown -= Time.deltaTime;
                    Fire();
                }
                else
                {
                    fireCooldown = fireRate;
                    canShoot = false;
                }
            }
        }

        void Fire()
        {
            // Only shoot if we are allowed to do it. 
            if (canShoot && fireCooldown <= 0)
            {
                if (animator != null) animator.SetTrigger("Fire");
                fireCooldown = fireRate;
                // Set the bullet's rotation to be the same as the turret's rotation
                TurretProjectile proj = Instantiate(projectilePrefab, firePoint.position, transform.rotation).GetComponent<TurretProjectile>();
                Instantiate(muzzleFlash, firePoint.position, targetRotation);
                proj.dir = -transform.forward;  // Set the bullet's direction to be the turret's forward direction
                proj.damage = projectileDamage;
                proj.speed = projectileSpeed;
                Destroy(proj.gameObject, projectileDuration);
            }
        }
        // Draw Gizmos
        void OnDrawGizmosSelected()
        {
            if (!displayGizmos) return;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1, .02f, 1));
            Gizmos.DrawSphere(Vector3.zero, detectionRange);
            Gizmos.matrix = oldMatrix;
        }

    }

}