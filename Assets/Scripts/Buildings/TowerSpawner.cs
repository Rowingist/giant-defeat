using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private List<Tower> _towersList = new List<Tower>();

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Tower newTown = Instantiate(_towerPrefab, _spawnPoints[i].position, Quaternion.identity, transform);
            _towersList.Add(newTown);
        }
    }


    public Tower GetClosest(Vector3 point)
    {
        UpdateTowers();

        float minDistance = Mathf.Infinity;
        Tower closestCoin = null;
        for (int i = 0; i < _towersList.Count; i++)
        {
            float distance = Vector3.Distance(point, _towersList[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = _towersList[i];
            }
        }

        return closestCoin;
    }

    private void UpdateTowers()
    {
        for (int i = 0; i < _towersList.Count; i++)
        {
            if (_towersList[i] == null)
            {
                _towersList.RemoveAt(i);
            }
        }
    }
}
