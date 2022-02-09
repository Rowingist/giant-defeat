using UnityEngine;

public class DamageMaker : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private int _damagePower = 1;
    [SerializeField] private Animator _animator;

    private Creature _damagingTarget;

    private void OnEnable()
    {
        _mover.ReachedTarget += OnSetTarget;
    }

    private void OnDisable()
    {
        _mover.ReachedTarget -= OnSetTarget;
    }

    private void OnSetTarget(Creature creactureToAttack)
    {
        _damagingTarget = creactureToAttack;
        _mover.enabled = false;
    }

    public void MakeDamage()
    {
        _damagingTarget.TakeDamage(_damagePower);
        if (_damagingTarget)
        {
            Invoke(nameof(EnableMover), 2f);
        }
    }

    private void EnableMover()
    {
        _mover.enabled = true;
    }
}