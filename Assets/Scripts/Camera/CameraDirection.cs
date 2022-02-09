using Cinemachine;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    [SerializeField] private PlayerMovement _target;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _standart;
    [SerializeField] private Transform _cameraCenter;

    private void OnEnable()
    {
        _target.Searching.AddListener(OnUnBindRotation);
        _target.Attacking.AddListener(OnBindRotation);
    }

    private void OnUnBindRotation()
    {
        _cameraCenter.transform.parent = null;
        _cameraCenter.SetPositionAndRotation(_target.transform.position, _target.transform.rotation);
        _standart.SetActive(true);
        _aim.SetActive(false);
    }

    private void OnBindRotation()
    {
        _standart.SetActive(false);
        _aim.SetActive(true);
        _cameraCenter.transform.parent = _target.transform;
    }

    private void OnDisable()
    {
        _target.Searching.RemoveListener(OnUnBindRotation);
        _target.Attacking.RemoveListener(OnBindRotation);
    }
}
