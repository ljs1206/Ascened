using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using PJH.Agent.Animation;

public class EnemyMove : Action
{
    [SerializeField] private EnemyScript _sharedEnemy;
    private Enemy _enemy;
    private Transform _playerTrm;

    public override void OnAwake()
    {
        base.OnAwake();
        _enemy = _sharedEnemy.Value;
    }

    public override void OnStart()
    {
        _playerTrm = _enemy.PlayerTrm;
        EnemyMovement movement = _enemy.MovementCompo;

        movement.SetDirectMovement(_playerTrm.position, true);

        // movement.LookToTarget(_enemy.MovementCompo.GetNextPathPoint());
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}