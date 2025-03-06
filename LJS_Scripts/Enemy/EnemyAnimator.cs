using FIMSpace.FProceduralAnimation;
using LJS;
using PJH.Agent;
using PJH.Agent.Animation;
using UnityEngine;
using System;

public class EnemyAnimator : AgentAnimator
{
    public LegsAnimator LegsAnimator {get; private set;}
    public EnemyAnimatorTrigger EnemyAnimatorTrigger {get; private set;}
    [SerializeField] protected AnimParamSO _hitIntParam;
    [SerializeField] protected AnimParamSO _hitBoolParam;

    #region Action
    public Action EnemyAttackSoundEvent;
    #endregion

    protected Enemy _enemy;

    #region SoundFunc
    public void EnemyAttackSoundFunc() => EnemyAttackSoundEvent?.Invoke();
    #endregion

    public override void Initialize(Agent agent)
    {
        base.Initialize(agent);
        _enemy = agent as Enemy;
        LegsAnimator = GetComponent<LegsAnimator>();
        EnemyAnimatorTrigger = GetComponent<EnemyAnimatorTrigger>();
        SetRootMotion(false);
    }

    public override void AfterInit()
    {
        base.AfterInit();
        _enemy.HealthCompo.ApplyDamagedEvent += HandleDamageFunc;
    }

    #region Hit
    public virtual void HandleDamageFunc(int damage)
    {
        if(_enemy.HealthCompo.IsDead) return;
        _enemy.MovementCompo.StopImmediately();
        _enemy.CanAction = false;
        SetParam(_hitIntParam, (int)_enemy.CalculateHitPointDir());
        AllBoolParamatorToFlase();
        SetParam(_hitBoolParam, true);
    }
#endregion

    public void AllBoolParamatorToFlase(){
        foreach (var item in AnimatorCompo.parameters)
        {
            if(item.type == AnimatorControllerParameterType.Bool)
                AnimatorCompo.SetBool(item.nameHash, false);
        }
    }
}
