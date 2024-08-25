using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Controller_Extended : MonoBehaviour
{
    public float moveDistance = 1f; // Distance to move per step
    public float rotationAngle = 90f; // Angle to rotate per step
    public float moveDuration = 0.5f; // Duration of the move

    private bool isMoving = false;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(Move(Vector3.forward, Vector3.right));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(Move(Vector3.back, Vector3.left));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(Move(Vector3.left, Vector3.forward));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(Move(Vector3.right, Vector3.back));
            }
        }
    }

    IEnumerator Move(Vector3 direction, Vector3 rotationAxis)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction * moveDistance;

        // Calculate the pivot point for the rotation
        Vector3 pivot = startPosition + (direction + Vector3.down) * (moveDistance / 2);

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(rotationAngle, rotationAxis) * startRotation;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.RotateAround(pivot, rotationAxis, rotationAngle * Time.deltaTime / moveDuration);

            yield return null;
        }

        // Ensure final position and rotation are set correctly
        transform.position = endPosition;
        transform.rotation = endRotation;

        isMoving = false;
    }
}