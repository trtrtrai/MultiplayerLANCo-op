using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button btnServer;
    [SerializeField] private Button btnHost;
    [SerializeField] private Button btnClient;
    [SerializeField] private NetworkObject player;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("DDOL") is null)
        {
            Instantiate(Resources.Load<GameObject>("Manager/NetworkManager"));
        }

        btnServer.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<NetworkListener>().StartMyServer(false);
            NetworkManager.Singleton.SceneManager.LoadScene("PlayGame", LoadSceneMode.Single);
        });

        btnHost.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<NetworkListener>().StartMyServer(true);
            NetworkManager.Singleton.SceneManager.LoadScene("PlayGame", LoadSceneMode.Single);
        });

        btnClient.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<NetworkListener>().StartMyClient();
        });
    }
}
