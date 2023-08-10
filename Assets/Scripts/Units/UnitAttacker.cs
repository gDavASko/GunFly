using UnityEngine;

public class UnitAttacker : MonoBehaviour, IAttackController
{
    public void Init(IInput input)
    {
        input.OnAction += OnAttack;
    }

    private void OnAttack(ActionType action)
    {
        if (action == ActionType.Attack)
            Attack();
    }

    public void Attack()
    {
        Debug.LogError($"{name} Attack!!!");
    }
}