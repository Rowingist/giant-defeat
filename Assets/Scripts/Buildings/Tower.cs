using System.Collections.Generic;
using UnityEngine;

public class Tower : Creature
{
    [SerializeField] private List<Brick> _bricks;
    [SerializeField] private Transform _enemyAimingPoint;
    [SerializeField] private BrickBurst[] _brickBurst;

    private float _elapsedTime;
    private float _timeRangetToGetDamage = 3f;

    public Transform EnemyAimingPoint => _enemyAimingPoint;
    private void Awake()
    {
        Health = new Health(_bricks.Count);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public override void TakeDamage(int damage)
    {
        if (_elapsedTime > _timeRangetToGetDamage)
        {
            _elapsedTime = 0f;
            Health.TakeDamage(damage);
            SubdivideBrick(damage);
        }
    }

    private void SubdivideBrick(int amount)
    {
        Brick damagedBrick = _bricks[_bricks.Count - amount];
        _brickBurst[_bricks.Count - amount].StartEffect();
        Destroy(damagedBrick.gameObject);
        _bricks.Remove(damagedBrick);

        if (_bricks.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
