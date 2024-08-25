using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotMovement : MonoBehaviour
{
    // Reference to the PlayerController's transform
    public Transform playerControllerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Match the x and z positions of the PlayerController
        if (playerControllerTransform != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = playerControllerTransform.position.x;
            newPosition.z = playerControllerTransform.position.z;
            transform.position = newPosition;
        }
    }
}