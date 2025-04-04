using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class S_ConnectUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;


    void Start()
    {
        hostButton.onClick.AddListener(HostButtonOnClick);
        clientButton.onClick.AddListener(ClientButtonOnClick);
    }

    private void HostButtonOnClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void ClientButtonOnClick()
    {
        NetworkManager.Singleton.StartClient();
    }

}
