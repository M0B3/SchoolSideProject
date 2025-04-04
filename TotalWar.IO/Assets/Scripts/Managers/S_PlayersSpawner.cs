using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayersSpawner : NetworkBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject playerToSpawn;
    //[SerializeField] private SpriteRenderer boundLimit;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject SpawnerCanvas;

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private GameObject camera;

    private void Awake()
    {
        // Find the canvas in the scene
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        camera = Camera.main.gameObject;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        // Create the button
        CreateButtonSpawner();
    }

    private void CreateButtonSpawner()
    {
        buttonPrefab = Instantiate(buttonPrefab, canvas.transform);
        buttonPrefab.GetComponent<Button>().onClick.AddListener(SpawnPlayers);
        playerToSpawn = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    private void SpawnPlayers()
    {
        //camera.SetActive(false);
        //canvas.SetActive(false);
        playerToSpawn.transform.position = new Vector3(Random.Range(-12 / 2, 12 / 2), Random.Range(-12 / 2, 12 / 2), 0);
        
    }
}

