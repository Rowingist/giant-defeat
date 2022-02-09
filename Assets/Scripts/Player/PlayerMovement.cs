using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _charachterController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Enemy _closestGiant;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private Transform _gunAttatchPoint;
    
    private float _gunAttackingDuration;
    private Transform _transform;
    private Vector3 _offset;
    private float _deltaSpeed;
    private bool _isSearching;

    public UnityEvent Attacking;
    public UnityEvent Searching;

    public Transform GunAttachPoint => _gunAttatchPoint;
    public Enemy ClosestEnemy => _closestGiant;
    public bool IsSearching => _isSearching;

    private void Awake()
    {
        _transform = transform;
    }
    private void Start()
    {
        SetSearchingState();
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        _deltaSpeed = _speed * Time.deltaTime;
        if (_isSearching)
        {
            SetDirectionsForSearch(horizontalInput, verticalInput);
        }
        else
        {
            SetDirectionsForAttack(horizontalInput, verticalInput);
        }
        _charachterController.Move(_offset);
    }

    private void SetDirectionsForSearch(float horizontal, float vertical)
    {
        Vector3 newRotation = new Vector3(0f, horizontal, 0f);
        _transform.Rotate(newRotation * _rotationSpeed * Time.deltaTime);
        _offset = _transform.forward * vertical * _deltaSpeed;
    }

    private void SetDirectionsForAttack(float horizontal, float vertical)
    {
        Vector3 targetDirection = _closestGiant.transform.position - _transform.position;
        Vector3 targetDirectionXZ = new Vector3(targetDirection.x, 0f, targetDirection.z);
        float deltaRotationSpeed = Time.deltaTime * 5f;
        _transform.localRotation = Quaternion.Lerp(_transform.localRotation, Quaternion.LookRotation(targetDirectionXZ), deltaRotationSpeed);
        _offset = ((_transform.right * horizontal) + (_transform.forward * vertical)) * _deltaSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WeaponZone weaponZone))
        {
            _gunAttackingDuration = weaponZone.Weapon.LifeTime;
            SetAttackinState();
        }
    }

    private void SetAttackinState()
    {
        _isSearching = false;
        Attacking.Invoke();
        StartCoroutine(AttackTimer(_gunAttackingDuration));
    }

    private IEnumerator AttackTimer(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        SetSearchingState();
    }

    private void SetSearchingState()
    {
        _isSearching = true;
        Searching.Invoke();
    }
}