using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtThis : TiltInteractable
{
  override public void Interact(RaycastHit hit)
  {
    // Set the position to hover in front of the camera
    Transform camTransform = Camera.main.transform;  // Reference to main camera's transform
    Vector3 hoverPosition = camTransform.position + camTransform.forward * 1.5f; // Hover 1.5 units in front

    // Update position and rotation for a hover effect
    transform.position = hoverPosition;
    transform.LookAt(camTransform);  // Rotate to face the player

    // Optional: disable gravity to keep it from falling
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
      rb.useGravity = false;
      rb.linearVelocity = Vector3.zero;  // Reset any existing velocity to prevent drift
    }
  }
  public void OnPointerEnter()
  {
    GetComponent<Renderer>().sharedMaterial.color = Color.cyan;
  }

  public void OnPointerExit()
  {
    GetComponent<Renderer>().sharedMaterial.color = Color.magenta;
  }
}