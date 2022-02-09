using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _getHitPeriodInSeconds = 1.5f;

    private float _elapsedTime;
    private int _attackHash;
    private int _deadHash;
    private int _walkHash;
    private int _hitHash;

    private void Awake()
    {
        _attackHash = Animator.StringToHash("IsAttacking");
        _deadHash = Animator.StringToHash("IsDead");
        _walkHash = Animator.StringToHash("IsWalking");
        _hitHash = Animator.StringToHash("Hit");
    }

    private void OnEnable()
    {
        _mover.ReachedTarget += OnSetAttackingState;
        _mover.FollowingTarget += OnSetWalkingState;
        _enemy.TookDamage += OsSetTakeDamageState;
        _enemy.Died += OnSetDiedState;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        _mover.ReachedTarget -= OnSetAttackingState;
        _mover.FollowingTarget -= OnSetWalkingState;
        _enemy.TookDamage -= OsSetTakeDamageState;
        _enemy.Died -= OnSetDiedState;
    }

    private void OnSetAttackingState(Creature creactureToAttack)
    {
        _animator.SetBool(_attackHash, !_animator.GetBool(_deadHash));
        _animator.SetBool(_walkHash, false);
    }

    private void OnSetWalkingState()
    {
        _animator.SetBool(_attackHash, false);
        _animator.SetBool(_walkHash, !_animator.GetBool(_deadHash));
    }

    private void OnSetDiedState()
    {
        _animator.SetBool(_walkHash, false);
        _animator.SetBool(_attackHash, false);
        _animator.SetBool(_deadHash, true);
    }

    private void OsSetTakeDamageState()
    {
        if (_elapsedTime >= _getHitPeriodInSeconds)
        {
            _animator.SetTrigger(_hitHash);
            _elapsedTime = 0f;
        }
    }
}
