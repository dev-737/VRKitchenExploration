using UnityEngine;

public class TiltInteract : MonoBehaviour
{
    public Transform cam;
    public Transform otherCrosshair;

    private bool canTriggerTilt = true;
    private float tiltCooldown = 0.5f; // Adjust this value as needed
    private float tiltTimer = 0f;

    void Update()
    {
        // Handle cooldown timer
        if (!canTriggerTilt)
        {
            tiltTimer += Time.deltaTime;
            if (tiltTimer >= tiltCooldown)
            {
                canTriggerTilt = true;
                tiltTimer = 0f;
            }
            return;
        }

        Vector3 target = cam.position + cam.forward * 2;

        transform.SetPositionAndRotation(target, cam.rotation);

        otherCrosshair.SetPositionAndRotation(target, cam.rotation);
        otherCrosshair.Rotate(0, 0, -45);

        // rotate original crosshair when head moves across z axis
        transform.Rotate(0, 0, -cam.rotation.eulerAngles.z);

        float angle = Quaternion.Angle(transform.rotation, otherCrosshair.rotation);

        if (angle <= 5 && Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 5))
        {
            hit.transform.gameObject.SendMessage("TiltInteract");
            canTriggerTilt = false; // Prevent immediate re-triggering
        }
    }
}