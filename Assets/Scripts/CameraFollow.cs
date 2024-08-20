using UnityEngine;
using Unity.Netcode;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 1.0f;
    private Transform target;

    void LateUpdate()
    {
        if (target == null)
        {
            FindLocalPlayer();
        }

        if (target != null)
        {
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target.position + target.forward * 2f);
        }
    }

    void FindLocalPlayer()
    {
        foreach (var player in FindObjectsOfType<NetworkBehaviour>())
        {
            if (player.IsOwner)
            {
                target = player.transform;
                break;
            }
        }
    }
}
