using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IController))]
public class EnemyBehaviourBase : MonoBehaviour, IInput
{
    [SerializeField] private float _actinoCycleSeconds = 5f;
    [SerializeField] private float _jumpDelaySeconds = 3f;

    private IController _controller = null;
    protected float _timeCounter = 0;
    private bool _jumped = false;

    public Action<ActionType> OnAction { get; set; }

    protected virtual void Awake()
    {
        _controller = GetComponent<IController>();
        _controller.Init(this);
        ResetCycle();
    }

    public virtual void Update()
    {
        if (!gameObject.activeInHierarchy || !gameObject.activeSelf)
            return;

        if (!_jumped && _timeCounter >= _jumpDelaySeconds)
        {
            OnAction?.Invoke(ActionType.Jump);
            _jumped = true;
        }

        _timeCounter += Time.deltaTime;
        if (_timeCounter >= _actinoCycleSeconds)
            ResetCycle();
    }

    protected virtual void ResetCycle()
    {
        _timeCounter = 0;
        _jumped = false;
    }
}