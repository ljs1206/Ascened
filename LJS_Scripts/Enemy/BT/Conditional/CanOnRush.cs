using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class CanOnRush : Conditional
{
	[SerializeField] private EnemyScript _sharedEnemy;

	private ShieldEnemy _shieldEnemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_shieldEnemy = _sharedEnemy.Value as ShieldEnemy;
    }

    public override TaskStatus OnUpdate()
	{
		if(_shieldEnemy.IsFristAttack){
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}