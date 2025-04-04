using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class S_Spawner : NetworkBehaviour
{
    //-- This script is used to spawn units in the game --//
    //-- It is attached to the spawner object in the scene --//
    //-- It uses a list of units to spawn and a spawn rate --//
    //-- It also has a reference to the unit parent object --//
    //-- The script is used to spawn units at a specific position --//

    [Header("Spawner Settings")]
    [SerializeField, Range(0.1f, 10f)] private float spawnRate = 2f;
    [Space(10)]
    [Header("Self Units")]
    [SerializeField] private List<GameObject> currentUnits = new List<GameObject>();
    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform spawnerTransform;
    [SerializeField] private GameObject unitGameObject;
    [SerializeField] private GameObject unitParent;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return; // -- Only Owner can spawn units --//

        //-- Get Unit Parent in the map hierarchy --//
        unitParent = GameObject.FindGameObjectsWithTag("UnitParent")[0];

        if (unitParent != null)
            InvokeRepeating("Spawn", 2, spawnRate); // -- Start Spawning and spawning every spawn Rate --//
    }

    private void Spawn()
    {
        //-- Spawn a unit and add it to the list of the player active unit --//
        GameObject unit = Instantiate(unitGameObject, spawnerTransform.position, Quaternion.identity);
        unit.transform.parent = unitParent.transform;
        AddUnit(unit);
    }

    public void AddUnit(GameObject unit)
    {
        //-- Add a unit to the list of the player active unit --//
        currentUnits.Add(unit);
    }

    
    public void RemoveUnit(GameObject unit) //-- Call it when Unit is dead --//
    {
        //-- Remove a unit from the list of the player active unit --//
        currentUnits.Remove(unit);
    }
}
