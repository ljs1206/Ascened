using LJS;
using PJH.Agent.Animation;
using TrailsFX;
using UnityEngine;

public class ShieldEnemyAT : EnemyAnimatorTrigger
{
    private ShieldEnemy _shieldEnemy;
    [SerializeField] private AnimParamSO _breakParam;
    [SerializeField] private TrailEffect _shieldTrail;

    private void Start() {
        _shieldEnemy = _enemy as ShieldEnemy;
    }

    public void GuardBreakEnd(){
        EndHit();
        _enemy.AnimatorCompo.SetParam(_breakParam, false);
    }

    public void MoveWhileAttacking(){
        _shieldEnemy.AttackMove();
    }

    public void TurnOnSwordTrail(){
        _shieldTrail.active = true;
        _shieldTrail.enabled = true;
    }

    public void TurnOffSwordTrail(){
        _shieldTrail.active = false;
        _shieldTrail.enabled = false;
    }
}
