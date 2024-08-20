using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

[RequireComponent(typeof(NetworkTransform))]
public class CarNetwork : NetworkBehaviour
{
    void Start()
    {
        if (!IsOwner)
        {
            GetComponent<CarController>().enabled = false;
        }
    }
}
