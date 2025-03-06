using LJS;
using UnityEngine;

public class GhostEnemyAT : EnemyAnimatorTrigger
{
    [SerializeField] private PoolTypeSO _soulball, _electricball;
    [SerializeField] private GameObject _SoulballPrefab, _SoulCharingPrefab, _Eball;
    [SerializeField] private Transform _rightCastTrm, _leftCastTrm;

    private GameObject _soulCharing;

    private GhostEnemy _ghostEnemy;
    private void Start() {
        _ghostEnemy = _enemy as GhostEnemy;
    }
    
    public void MoveWhileAttacking(){
        _ghostEnemy.AttackMove();
    }

    public void CastSoulBall(){
        var projectile = _ghostEnemy.PoolManager.Pop(_soulball);
        projectile.GameObject.transform.rotation = _ghostEnemy.transform.rotation;
        projectile.GameObject.transform.position = _leftCastTrm.position;
    }

    public void CastElectricBall(){
        var projectile = _ghostEnemy.PoolManager.Pop(_electricball);
        projectile.GameObject.transform.rotation = _ghostEnemy.transform.rotation;
        projectile.GameObject.transform.position = _rightCastTrm.position;
    }

    public void SCharingEffectStart(){
        if(_soulCharing != null) return;
        
        _ghostEnemy.StartDelayCallback(0.01f, () => {
            GameObject obj = Instantiate(_SoulCharingPrefab, _leftCastTrm);
            _soulCharing = obj;
            _soulCharing.transform.localPosition = Vector3.zero;
        });
    }

    public void GhostEnemyCastType1Play(){
        _ghostEnemy.GhostEnemySound.CastSoundType1Play();
    }

    public void GhostEnemyCastType2Play(){
        _ghostEnemy.GhostEnemySound.CastSoundType2Play();
    }

    public void SCharingEffectStop(){
        Destroy(_soulCharing);
        _soulCharing = null;
    }

    public void AllEffectDelete(){
        Destroy(_soulCharing);
    }
}
