using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerNetworkController : NetworkBehaviour
{
    private Vector2 direction;
    void Update()
     {
         if (IsOwner)
         {
             if (Input.GetKeyDown(KeyCode.W))
             {
                 direction = Vector2.up;
             }
             else if (Input.GetKeyDown(KeyCode.S))
             {
                 direction = Vector2.down;
             }
             else if (Input.GetKeyDown(KeyCode.D))
             {
                 direction = Vector2.right;
             }
             else if (Input.GetKeyDown(KeyCode.A))
             {
                 direction = Vector2.left;
             }
             if (direction != Vector2.zero)
             {
                 ulong id = GetComponent<PlayerController>().GetSelectedHero().GetComponent<NetworkObject>().NetworkObjectId;
                 MoveServerRpc(direction.x, direction.y, id);
                 direction = Vector2.zero;
             }
         }
     }

     [ServerRpc]
     void MoveServerRpc(float x, float y, ulong objectId)
     {
         NetworkObject obj = NetworkManager.Singleton.SpawnManager.SpawnedObjects[objectId];
         Vector2 direction = new Vector2(x, y);
         obj.gameObject.GetComponent<HeroController>().Move(direction);
     }
}