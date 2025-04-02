using System.Collections.Generic;
using UnityEngine;

public class S_Spawner : MonoBehaviour
{
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

    private void OnEnable()
    {
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
