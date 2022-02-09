using System;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _distanceToAttack = 1.5f;
    [SerializeField] private TowerSpawner _towerSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private float _notionProbability = 0.85f;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;

    private Creature _currentCreature;
    private Vector3 _currentTarget;

    public event Action<Creature> ReachedTarget;
    public event Action FollowingTarget;
    private void Start()
    {
        _currentCreature = _towerSpawner.GetClosest(transform.position);
    }

    private void Update()
    {
        if ((_enemy.GetHealth() / _enemy.StartHealthValue) <= _notionProbability || !_player)
        {
            SetCreature(_player);
        }
        else
        {
            SetCreature(_towerSpawner.GetClosest(transform.position));
        }
        MoveToTarget(_currentTarget);
        RotateToTarget(_currentTarget);
    }

    private void MoveToTarget(Vector3 targetPoint)
    {
        float deltaSpeed = Time.deltaTime * _movementSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint);
        if (distanceToTarget >= _distanceToAttack && !_animator.GetBool("IsDead"))
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint, deltaSpeed);
            FollowingTarget?.Invoke();
        }
        else
        {
            ReachedTarget?.Invoke(_currentCreature);
        }
    }

    private void RotateToTarget(Vector3 targetPoint)
    {
        Vector3 directionToTarget = targetPoint - transform.position;
        directionToTarget.y = 0f; 
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        float deltaSpeed = Time.deltaTime * _rotationSpeed;
        transform.rotation = Quaternion.Lerp(transform.localRotation, targetRotation, deltaSpeed);
    }

    public void SetCreature(Creature currenCteature)
    {
        _currentCreature = currenCteature;
        if(currenCteature is Tower)
        {
            _currentTarget = _towerSpawner.GetClosest(transform.position).EnemyAimingPoint.position;
        }
        else
        {
            _movementSpeed *= 0.5f;
            _currentTarget = currenCteature.transform.position;
        }
    }
}
