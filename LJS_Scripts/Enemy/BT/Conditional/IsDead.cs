using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : Conditional
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
        if (_enemy.HealthCompo.IsDead)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}