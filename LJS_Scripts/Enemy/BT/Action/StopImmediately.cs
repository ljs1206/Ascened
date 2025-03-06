using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class StopImmediately : Action
{
	[SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value as Enemy;
    }

    public override void OnStart()
	{
		_enemy.MovementCompo.StopImmediately();
	}
}