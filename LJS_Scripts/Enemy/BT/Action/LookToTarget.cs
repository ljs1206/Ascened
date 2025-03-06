using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class LookToTarget : Action
{
	[SerializeField] private EnemyScript _sharedEnemy;
	[SerializeField] private Transform _targetTrm;

	private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = _sharedEnemy.Value as Enemy;
    }

    public override void OnStart()
	{
		if(_targetTrm == null)
			_targetTrm = _enemy.PlayerTrm;
	}

	public override TaskStatus OnUpdate()
	{
		_enemy.MovementCompo.LookToTarget(_targetTrm.position);
		return TaskStatus.Success;
	}
}