using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class SpeedBoost : NetworkBehaviour
{
    public float boostAmount = 20f;
    public float duration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if ((IsServer || IsClient) && other.CompareTag("Player"))
        {
            CarController player = other.GetComponent<CarController>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
        }
    }

    private IEnumerator ApplySpeedBoost(CarController player)
    {
        player.moveSpeed += boostAmount;
        UpdateSpeedClientRpc(player.GetComponent<NetworkObject>().NetworkObjectId, player.moveSpeed);
        yield return new WaitForSeconds(duration);
        player.moveSpeed -= boostAmount;
        UpdateSpeedClientRpc(player.GetComponent<NetworkObject>().NetworkObjectId, player.moveSpeed);
    }

    [ClientRpc]
    private void UpdateSpeedClientRpc(ulong playerId, float newSpeed, ClientRpcParams clientRpcParams = default)
    {
        if (IsOwner)
        {
            CarController player = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerId].GetComponent<CarController>();
            if (player != null)
            {
                player.moveSpeed = newSpeed;
            }
        }
    }

}
