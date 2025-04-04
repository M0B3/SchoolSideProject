using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class S_ResetPosButon : NetworkBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform selfTranform;
    [SerializeField] private Canvas canvas;

    [SerializeField] private GameObject resetButtonPrefab;
    [SerializeField] private GameObject spawnButtonPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        // Create the button
        CreateButtonSpawner();
    }

    public void ResetCameraPosition()
    {
        //-- Camera go back to Spawn Position --//
        camTransform.position = new Vector3(selfTranform.position.x, selfTranform.position.y, -10);
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
    private void SpawnPlayers()
    {
        gameObject.transform.position = new Vector3(Random.Range(-12 / 2, 12 / 2), Random.Range(-12 / 2, 12 / 2), 0);
        CreateButton();

    }
}
