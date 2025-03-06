using PJH.Manager;
using UnityEngine;

public class EnemyHitImpact : MonoBehaviour, IPoolable
{
    private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _poolType;
    public PoolTypeSO PoolType => _poolType;

    public GameObject GameObject => gameObject;

    private ParticleSystem _hitparticle;

    public void ResetItem()
    {
        _hitparticle.Play();
    }

    public void SetUpPool(Pool pool)
    {
        _poolManager = Managers.Addressable.Load<PoolManagerSO>("PoolManager");

        _hitparticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_hitparticle.isStopped)
        {
            _poolManager.Push(this);
        }
    }
}