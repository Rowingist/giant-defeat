using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 cameraPoint = _mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(cameraPoint);
    }
}
