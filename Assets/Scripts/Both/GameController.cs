using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Server owner. Communication between client and server.
/// </summary>
public class GameController : NetworkBehaviour
{
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default)
    {
        SpawnPlayer(serverRpcParams.Receive.SenderClientId);
    }

    public void SpawnPlayer(ulong clientId)
    {
        var player = Instantiate(Resources.Load<GameObject>("Player/PlayerPrefab"));
        player.GetComponent<PlayerController>().Owner = clientId;
        Instantiate(Resources.Load<GameObject>("Player/Sword"), player.transform);
        Instantiate(Resources.Load<GameObject>("Player/SpecialShield"), player.transform);

        var control = Instantiate(Resources.Load<GameObject>("Player/PlayerControl"));
        player.GetComponent<PlayerController>().AddControl(control.GetComponent<PlayerControl>());
        control.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        player.GetComponent<NetworkObject>().Spawn(true);

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };

        SpawnCameraClientRpc(clientRpcParams);
    }

    /// <summary>
    /// ClientRpc params only send to client ID targeted
    /// </summary>
    /// <param name="clientRpcParams"></param>
    [ClientRpc]
    private void SpawnCameraClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if (IsClient)
        {
            var cmr = GameObject.FindGameObjectWithTag("MainCamera");

            if (cmr is null)
            {
                cmr = Instantiate(Resources.Load<GameObject>("CameraFollow"));
                cmr.GetComponent<NetworkObject>().Spawn();
            }

            var player = GameObject.FindGameObjectsWithTag("Player").ToList().First((p) => p.GetComponent<PlayerController>().Owner == NetworkManager.Singleton.LocalClientId).transform;
            var cmrFolow = cmr.GetComponent<CameraFollower>();
            cmrFolow.Target = player;
            cmrFolow.StartFocus();
        }
    }
}
