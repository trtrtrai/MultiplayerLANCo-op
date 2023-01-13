using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkListener : MonoBehaviour
{
    public bool StartMyServer(bool isHost)
    {
        var success = false;
        if (isHost)
        {
            success = NetworkManager.Singleton.StartHost();
        }
        else
        {
            success = NetworkManager.Singleton.StartServer();
        }

        if (success)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        }

        return success;
    }

    public bool StartMyClient()
    {
        var success = NetworkManager.Singleton.StartClient();
        if (success)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        }
        return success;
    }

    private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        // Both client and server receive these notifications
        switch (sceneEvent.SceneEventType)
        {
            // Handle server to client Load Notifications
            case SceneEventType.Load:
                {
                    // This event provides you with the associated AsyncOperation
                    // AsyncOperation.progress can be used to determine scene loading progression
                    var asyncOperation = sceneEvent.AsyncOperation;
                    // Since the server "initiates" the event we can simply just check if we are the server here
                    if (NetworkManager.Singleton.IsServer)
                    {
                        Debug.Log("Load Server");
                        if (NetworkManager.Singleton.IsHost)
                        {
                            Debug.Log("Host in server");
                        }
                    }
                    else
                    {
                        Debug.Log("Load Client");
                    }
                    break;
                }
            // Handle server to client unload notifications
            case SceneEventType.Unload:
                {
                    // You can use the same pattern above under SceneEventType.Load here
                    break;
                }
            // Handle client to server LoadComplete notifications
            case SceneEventType.LoadComplete:
                {
                    // This will let you know when a load is completed
                    // Server Side: receives this notification for both itself and all clients
                    if (NetworkManager.Singleton.IsServer)
                    {
                        GameController.Instance.SpawnPlayer(sceneEvent.ClientId);

                        if (sceneEvent.ClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            // Handle server side LoadComplete related tasks here
                            Debug.Log("Server load " + sceneEvent.ClientId + " completed");

                            if (NetworkManager.Singleton.IsHost)
                            {
                                Debug.Log("Host in server load completed");
                            }
                        }
                        else
                        {
                            // Handle client LoadComplete **server-side** notifications here
                            Debug.Log("Server side client load " + sceneEvent.ClientId + " completed");
                        }
                    }
                    else // Clients generate this notification locally
                    {
                        // Handle client side LoadComplete related tasks here
                        Debug.Log("Client load " + sceneEvent.ClientId + " completed");
                    }

                    // So you can use sceneEvent.ClientId to also track when clients are finished loading a scene
                    break;
                }
            // Handle Client to Server Unload Complete Notification(s)
            case SceneEventType.UnloadComplete:
                {
                    // This will let you know when an unload is completed
                    // You can follow the same pattern above as SceneEventType.LoadComplete here

                    // Server Side: receives this notification for both itself and all clients
                    // Client Side: receives this notification for itself

                    // So you can use sceneEvent.ClientId to also track when clients are finished unloading a scene
                    break;
                }
            // Handle Server to Client Load Complete (all clients finished loading notification)
            case SceneEventType.LoadEventCompleted:
                {
                    // This will let you know when all clients have finished loading a scene
                    // Received on both server and clients
                    foreach (var clientId in sceneEvent.ClientsThatCompleted)
                    {
                        // Example of parsing through the clients that completed list
                        if (NetworkManager.Singleton.IsServer)
                        {
                            // Handle any server-side tasks here
                        }
                        else
                        {
                            // Handle any client-side tasks here
                        }
                    }
                    break;
                }
            // Handle Server to Client unload Complete (all clients finished unloading notification)
            case SceneEventType.UnloadEventCompleted:
                {
                    // This will let you know when all clients have finished unloading a scene
                    // Received on both server and clients
                    foreach (var clientId in sceneEvent.ClientsThatCompleted)
                    {
                        // Example of parsing through the clients that completed list
                        if (NetworkManager.Singleton.IsServer)
                        {
                            // Handle any server-side tasks here
                        }
                        else
                        {
                            // Handle any client-side tasks here
                        }
                    }
                    break;
                }
        }
    }
}
