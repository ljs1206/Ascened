using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsAttackCooldown : Conditional
{
	[SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value as Enemy;
    }

    public override TaskStatus OnUpdate()
	{
		if(_enemy.lastAttackTime + _enemy.GetStat().attackCooldown.GetValue() <= Time.time)
			return TaskStatus.Success;
		return TaskStatus.Failure;
	}
}