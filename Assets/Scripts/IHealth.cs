using System;

public interface IHealth
{
    public event Action Died;
    public event Action Damaged;
    public float GetHealth();
    public void TakeDamage(float damage);
}
