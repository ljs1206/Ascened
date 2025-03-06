using LJS;
using UnityEngine;

public class MeleeEnemyAT : EnemyAnimatorTrigger
{
    private MeleeEnemy _meleeEnemy;
    [SerializeField] private GameObject _swordTrail;
    
    private void Start(){
        _meleeEnemy = _enemy as MeleeEnemy;
        
        _meleeEnemy.HealthCompo.ApplyDamagedEvent += HandleDamageFunc;
    }

    private void HandleDamageFunc(int obj)
    {
        _swordTrail.SetActive(false);
    }

    public void MoveWhileAttacking(){
        _meleeEnemy.AttackMove();
    }

    public void TurnOnSwordTrail(){
        _swordTrail.SetActive(true);
    }

    public void TurnOffSwordTrail(){
        _swordTrail.SetActive(false);
    }
}
