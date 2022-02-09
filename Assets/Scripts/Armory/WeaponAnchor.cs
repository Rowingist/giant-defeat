using UnityEngine;

public class WeaponAnchor : MonoBehaviour
{
    [SerializeField] private Transform _anchor;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = _anchor.position;
    }
}
