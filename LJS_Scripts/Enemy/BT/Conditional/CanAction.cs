using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CanAction : Conditional
{
    [SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value;
    }

    public override TaskStatus OnUpdate()
	{
		if(_enemy.CanAction){
			return TaskStatus.Success;
		}
		else{
			return TaskStatus.Failure;
		}
	}
}
