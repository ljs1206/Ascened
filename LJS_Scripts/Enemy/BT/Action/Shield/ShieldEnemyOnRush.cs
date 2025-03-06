using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ShieldEnemyOnRush : Action
{
    [SerializeField] private EnemyScript _sharedEnemy;
    [SerializeField] private float _moveTime;

	private Enemy _enemy;
    public override void OnAwake()
    {
        _enemy = _sharedEnemy.Value as Enemy;
        base.OnAwake();
    }

    public override void OnStart()
    {
        base.OnStart();

        (_sharedEnemy.Value as ShieldEnemy).OnRush(_moveTime);
        (_sharedEnemy.Value as ShieldEnemy).IsFristAttack = false;
    }
}
