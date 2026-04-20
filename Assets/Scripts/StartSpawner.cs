using UnityEngine;
using Unity.Netcode;

public class StartSpawner : NetworkBehaviour
{
    public GameObject startText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 0){
            NetworkManager.Singleton.StartHost();
            startText.SetActive(true);
        }
    }
}
