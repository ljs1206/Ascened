using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class InCombat : Action
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
		_enemy.Combat = true;
	}
}