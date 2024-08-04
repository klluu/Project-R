using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private float _jumpForce = 300.0f;
    [SerializeField] private Rigidbody _rb;
        
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;
        movement.y = _rb.velocity.y;
        _rb.velocity = movement;

        // Jump function
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce);
            _isGrounded = false;
        }

    }

    // Stop the player from double jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}