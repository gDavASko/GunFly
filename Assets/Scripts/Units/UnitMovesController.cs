using System;
using System.Linq;
using UnityEngine;

public class UnitMovesController : MonoBehaviour, IController
{
    [SerializeField] private Rigidbody2D _rigidBody = default;
    [SerializeField] private Vector3 _jumpForce = default;

    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private string[] _jumpEnableTags = new string[] { "Ground", "Enemy" };

    [SerializeField] private string _soundOnJump = "Jump";

    private int _jumpCounter = 0;
    private IAudioPlayer _audioPlayer = null;

    private void Awake()
    {
        _audioPlayer = GetComponent<IAudioPlayer>();
    }

    public void Init(IInput input)
    {
        input.OnAction += OnJump;
    }

    private void OnJump(ActionType action)
    {
        if (_rigidBody != null && action == ActionType.Jump && _jumpCounter < _maxJumpCount)
            Jump();
    }

    public void Jump()
    {
        _jumpCounter++;
        _rigidBody.AddForce(_jumpForce, ForceMode2D.Impulse);
        _audioPlayer?.PlaySound(_soundOnJump);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckGrounded(col.tag);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CheckGrounded(col.collider.tag);
    }

    private void CheckGrounded(string tag)
    {
        if (_jumpEnableTags.Contains(tag))
            _jumpCounter = 0;
    }
}