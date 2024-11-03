using UnityEngine;
using Google.XR.Cardboard;

public class CardboardTeleportation : MonoBehaviour
{
    private TeleportationSpot currentTeleportSpot;
    public Camera cam;
    public LayerMask teleportMask;

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        HandleGaze();
        HandleTrigger();
    }

    private void HandleGaze()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportMask))
        {
            TeleportationSpot newSpot = hit.collider.GetComponent<TeleportationSpot>();


            if (
                transform.position != AdjustTeleportPosition(newSpot.target.position) && // make sure we are not already at the new gazed spot
                newSpot != null &&
                newSpot != currentTeleportSpot)
            {
                // Exit previous spot
                if (currentTeleportSpot != null) currentTeleportSpot.OnGazeExit();

                // Enter new spot
                currentTeleportSpot = newSpot;
                currentTeleportSpot.OnGazeEnter();
            }
        }
        else if (currentTeleportSpot != null)
        {
            currentTeleportSpot.OnGazeExit();
            currentTeleportSpot = null;
        }
    }


    private void HandleTrigger()
    {
        if (currentTeleportSpot != null && Api.IsTriggerPressed) Teleport();
    }

    private void Teleport()
    {
        // assign teleport location to the upper middle of the teleport cuboid (we dont wanna be teleported to the ground)
        Vector3 position = AdjustTeleportPosition(currentTeleportSpot.target.position);

        transform.position = position;
        currentTeleportSpot.OnGazeExit();
        currentTeleportSpot = null;
    }

    private Vector3 AdjustTeleportPosition(Vector3 position)
    {
        return new Vector3(position.x, position.y + 1.7f, position.z);
    }
}