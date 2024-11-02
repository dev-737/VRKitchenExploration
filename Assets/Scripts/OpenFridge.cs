using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{


    public Transform door;
    public float rotationSpeed = 50.0f; // Speed of rotation
    private bool isOpen = false;

    private Quaternion doorClosedRotation;
    private Quaternion doorOpenRotation;
    private Quaternion targetRotation;

    void Start()
    {
        // Store the starting rotation of the door (closed state)
        doorClosedRotation = door.rotation;
        // Define the open rotation
        doorOpenRotation = doorClosedRotation * Quaternion.Euler(0, -80, 0);
        targetRotation = doorClosedRotation; // Start with the door closed
    }

    void Update()
    {
        // Smoothly rotate towards the target rotation
        door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void TiltInteract()
    {
        // Toggle the open/close state
        isOpen = !isOpen;
        targetRotation = isOpen ? doorOpenRotation : doorClosedRotation;
    }

}