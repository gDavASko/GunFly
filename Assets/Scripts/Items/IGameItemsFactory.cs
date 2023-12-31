﻿using System.Threading.Tasks;
using UnityEngine;

public interface IGameItemsFactory
{
    public Task<T> CreateGameItem<T>(string id, Vector3 position, bool cacheIt)
        where T : class, IGameItem;

    public void ClearAll();
}