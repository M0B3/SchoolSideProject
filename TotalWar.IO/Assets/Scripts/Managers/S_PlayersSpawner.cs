using System.Collections.Generic;
using UnityEngine;

public class S_PlayersSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private List<GameObject> playerToSpawn = new List<GameObject>();
    [SerializeField] private SpriteRenderer boundLimit;

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < playerToSpawn.Count; i++)
        {
            GameObject player = Instantiate(playerToSpawn[i], new Vector3(Random.Range(-boundLimit.bounds.size.x / 2, boundLimit.bounds.size.x / 2), Random.Range(-boundLimit.bounds.size.y / 2, boundLimit.bounds.size.y / 2), 0), Quaternion.identity);
        }
    }
}

