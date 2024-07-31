using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isGrounded = true;
    public float jumpForce = 4.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
        
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the player
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement * Time.deltaTime * speed);

        // Jump function
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    // Stop the player from double jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}