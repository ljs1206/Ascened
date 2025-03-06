using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsGuardNow : Conditional
{
    [SerializeField] private EnemyScript _sharedEnemy;
	private ShieldEnemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value as ShieldEnemy;
    }

    public override TaskStatus OnUpdate()
	{
		if(_enemy.Guard)
			return TaskStatus.Success;
		return TaskStatus.Failure;
	}
}