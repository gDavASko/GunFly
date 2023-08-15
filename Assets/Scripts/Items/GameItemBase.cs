using System;
using Events;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class GameItemBase : MonoBehaviour, IGameItem
{
    protected ItemEvents _itemEvents = null;
    protected IAudioPlayer _audioPlayer = null;

    public abstract bool IsWeapon { get; }
    public abstract float Count { get; }
    public abstract string id { get; }

    protected void Awake()
    {
        _audioPlayer = GetComponent<IAudioPlayer>();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Init(Vector3 initPosition, ItemEvents itemEvents)
    {
        _itemEvents = itemEvents;
        transform.position = initPosition;
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameController.PLAYER_ID))
        {
            _audioPlayer?.PlaySound();

            if(_itemEvents.OnItemPlayerCollision != null)
                _itemEvents.OnItemPlayerCollision(this);

            Hide();
        }
    }

    public void Release()
    {
        //ToDo: remake to pool object release
        Destroy(this.gameObject);
    }
}