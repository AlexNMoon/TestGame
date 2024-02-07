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
    [SerializeField] 
    private DroppableItemController healthItemPrefab;
    [SerializeField] 
    private BoosterItemController damageItemPrefab;
    [SerializeField] 
    private BoosterItemController speedItemPrefab;

    private List<BulletController> _bulletsPool;
    private List<DroppableItemController> _healthItemsPool;
    private List<BoosterItemController> _damageItemsPool;
    private List<BoosterItemController> _speedItemsPool;
    private int _damage;
    private float _bulletSpeed;
    private int _healthMax;
    private int _healthCurrent;
    private int _droppedCoins;
    private int _healthBonusPercent;
    private int _damageBoosterPercent;
    private int _damageBoosterTimer;
    private int _speedBoosterPercent;
    private int _speedBoosterTimer;
    private int _dropItemProbability;
    private Transform _transform;
    private GameObject _gameObject;
    
    private const string TargetTag = "Player";

    public void SetUp(EnemySettingsSO settings)
    {
        _damage = settings.Damage;
        _bulletSpeed = settings.BulletSpeed;
        _healthMax = settings.Health;
        _healthCurrent = settings.Health;
        _droppedCoins = settings.CoinsDroped;
        _healthBonusPercent = settings.HealthBonusPercent;
        _damageBoosterPercent = settings.DamageBoosterPercent;
        _damageBoosterTimer = settings.DamageBoosterTimer;
        _speedBoosterPercent = settings.SpeedBoosterPercent;
        _speedBoosterTimer = settings.SpeedBoosterTimer;
        _dropItemProbability = settings.DropItemProbability;
    }

    public void ReceiveDamage(int damage)
    {
        _healthCurrent -= damage;

        if (_healthCurrent <= 0)
        {
            StopCoroutine(WaitToShootBullet());
            DropItem();
            _gameObject.SetActive(false);
            OnEnemyDeath?.Invoke(_droppedCoins);
        }
    }

    private void DropItem()
    {
        if (DropItemAvailable())
        {
            switch (GetItemType())
            {
                case DroppableItemTypes.HealthRecharge:
                    DropHealthItem();
                    break;
                case DroppableItemTypes.StrengthBooster:
                    DropDamageItem();
                    break;
                case DroppableItemTypes.DexterityBooster:
                    DropSpeedItem();
                    break;
            }
        }
    }

    private bool DropItemAvailable()
    {
        int possibility = Random.Range(1, 101);
        return possibility <= _dropItemProbability;
    }

    private DroppableItemTypes GetItemType()
    {
        return (DroppableItemTypes) Random.Range(0, 3) ;
    }
    
    private void DropHealthItem()
    {
        DroppableItemController item = _healthItemsPool.Find(x => !x.gameObject.activeInHierarchy);

        if (item == null)
        {
            item = Instantiate(healthItemPrefab, _transform.position, Quaternion.identity);
            _healthItemsPool.Add(item);
        }
        else
        {
            item.ResetPosition(_transform.position);
        }
        
        item.AddPercent = _healthBonusPercent;
    }

    private void DropDamageItem()
    {
        BoosterItemController item = _damageItemsPool.Find(x => !x.gameObject.activeInHierarchy);

        if (item == null)
        {
            item = Instantiate(damageItemPrefab, _transform.position, Quaternion.identity);
            _damageItemsPool.Add(item);
        }
        else
        {
            item.ResetPosition(_transform.position);
        }

        item.AddPercent = _damageBoosterPercent;
        item.Timer = _damageBoosterTimer;
    }
    
    private void DropSpeedItem()
    {
        BoosterItemController item = _speedItemsPool.Find(x => !x.gameObject.activeInHierarchy);

        if (item == null)
        {
            item = Instantiate(speedItemPrefab, _transform.position, Quaternion.identity);
            _speedItemsPool.Add(item);
        }
        else
        {
            item.ResetPosition(_transform.position);
        }

        item.AddPercent = _speedBoosterPercent;
        item.Timer = _speedBoosterTimer;
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
        _healthItemsPool = new List<DroppableItemController>();
        _damageItemsPool = new List<BoosterItemController>();
        _speedItemsPool = new List<BoosterItemController>();
        
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
