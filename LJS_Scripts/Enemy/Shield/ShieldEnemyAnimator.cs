using System;
using PJH.Agent.Animation;
using PJH.Agent.Player;
using PJH.Core;
using PJH.Manager;
using PJH.Scene;
using UnityEngine;

public class ShieldEnemyAnimator : EnemyAnimator
{
    public AnimParamSO OnRushBoolParam;

    [SerializeField] private RuntimeAnimatorController _originController;
    [SerializeField] private AnimatorOverrideController _breakController;

    [Header("AnimParam")]
    [SerializeField] private AnimParamSO _breakParam;

    private ShieldEnemy _shieldEnemy;

    #region Action
    public Action GuardBreakEvent;
    public Action GuardEvent;
    #endregion

    private void Start(){
        _shieldEnemy = _enemy as ShieldEnemy;
    }

    public void ChangeShieldAnimatorState(bool value)
    {
        if (value)
            AnimatorCompo.runtimeAnimatorController = _originController;
        else
            AnimatorCompo.runtimeAnimatorController = _breakController;
    }

    public override void HandleDamageFunc(int damage)
    {
        if(_enemy.HealthCompo.IsDead) return;
        
        _enemy.BehaviourTree.enabled = false;
        _shieldEnemy.CanAction = false;

        _shieldEnemy.MovementCompo.StopImmediately();

        SetParam(_hitIntParam, (int)_shieldEnemy.CalculateHitPointDir());
        AllBoolParamatorToFlase();

        if(_shieldEnemy.Guard
            && ((GameScene)Managers.Scene.CurrentScene).Player.GetCompo<PlayerEquipmentController>().GetWeapon().name ==
                Define.EPlayerEquipmentType.Hammer.ToString())
        {
            GuardBreakEvent?.Invoke();
            _shieldEnemy.Guard = false;
            SetParam(_breakParam, true);
        }
        else{
            GuardEvent?.Invoke();
        }
        SetParam(_hitBoolParam, true);
    }
}


