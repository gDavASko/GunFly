using UnityEngine;

public class UnitController : MonoBehaviour, IController
{
    [SerializeField] private Rigidbody2D _rigidBody = default;
    [SerializeField] private Vector3 _jumpForce = default;

    public void Init(IInput input)
    {
        input.OnAction += OnJump;
    }

    private void OnJump(ActionType action)
    {
        if (_rigidBody != null && action == ActionType.Jump)
            Jump();
    }

    public void Jump()
    {
        _rigidBody.AddForce(_jumpForce, ForceMode2D.Impulse);
    }
}