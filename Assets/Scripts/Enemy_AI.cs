//Kevin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 2f;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    [Header("Range")]
    [SerializeField] private float SightRange = 20f;
    [SerializeField] private float DetectionRange = 20f;

    public Rigidbody rb;
    public GameObject Target;

    private bool seePlayer;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;
    

    void Start()
    {
    
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, (int)whatIsGround);
        //detect any players in range

        if (isGrounded)
        {
            if (!seePlayer)
            {
                hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
                foreach (var hitCollider in hitColliders)
                {
                    if(hitCollider.tag == "Player")
                    {
                        Target = hitCollider.gameObject;
                        seePlayer = true;
                    }
                }
            }
            else
            {
                if(Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange))
                {
                    if(Hit.collider.tag != "Player")
                    {
                        seePlayer = false;
                    }
                    else
                    {
                        //calculate the direction
                        var Heading = Target.transform.position - transform.position;
                        var Distance = Heading.magnitude;
                        var Direction = Heading/Distance;

                        //move enemy towards player
                        Vector3 Move = new Vector3(Direction.x * speed, 0, Direction.z * speed);
                        rb.velocity = Move;
                        transform.forward = Move;
                    }
                }
            }
        }

        
    }
}
