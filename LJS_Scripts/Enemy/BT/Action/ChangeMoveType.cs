using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public enum LJSMoveType{
	Walk, Chase
}

public class ChangeMoveType : Action
{
	[SerializeField] private LJSMoveType _moveType;
	[SerializeField] private EnemyScript _sharedEnemy;
	private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value as Enemy;
    }

    public override TaskStatus OnUpdate()
	{
		if(_enemy.Slow) return TaskStatus.Success;
		EnemyStat stat = _enemy.GetStat();
		if(_moveType == LJSMoveType.Walk){
			_enemy.MovementCompo.NavAgentCompo.speed = stat.WalkSpeed.GetValue();
		}
		else{
			_enemy.MovementCompo.NavAgentCompo.speed = stat.ChaseSpeed.GetValue();
		}
		return TaskStatus.Success;
	}
}