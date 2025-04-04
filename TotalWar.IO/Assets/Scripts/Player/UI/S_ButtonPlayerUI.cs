using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class S_ButtonPlayerUI : NetworkBehaviour
{
    public bool AsSpawned { get;  set; } = false;

    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform selfTranform;
    [SerializeField] private Canvas canvas;

    [SerializeField] private GameObject resetButtonPrefab;
    [SerializeField] private GameObject spawnButtonPrefab;

    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject playerNameInputField;

    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(
        "New Player", // -- Default Name --//
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        playerName.OnValueChanged += OnPlayerNameChanged;

        playerNameText.text = playerName.Value.ToString();

        if (!IsOwner) return;

        // Create the button
        CreateButtonSpawner();
        CreatePlayerNameButton();
    }

    private void CreateButton()
    {
        resetButtonPrefab = Instantiate(resetButtonPrefab, canvas.transform);
        resetButtonPrefab.GetComponent<Button>().onClick.AddListener(ResetCameraPosition);
    }

    private void CreateButtonSpawner()
    {
        spawnButtonPrefab = Instantiate(spawnButtonPrefab, canvas.transform);
        spawnButtonPrefab.GetComponent<Button>().onClick.AddListener(SpawnPlayers);
    }

    private void CreatePlayerNameButton()
    {
        playerNameInputField = Instantiate(playerNameInputField, canvas.transform);
        playerNameInputField.SetActive(true);
        var inputField = playerNameInputField.GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(delegate { OnNameEndEdit(inputField.text); });
    }

    private void ResetCameraPosition()
    {
        //-- Camera go back to Spawn Position --//
        camTransform.position = new Vector3(selfTranform.position.x, selfTranform.position.y, -10);
    }
    private void SpawnPlayers()
    {
        spawnButtonPrefab.SetActive(false);
        playerNameInputField.SetActive(false);

        gameObject.transform.position = new Vector3(Random.Range(-12 / 2, 12 / 2), Random.Range(-12 / 2, 12 / 2), 0);
        CreateButton();
        AsSpawned = true;
    }

    private void OnNameEndEdit(FixedString32Bytes newName)
    {
        SetNameServerServerRpc(newName);
    }

    [ServerRpc]
    private void SetNameServerServerRpc(FixedString32Bytes newName)
    {
        playerName.Value = newName;
    }

    private void OnPlayerNameChanged(FixedString32Bytes oldName, FixedString32Bytes newName)
    {
        playerNameText.text = newName.ToString();
    }

}
