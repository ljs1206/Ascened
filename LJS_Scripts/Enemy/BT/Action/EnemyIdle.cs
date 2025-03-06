using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyIdle : Action
{
    [SerializeField] private EnemyScript _sharedEnemy;
    private Enemy _enemy;

    public override void OnAwake()
    {
        base.OnAwake();
        _enemy = _sharedEnemy.Value;
    }

    public override void OnStart()
    {
        Debug.Log(011000);
        _enemy.MovementCompo.StopImmediately();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}