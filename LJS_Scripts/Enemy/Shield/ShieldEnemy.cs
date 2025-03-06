using PJH.Agent.Animation;
using UnityEngine;

public class ShieldEnemy : Enemy, IAttackMoveable
{
    public ShieldEnemyAnimator ShieldEnemyAnimatorCompo { get; private set; }

    [HideInInspector] public bool IsFristAttack;
    private bool _guard = false;

    [HideInInspector]
    public bool Guard
    {
        get { return _guard; }
        set
        {
            ShieldEnemyAnimatorCompo.ChangeShieldAnimatorState(value);
            _guard = value;
        }
    }

    [SerializeField] private float _attackMoveSpeed = 0.2f;
    [SerializeField] private float[] _attack1FrontMoves;
    [SerializeField] private float[] _attack2FrontMoves;
    [SerializeField] private float[] _attack3FrontMoves;

    [SerializeField] private AnimParamSO _attackNumberParam;


    public override void Start()
    {
        base.Start();
        HealthCompo.DeathEvent += HandleDeadFunc;
    }

    private void HandleDeadFunc()
    {
        DeadEvent();
        // DeadFunc();
    }

    public override void DeadFunc()
    {
    }

    public override void ResetItem()
    {
        ShieldEnemyAnimatorCompo = AnimatorCompo as ShieldEnemyAnimator;

        Guard = true;
        base.ResetItem();
    }

    public override void SetUpPool(Pool pool)
    {
        base.SetUpPool(pool);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        HealthCompo.DeathEvent -= HandleDeadFunc;
    }

    public void OnRush(float moveTime)
    {
        ShieldEnemyAttack shieldEnemyAttack = EnemyAttackCompo as ShieldEnemyAttack;
        shieldEnemyAttack.OnRushFunc(moveTime);
    }

    public void AttackMove()
    {
        int attackNumber = AnimatorCompo.AnimatorCompo.GetInteger(_attackNumberParam.hashValue);
        float[] arr = new float[] { };
        switch (attackNumber)
        {
            case 0:
                arr = _attack1FrontMoves;
                break;
            case 1:
                arr = _attack2FrontMoves;
                break;
            case 2:
                arr = _attack3FrontMoves;
                break;
        }

        if (_currentIdx + 1 >= arr.Length)
        {
            _currentIdx = 0;
        }

        MovementCompo.SetForce(transform.forward, arr[_currentIdx], _attackMoveSpeed);
        _currentIdx++;
    }
}