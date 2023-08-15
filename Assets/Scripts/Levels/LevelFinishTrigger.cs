using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelFinishTrigger : MonoBehaviour
{
    //ToDo: Move sounds to sound event system
    private IAudioPlayer _soundFinish = null;

    private void Start()
    {
        _soundFinish = GetComponent<IAudioPlayer>();
    }

    public System.Action OnLevelFinish { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameController.PLAYER_ID))
        {
            _soundFinish?.PlaySound();
            OnLevelFinish?.Invoke();
        }
    }
}