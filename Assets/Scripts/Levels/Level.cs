using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Events;
using JetBrains.Annotations;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private string _id = "level";

    private LevelFinishTrigger _finishTrigger = null;
    private Transform _playerSpawn = null;
    private List<KeyValuePair<string, Vector3>> _enemiesSpawns = new List<KeyValuePair<string, Vector3>>();

    private GameEvents _gameEvents = null;

    public string Id => _id;
    public Transform Transform => transform;
    public Vector3 PlayerSpawnPoint => _playerSpawn.position;
    public List<KeyValuePair<string, Vector3>> EnemySpawnPoints => _enemiesSpawns;

    public void Init(GameEvents gameEvents)
    {
        _gameEvents = gameEvents;
    }

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

        _finishTrigger = GetComponentInChildren<LevelFinishTrigger>();
        _finishTrigger.OnLevelFinish += FinishLevel;
    }

    private void FinishLevel()
    {
        _gameEvents.OnGameFinish?.Invoke();
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
    }
}