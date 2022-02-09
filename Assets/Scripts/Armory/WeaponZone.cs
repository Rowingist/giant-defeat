using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponZone : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Rotation _rotator;
    [SerializeField] private float _spawnedWeaponSize = 5f;
    [SerializeField] private Image _weaponImage;

    public Weapon Weapon { get; private set; }

    private void Awake()
    {
        int random = Random.Range(0, _weapons.Count);
        for (int i = 0; i < _weapons.Count; i++)
        {
            if(i == random)
            {
                Weapon = Instantiate(_weapons[i], _rotator.transform);
                Weapon.transform.localScale = new Vector3(_spawnedWeaponSize, _spawnedWeaponSize, _spawnedWeaponSize);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            if (player.IsSearching)
            {
                Destroy(gameObject);
                Weapon.SetNewParent(player.GunAttachPoint, player.ClosestEnemy.PointToAim);
                Weapon.transform.localScale = Vector3.one;
                Weapon.Invoke(nameof(Weapon.StartShoot), 0.3f);
                _weaponImage.sprite = Weapon.Icon;
            }
        }
    }
}