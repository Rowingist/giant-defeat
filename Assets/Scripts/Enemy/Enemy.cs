using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Creature
{
    [SerializeField, Range(1f, 100f)] private float _healthValue;
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private Transform _pointToAim;
    [SerializeField] private Animator _animator;

    public float StartHealthValue => _healthValue;
    public Transform PointToAim => _pointToAim;

    public UnityEvent TookDamaBeforeAgro;
    public event Action TookDamage;
    public event Action Died;

    private void Awake()
    {
        Health = new Health(_healthValue);
        _healthbar.Construct(Health);
    }

    private void OnEnable()
    {
        Health.Died += OnDie;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            if (Health.GetHealth() / StartHealthValue > 0.85f)
            {
                TookDamaBeforeAgro.Invoke();
            }
            TakeDamage(bullet.Damage);
        }
    }

    private void OnDie()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            for (int j = 0; j < _meshRenderers[i].materials.Length; j++)
            {
                _meshRenderers[i].materials[j].color = Color.white;
            }

        }
        Died?.Invoke();
        GetComponent<Collider>().isTrigger = true;
        Destroy(gameObject, 6.5f);
    }

    private void OnDisable()
    {
        Health.Died -= OnDie;
    }

    public override void TakeDamage(int damage)
    {
        TookDamage?.Invoke();
        Health.TakeDamage(damage);
    }

    public float GetHealth()
    {
        return Health.GetHealth();
    }
}