using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy_AI : MonoBehaviour
{
    [Header("Speed")]
    public float maxSpeed;
    private float speed;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    [Header("Range")]
    [SerializeField] private float SightRange = 10f;
    [SerializeField] private float DetectionRange = 20f;

    public Rigidbody rb;
    public GameObject Target;
    private bool isCollidingWithPlayer = false;

    private bool seePlayer;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private float delayTime = 1f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    void Start()
    {
        // Initialization code here
    }

    void Update()
    {
        DetectPlayer();
        if (seePlayer && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SightRange);
        seePlayer = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Target = hitCollider.gameObject;
                seePlayer = true;
                break;
            }
        }
    }

    void Shoot()
    {
        if (Target != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (Target.transform.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (!isCollidingWithPlayer)
            {
                isCollidingWithPlayer = true;
                StartCoroutine(DamagePlayerOverTime(collision.collider.gameObject.GetComponent<Player_Stats>()));
            }
        }
        if (collision.collider.tag == "PlayerProjectile")
        {
            gameObject.GetComponent<Enemy_Health>().takeDamage(damage);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            isCollidingWithPlayer = false;
        }
    }

    //adds delay to enemy attack
    private IEnumerator DamagePlayerOverTime(Player_Stats playerStats)
    {
        while (isCollidingWithPlayer)
        {
            playerStats.takeDamage(damage);
            yield return new WaitForSeconds(delayTime);
        }
    }
}