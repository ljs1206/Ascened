using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SetGuardValue : Action
{
	[SerializeField] private EnemyScript _sharedEnemy;
	[SerializeField] private bool value;

	private ShieldEnemy _shieldEnemy;

	public override void OnStart()
	{
		_shieldEnemy = _sharedEnemy.Value as ShieldEnemy;
		_shieldEnemy.Guard = value;
	}
}	