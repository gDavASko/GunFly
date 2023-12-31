using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected string _damageId = "damage";
    [SerializeField] protected string _attackSoundName = "Attack";

    protected IDamageConfig _damageConfig = null;
    protected GameObject _owner = null;
    protected string _targetTag = string.Empty;


    //ToDo: move audio to audio event system
    protected IAudioPlayer _audioPlayer = null;

    public string DamageId => _damageId;

    private void Start()
    {
        _audioPlayer = GetComponent<IAudioPlayer>();
    }

    public virtual void Attack()
    {
        if(!gameObject.activeSelf || !gameObject.activeInHierarchy)
            return;

        //ToDo: Remake hardcode to switch variant
        _audioPlayer?.PlaySound(_attackSoundName);
    }

    public virtual void Init(GameObject owner, IDamageConfig damageConfig, string targetTag)
    {
        _owner = owner;
        _damageConfig = damageConfig;
        _targetTag = targetTag;
    }

    private void OnAttack(ActionType action)
    {
        if (action == ActionType.Attack)
        {
            Attack();
        }
    }

    public abstract void Dispose();
}