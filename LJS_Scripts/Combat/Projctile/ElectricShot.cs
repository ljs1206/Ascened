using System;
using PJH.Agent.Player;
using PJH.Combat;
using PJH.Equipment.Weapon;
using PJH.Manager;
using PJH.Scene;
using UnityEngine;

public class ElectricShot : Projectile, IPoolable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PoolTypeSO _poolTypeSO;
    public PoolTypeSO PoolType => _poolTypeSO;

    public GameObject GameObject => gameObject;

    private Transform PlayerTrm;
    private Rigidbody _rbCompo;

    protected override void Awake()
    {
        base.Awake();
        _rbCompo = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (!Managers.Scene.CurrentScene)
        {
            PlayerTrm = GameObject.FindWithTag("Player").transform;
        }
        else
        {
            PlayerTrm = ((GameScene)Managers.Scene.CurrentScene).Player.transform;
        }
    }

    public override void Update()
    {
        base.Update();
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 dir = (PlayerTrm.position - transform.position).normalized;
        dir.y = 0;
        _rbCompo.linearVelocity = dir * _moveSpeed;
    }

    public void ResetItem()
    {
        _currentTime = 0;
    }

    public void SetUpPool(Pool pool)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) || other.TryGetComponent(out PlayerWeapon Weapon))
        {
            Debug.Log("??");
            CombatData combatData = new CombatData();
            combatData.damage = 5;
            combatData.damageCategory = PJH.Core.Define.EDamageCategory.Normal;
            player.HealthCompo.ApplyDamage(combatData);
            DeleteProjectile();
        }
        // DeleteProjectile();
    }

    protected override void DeleteProjectile()
    {
        var deadParticle = _poolManager.Pop(_deadParticle);
        deadParticle.GameObject.transform.position = transform.position;
        _poolManager.Push(this);
    }
}