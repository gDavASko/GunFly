using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private string _id = "level";

    private Transform _playerSpawn = null;
    private List<KeyValuePair<string, Vector3>> _enemiesSpawns = new List<KeyValuePair<string, Vector3>>();

    public string Id => _id;
    public Transform Transform => transform;
    public Vector3 PlayerSpawnPoint => _playerSpawn.position;
    public List<KeyValuePair<string, Vector3>> EnemySpawnPoints => _enemiesSpawns;

    private void Awake()
    {
        UnitMarker[] markers = transform.GetComponentsInChildren<UnitMarker>(false);
        foreach (var marker in markers)
        {
            if (marker.Id != "Player")
            {
                _enemiesSpawns.Add(new KeyValuePair<string, Vector3>(marker.Id, marker.transform.position));
            }
            else
            {
                _playerSpawn = marker.transform;
            }
        }
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
    }
}