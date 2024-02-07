using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, ITarget
{
    public event Action OnPlayerDeath;
    public event Action<int> OnPlayerDamage; 

    [SerializeField] 
    private Rigidbody playerRigidbody;
    [SerializeField] 
    private BulletController bulletPrefab;
    
    private float _speed;
    private float _rotationSpeed;
    private int _currentHealth;
    private int _healthMax;
    private int _damage;
    private float _bulletSpeed;
    private Transform _transform;
    private List<BulletController> _bulletsPool;
    
    private const string TargetTag = "Enemy";

    public void ReceiveDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnPlayerDamage?.Invoke(0);
            OnPlayerDeath?.Invoke();
        }
        else
        {
            OnPlayerDamage?.Invoke(_currentHealth);
        }
    }

    public void SetUp(PlayerSettingsSO settings)
    {
        _speed = settings.Speed;
        _rotationSpeed = settings.RotationSpeed;
        _currentHealth = settings.Health;
        _healthMax = settings.Health;
        _damage = settings.Damage;
        _bulletSpeed = settings.BulletSpeed;
    }

    public void ChangeSpeed(int newSpeed)
    {
        _speed = newSpeed;
    }

    public void ChangeHealth(int newHealth)
    {
        _healthMax = newHealth;
    }

    public void ChangeDamage(int newDamage)
    {
        _damage = newDamage;

        for (int i = 0; i < _bulletsPool.Count; i++)
        {
            _bulletsPool[i].ChangeDamage(_damage);
        }
    }

    private void Awake()
    {
        _transform = transform;
        _bulletsPool = new List<BulletController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            ShootBullet();
    }

    private void ShootBullet()
    {
        Vector3 rotation = new Vector3(0, _transform.rotation.eulerAngles.y + 90, 90);
        BulletController bullet = GetAvailableBullet();
        bullet.Move(rotation);
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

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        playerRigidbody.position += _transform.forward * (z * Time.deltaTime * _speed);
        
        Vector3 rotation = new Vector3(0f, x, 0f) * Time.deltaTime * _rotationSpeed;
        playerRigidbody.rotation = Quaternion.Euler(playerRigidbody.rotation.eulerAngles + rotation);
    }
}
