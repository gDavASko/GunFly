
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel : IDisposable
{
   string Id { get; }

   Transform Transform { get; }
   Vector3 PlayerSpawnPoint { get; }
   List<KeyValuePair<string, Vector3>> EnemySpawnPoints { get; }
}