using UnityEngine;

public class BrickBurst : MonoBehaviour
{
    [SerializeField] private SubBrick _subBrickTemplate;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _force = 5f;
    [SerializeField] private ParticleSystem _smokeEffect;

    public void StartEffect()
    {
        transform.parent = null;
        for(int i = 0; i < _spawnPoints.Length; i++)
        {
            SubBrick subBrick = Instantiate(_subBrickTemplate, _spawnPoints[i].position, Quaternion.identity, _spawnPoints[i]);
            subBrick.GetComponent<Rigidbody>().velocity = Vector3.up * _force;
        }
        _smokeEffect.Play();
        Destroy(gameObject, 5f);
    }
}