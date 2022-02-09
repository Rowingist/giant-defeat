using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _shotPeriod = 0.2f;
    [SerializeField] private bool _isGrabbed = false;
    [SerializeField] private ParticleSystem _splashEffect;

    public float LifeTime => _lifeTime;
    public Sprite Icon => _icon;

    private float _timer;
    private Transform _transform;
    private Transform _enemyAimingPoint;

    public void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _shotPeriod && _isGrabbed)
        {
            Vector3 poitingDirection = _enemyAimingPoint.position - _transform.position;
            _bulletSpawnPoint.rotation = Quaternion.Lerp(_bulletSpawnPoint.rotation, Quaternion.LookRotation(poitingDirection), Time.deltaTime * 5f);
            Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            _timer = 0;
        }
    }

    public void StartShoot()
    {
        _isGrabbed = true;
        _splashEffect.Play();
        Destroy(gameObject, _lifeTime);
    }

    public void SetNewParent(Transform newParent, Transform aim)
    {
        _transform.parent = newParent;
        _transform.SetPositionAndRotation(newParent.position, newParent.rotation);
        _enemyAimingPoint = aim;
    }
}
