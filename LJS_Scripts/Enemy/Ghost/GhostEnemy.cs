using PJH.Agent.Animation;
using PJH.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostEnemy : Enemy
{
    [SerializeField] private float _attackMoveSpeed = 0.2f;
    [SerializeField] private float[] _attack1FrontMoves;
    [SerializeField] private float[] _attack2FrontMoves;
    [SerializeField] private float[] _attack3FrontMoves;
    [SerializeField] private AnimParamSO _attackNumberParam;

    [Header("Sound")]
    [HideInInspector] public GhostEnemySound GhostEnemySound;

    public bool Hide;

    #region Compo
    public GhostEnemyHide GhostHideCompo {get; private set;}
    #endregion
    
    public override void Start()
    {
        base.Start();
        Transform visualTrm = transform.Find("Visual");
        GhostHideCompo = visualTrm.GetComponent<GhostEnemyHide>();
        
        HealthCompo.DeathEvent += HandleDeadFunc;
    }

    public override void Attack()
    {
        base.Attack();
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

    public override void DeadFunc()
    {
        DeadEvent();
    }

    private void HandleDeadFunc()
    {
        DeadFunc();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        HealthCompo.DeathEvent -= HandleDeadFunc;
    }
    public override void ResetItem()
    {
        base.ResetItem();
    }

    public override void SetUpPool(Pool pool)
    {
        base.SetUpPool(pool);
    }
}
