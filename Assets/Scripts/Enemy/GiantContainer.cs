using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantContainer : MonoBehaviour
{
    [SerializeField] private List<Enemy> _giantsList = new List<Enemy>();

    public Enemy GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Enemy closestGiant = null;
        for (int i = 0; i < _giantsList.Count; i++)
        {
            float distance = Vector3.Distance(point, _giantsList[i].transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                closestGiant = _giantsList[i];
            }
        }

        return closestGiant;
    }
}
