using PJH.Agent.Player;
using PJH.Combat;
using UnityEngine;

public class GhostShot : Projectile, IPoolable
{
    [SerializeField] private PoolTypeSO _poolTypeSO;
    [SerializeField] private float _moveSpeed;
    public PoolTypeSO PoolType => _poolTypeSO;

    public GameObject GameObject => gameObject;

    private Rigidbody _rbCompo;

    private Pool _pool;

    protected override void Awake()
    {
        base.Awake();
        _rbCompo = GetComponent<Rigidbody>();
    }

    public override void Update()
    {
        base.Update();
    }

    private void Start()
    {
        _rbCompo.linearVelocity = transform.forward * _moveSpeed;
    }

    public void ResetItem()
    {
        _currentTime = 0;
        _rbCompo.linearVelocity = transform.forward * _moveSpeed;
    }

    public void SetUpPool(Pool pool)
    {
        _pool = pool;
        _rbCompo = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
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
        _pool.Push(this);
    }
}