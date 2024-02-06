using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, ITarget
{
    public event Action OnPlayerDeath;
    
    [SerializeField] 
    private Rigidbody playerRigidbody;
    [SerializeField] 
    private BulletController bulletPrefab;
    
    private float _speed = 4.0f;
    private float _rotationSpeed = 100f;
    private int _healthMax = 100;
    private int _healthCurrent = 10;
    private int _damage = 10;
    private float _bulletSpeed = 10;
    private Transform _transform;
    private List<BulletController> _bulletsPool;
    
    private const string TargetTag = "Enemy";

    public void ReceiveDamage(int damage)
    {
        _healthCurrent -= damage;
        
        if(_healthCurrent <= 0)
            OnPlayerDeath?.Invoke();
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
