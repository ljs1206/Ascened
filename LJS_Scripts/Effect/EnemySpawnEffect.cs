using System.Collections;
using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using PJH.Manager;
using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolTypeSO _poolType;
    private PoolManagerSO _poolManager;
    [SerializeField] private float _delayTime;
    public PoolTypeSO PoolType => _poolType;

    public GameObject GameObject => gameObject;

    private ParticleSystem _spawnParticle;

    public void ResetItem()
    {
        gameObject.SetActive(true);
        _spawnParticle.Play();
        StartCoroutine(DelayCallbackCoro(_delayTime));
    }

    private IEnumerator DelayCallbackCoro(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _spawnParticle.Stop();
        _poolManager.Push(this);
    }

    public void SetUpPool(Pool pool)
    {
        _poolManager = Managers.Addressable.Load<PoolManagerSO>("PoolManager");

        _spawnParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
    }
}