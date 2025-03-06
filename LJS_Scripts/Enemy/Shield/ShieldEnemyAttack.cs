using System.Collections;
using UnityEngine;

public class ShieldEnemyAttack : EnemyAttackCompo
{
    private ShieldEnemy _shieldEnemy;

    private void Start(){
        _shieldEnemy = _enemy as ShieldEnemy;
    }

    public void OnRushFunc(float time){
        StartCoroutine(OnRushCoro(time));
    }
    
    private IEnumerator OnRushCoro(float time){
        float currentTime = 0;

        while(currentTime < time){
            currentTime += Time.deltaTime;
            _shieldEnemy.MovementCompo.SetDirectMovement(_shieldEnemy.PlayerTrm.position, true);
            if(Vector3.Distance(_shieldEnemy.transform.position, _shieldEnemy.transform.position) < 0.5f)
                break;
            yield return null;
        }
        _shieldEnemy.AnimatorCompo.SetParam(
            (_shieldEnemy.AnimatorCompo as ShieldEnemyAnimator).OnRushBoolParam, false);
    }
}
