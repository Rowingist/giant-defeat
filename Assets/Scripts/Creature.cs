using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected IHealth Health;
    public abstract void TakeDamage(int damage);
}