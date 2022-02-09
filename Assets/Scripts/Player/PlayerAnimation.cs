using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private Transform _playerBody;

    private void OnEnable()
    {
        _player.Searching.AddListener(OnSearchingState);
        _player.Attacking.AddListener(OnAttackingState);
    }

    private void OnSearchingState()
    {
        _animator.SetBool("IsAttacking", false);
        _playerBody.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    private void OnAttackingState()
    {
        _animator.SetBool("IsAttacking", true);
        _playerBody.localRotation = Quaternion.Euler(new Vector3(0f, 50f, 0f));
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical") * _changeSpeed;
        float horizontalInput = Input.GetAxis("Horizontal") * _changeSpeed;
        _animator.SetFloat("Front", verticalInput);
        _animator.SetFloat("Right", horizontalInput);
    }

    private void OnDisable()
    {
        _player.Searching.RemoveListener(OnSearchingState);
        _player.Attacking.RemoveListener(OnAttackingState);
    }
}
