using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IHitPointSystem))]
public class DeathZoneChecker : MonoBehaviour
{
    [SerializeField] private string[] _deathTags = new string[] { "DeathZone" };

    private IHitPointSystem _hpSystem = null;

    private void Awake()
    {
        _hpSystem = GetComponent<IHitPointSystem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckDeath(col.tag);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CheckDeath(col.collider.tag);
    }

    private void CheckDeath(string tag)
    {
        if (_deathTags.Contains(tag))
            _hpSystem.Kill();
    }
}