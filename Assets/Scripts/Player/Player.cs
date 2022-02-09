using UnityEngine;

public class Player : Creature
{
    [SerializeField] private float _health;
    [SerializeField] private Healthbar _healthbar;

    private float _elapsedTime;
    private float _timeRangetToGetDamage = 2f;

    private void Awake()
    {
        Health = new Health(_health);
        _healthbar.Construct(Health);
    }

    public override void TakeDamage(int damage)
    {
        if (_elapsedTime > _timeRangetToGetDamage)
        {
            _elapsedTime = 0f;
            Health.TakeDamage(damage);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }
}