using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class S_Spawner : NetworkBehaviour
{
    //-- This script is used to spawn units in the game --//
    //-- It is attached to the spawner object in the scene --//
    //-- It uses a list of units to spawn and a spawn rate --//
    //-- The script is used to spawn units at a specific position --//

    [Header("Spawner Settings")]
    [Range(0.1f, 10f)] public float spawnRate { get; private set; } = 4f;
    [Space(10)]
    [Header("Self Units")]
    [SerializeField] private List<NetworkObject> currentUnits = new List<NetworkObject>();
    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform spawnerTransform;
    [SerializeField] private GameObject unitGameObject;

    private S_ButtonPlayerUI playerUI;

    public override void OnNetworkSpawn()
    {
        playerUI = GetComponent<S_ButtonPlayerUI>();

        if (!IsOwner) return; // -- Only Owner can spawn units --//

        //InvokeRepeating(nameof(AskToSpawnUnit), 2, spawnRate); // -- Start Spawning and spawning every spawn Rate --//
    }

    private void Update()
    {
        if (!IsOwner) return; // -- Only Owner can spawn units --//

        if (playerUI.AsSpawned)
        {
            InvokeRepeating(nameof(AskToSpawnUnit), 2, spawnRate);// -- Start Spawning and spawning every spawn Rate --//
            playerUI.AsSpawned = false; // -- Reset the button --//
        }
    }

    public void AskToSpawnUnit() //-- Spawn Unit --//
    {
        SpawnServerRpc();
    }

    [ServerRpc]
    private void SpawnServerRpc(ServerRpcParams rpcParams = default) //-- ask spawn unit to the server --//
    {
        NetworkObject unit = Instantiate(unitGameObject, spawnerTransform.position, Quaternion.identity).GetComponent<NetworkObject>();
        unit.SpawnWithOwnership(OwnerClientId);
        AddUnit(unit);
    }

    public void AddUnit(NetworkObject unit)
    {
        //-- Add a unit to the list of the player active unit --//
        currentUnits.Add(unit);
    }

    
    public void RemoveUnit(NetworkObject unit) //-- Call it when Unit is dead --//
    {
        //-- Remove a unit from the list of the player active unit --//
        currentUnits.Remove(unit);
    }
}
