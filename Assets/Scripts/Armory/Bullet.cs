using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private ParticleSystem _enemyHitEffect;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Rigidbody _rigidbody;

    private ParticleSystem _effect;
    private Transform _transform;
    public int Damage => _damage;

    private void Awake()
    {
        _transform = transform;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
           _effect = Instantiate(_enemyHitEffect);
        }
        else
        {
            _effect = Instantiate(_hitEffect);
        }
        _effect.transform.position = _transform.position;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _transform.forward * _speed;
    }
}
