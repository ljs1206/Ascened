using System;
using FMODUnity;
using LJS;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShieldEnemySound : EnemySound
{
    private ShieldEnemy _shieldEnemy;

    private DamageCaster _meleeEnemyCaster;
    private int _currentAttackNumber;

    [field: BoxGroup("Sound")] 
    [Header("Sound Ref")]
    [SerializeField] private EventReference shieldAttackType1;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference shieldAttackType2;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference guardBreak;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference guard;
    
    public void Start(){
        _shieldEnemy = _enemy as ShieldEnemy;

        Transform casterTrm = _shieldEnemy.transform.Find("DamageCaster");
        if (casterTrm)
            _meleeEnemyCaster = casterTrm.GetComponent<DamageCaster>();

        _meleeEnemyCaster.AttackCastEvent += HandleShieldAttackFunc;

        ShieldEnemyAnimator shieldEnemyAnimator = (_shieldEnemy.AnimatorCompo as ShieldEnemyAnimator);
        shieldEnemyAnimator.GuardBreakEvent += HandleGuardBreakFunc;
        shieldEnemyAnimator.GuardEvent += HandleGuardFunc;
    }

    private void HandleGuardFunc() => RuntimeManager.PlayOneShot(guardBreak, transform.position);
    private void HandleGuardBreakFunc() => RuntimeManager.PlayOneShot(guard, transform.position);

    private void HandleShieldAttackFunc()
    {
        _currentAttackNumber = (int)_shieldEnemy.BehaviourTree.GetVariable("AttackNumber").GetValue();
        Debug.Log("Shield Sound");

        switch(_currentAttackNumber){
            case 0:
                ShieldAttackType1Play();
            break;
            case 1:
                ShieldAttackType2Play();
            break;
        }
    }

    public void ShieldAttackType1Play() => RuntimeManager.PlayOneShot(shieldAttackType1, transform.position);
    public void ShieldAttackType2Play() => RuntimeManager.PlayOneShot(shieldAttackType2, transform.position);

    public void OnDestroy(){
        _meleeEnemyCaster.AttackCastEvent -= HandleShieldAttackFunc;
    }
}
