using System;

public class Health : IHealth
{
    private float _value;

    public event Action Damaged;
    public event Action Died;

    public Health(float value)
    {
        _value = value;
    }

    public float GetHealth()
    {
        return _value;
    }
    public void TakeDamage(float damage)
    {
        Damaged?.Invoke();
        if(damage >= _value)
        {
            _value = 0;
        }

        if (_value == 0)
        {
            Died?.Invoke();
        }
        else
        {
            _value -= damage;
        }
    }
}
