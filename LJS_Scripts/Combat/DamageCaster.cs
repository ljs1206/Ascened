using System;
using PJH.Combat;
using UnityEngine;
using static PJH.Core.MInterface;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 3f)]
    private float _casterRadius = 1f;

    public LayerMask targetLayer;
    private Enemy _owner;

    private Collider[] _detectedCol;

    #region Action
    public Action AttackCastEvent;
    #endregion

    public void InitCaster(Enemy enemy)
    {
        _owner = enemy;
        _detectedCol = new Collider[2];
    }

    public bool CastDamage()
    {
        AttackCastEvent?.Invoke();
        
        int cnt = Physics.OverlapSphereNonAlloc(
            transform.position,
            _casterRadius, _detectedCol, targetLayer);

        if(cnt > 0)
        {
            int power = (int)_owner.GetStat().power.GetValue();
            for(int i = 0; i < cnt; ++i){
                Vector3 hitPos = _detectedCol[i].ClosestPointOnBounds(_owner.transform.position);
                if(_detectedCol[i].TryGetComponent<IDamageable>(out IDamageable health))
                {
                    CombatData combatData = new CombatData
                    {
                        damage = power,
                        hitPoint = hitPos,
                        damageCategory = PJH.Core.Define.EDamageCategory.Normal
                        ,knockBackPower = power
                        ,knockBackDuration = 0.3f
                    };

                    health.ApplyDamage(combatData);
                }
            }
        }

        return cnt > 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _casterRadius);
        Gizmos.color = Color.white;
    }
#endif
}
