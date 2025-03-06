using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EndCombat : Action
{
	[SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

	public override void OnStart()
	{
		_enemy = _sharedEnemy.Value;
		_enemy.Combat = false;
	}
}