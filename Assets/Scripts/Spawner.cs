using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private GameObject heroPrefab;
    
    [ServerRpc(RequireOwnership = false)]  
    public void SpawnHeroServerRpc(ulong clientId)
    {
        GameObject newHero = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        var newHeroNetwork = newHero.GetComponent<NetworkObject>();
        newHeroNetwork.Spawn();
        newHeroNetwork.GetComponent<NetworkObject>().ChangeOwnership(clientId);
        ulong newHeroId = newHeroNetwork.NetworkObjectId;
        SpawnClientRpc(newHeroId);
    }
    
    [ClientRpc]
    private void SpawnClientRpc(ulong objectId)
    {
        if (!IsOwner)
            return;
        NetworkObject newHero = NetworkManager.Singleton.SpawnManager.SpawnedObjects[objectId];
        var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        playerObject.GetComponent<PlayerController>().AddHero(newHero.gameObject.GetComponent<HeroController>());
    }
}
