using Unity.Netcode;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
            SpawnHero();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
                        NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void SpawnHero()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Spawn Hero" : "Request Server Spawn Hero"))
        {
            ulong id = NetworkManager.Singleton.LocalClientId;
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            playerObject.GetComponent<Spawner>().SpawnHeroServerRpc(id);
        }
    }
}