using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, ITarget
{
    public event Action<int> OnEnemyDeath;
    
    [SerializeField] 
    private BulletController bulletPrefab;

    private List<BulletController> _bulletsPool;
    private int _damage;
    private float _bulletSpeed;
    private int _healthMax;
    private int _healthCurrent;
    private int _droppedCoins;
    private const string TargetTag = "Player";
    private Transform _transform;
    private GameObject _gameObject;

    public void SetUp(EnemySettingsSO settings)
    {
        _damage = settings.Damage;
        _bulletSpeed = settings.BulletSpeed;
        _healthMax = settings.Health;
        _healthCurrent = settings.Health;
        _droppedCoins = settings.CoinsDroped;
    }

    public void ReceiveDamage(int damage)
    {
        _healthCurrent -= damage;

        if (_healthCurrent <= 0)
        {
            StopCoroutine(WaitToShootBullet());
            _gameObject.SetActive(false);
            OnEnemyDeath?.Invoke(_droppedCoins);
        }
    }

    public void ActivateEnemy(Vector3 location)
    {
        _transform.position = location;
        _healthCurrent = _healthMax;
        _gameObject.SetActive(true);
        StartCoroutine(WaitToShootBullet());
    }
    
    private void Awake()
    {
        _gameObject = gameObject;
        _transform = transform;
        _bulletsPool = new List<BulletController>();
        StartCoroutine(WaitToShootBullet());
    }

    private IEnumerator WaitToShootBullet()
    {
        yield return new WaitForSeconds(1);
        ShootBullet();
        StartCoroutine(WaitToShootBullet());
    }

    private void ShootBullet()
    {
        BulletController bullet = GetAvailableBullet();
        bullet.Move(GetBulletTargetRotation());
    }

    private BulletController GetAvailableBullet()
    {
        BulletController bullet = _bulletsPool.Find(x => !x.gameObject.activeInHierarchy);

        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, _transform.position, Quaternion.identity, _transform);
            bullet.Init(_damage, TargetTag, _bulletSpeed);
            _bulletsPool.Add(bullet);
        }
        else
        {
            bullet.ResetPosition(_transform.position);
        }

        return bullet;
    }

    private Vector3 GetBulletTargetRotation()
    {
        float y = Random.Range(0, 360);
        Vector3 rotation = new Vector3(0, y, 90);
        return rotation;
    }
}
