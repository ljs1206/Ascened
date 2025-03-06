using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyAttack : Action
{
	[SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

	public override void OnAwake()
	{
		_enemy = _sharedEnemy.Value;
	}

    public override void OnStart()
    {
        base.OnStart();
		_enemy.MovementCompo.StopImmediately();
        _enemy.MovementCompo.LookToTarget(_enemy.PlayerTrm.position);
        _enemy.CanAttack = false;
        _enemy.CanAction = false;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}