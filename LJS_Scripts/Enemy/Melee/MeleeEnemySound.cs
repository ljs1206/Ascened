using System;
using BehaviorDesigner.Runtime;
using FMODUnity;
using LJS;
using Sirenix.OdinInspector;
using UnityEngine;

public class MeleeEnemySound : EnemySound
{
    private MeleeEnemy _meleeEnemy;

    private DamageCaster _meleeEnemyCaster;
    private int _currentAttackNumber;

    [field: BoxGroup("Sound")] 
    [Header("Sound Ref")]
    [SerializeField] private EventReference slashAttackType1;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference slashAttackType2;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference slashAttackType3;
    
    public void Start(){
        _meleeEnemy = _enemy as MeleeEnemy;

        Transform casterTrm = _meleeEnemy.transform.Find("DamageCaster");
        if (casterTrm)
            _meleeEnemyCaster = casterTrm.GetComponent<DamageCaster>();

        _meleeEnemyCaster.AttackCastEvent += HandleSlashAttackFunc;
    }

    private void HandleSlashAttackFunc()
    {
        _currentAttackNumber = (int)_meleeEnemy.BehaviourTree.GetVariable("AttackNumber").GetValue();
        Debug.Log("Melee Sound");

        switch(_currentAttackNumber){
            case 0:
                SalshAttackType2Play();
            break;
            case 1:
                SalshAttackType2Play();
            break;
            case 2:
                SalshAttackType3Play();
            break;
        }
    }

    public void SalshAttackType1Play() => RuntimeManager.PlayOneShot(slashAttackType1, transform.position);
    public void SalshAttackType2Play() => RuntimeManager.PlayOneShot(slashAttackType2, transform.position);
    public void SalshAttackType3Play() => RuntimeManager.PlayOneShot(slashAttackType3, transform.position);

    public void OnDestroy(){
        _meleeEnemyCaster.AttackCastEvent -= HandleSlashAttackFunc;
    }
}
