using System;
using PJH.Manager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected PoolTypeSO _deadParticle;
    protected PoolManagerSO _poolManager;
    [SerializeField] protected float _deleteTime;
    protected float _currentTime;

    protected virtual void Awake()
    {
        _poolManager = Managers.Addressable.Load<PoolManagerSO>("PoolManager");
    }

    public virtual void Update()
    {
        if (_currentTime >= _deleteTime)
        {
            DeleteProjectile();
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    protected virtual void DeleteProjectile()
    {
    }
}