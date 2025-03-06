using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttack : Conditional
{
	[SerializeField] private SharedFloat _radius;
	[SerializeField] private EnemyScript _sharedEnemy;
	[SerializeField] private SharedLayerMask _whatIsPlayer;
	[SerializeField] private bool _showGizmos;
	private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
		_enemy = _sharedEnemy.Value;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
	{
		EnemyMovement movement = _enemy.MovementCompo;
		bool isScucces = Physics.CheckSphere(Owner.transform.position, _radius.Value, _whatIsPlayer.Value);
		bool cooldownPass = _enemy.lastAttackTime + _enemy.GetStat().attackCooldown.GetValue() <= Time.time;

		if(isScucces){
			movement.LookToTarget(_enemy.PlayerTrm.position);
			movement.StopImmediately();
			if(_enemy.CanAttack && cooldownPass) return TaskStatus.Success;
			else return TaskStatus.Failure;
		}
		return TaskStatus.Failure;
	}

	public override void OnDrawGizmos()
	{
		if(!_showGizmos) return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Owner.transform.position, _radius.Value);
	}
}