using BehaviorDesigner.Runtime;
using PJH.Agent;
using PJH.Agent.Animation;
using PJH.Core;
using PJH.Manager;
using PJH.Scene;
using UnityEngine;
using UnityEngine.AI;

public enum HitDir
{
    Front,
    Back,
    Left,
    Right
}

public abstract class Enemy : Agent, IPoolable, MInterface.IEnemy
{
    #region BaseSetting

    public bool CanAttack;
    public bool Combat;
    public bool CanAction;
    public bool Slow;


    [Header("Animator Setting")] [SerializeField]
    protected AnimParamSO _DeadParam;

    #endregion

    [Header("Attack Settings")] [HideInInspector]
    public float lastAttackTime;

    [SerializeField] private PoolTypeSO _poolType;
    private PoolManagerSO _poolManager;
    public PoolTypeSO PoolType => _poolType;
    public GameObject GameObject => gameObject;

    public PoolManagerSO PoolManager { get; private set; }
    public Pool Pool { get; private set; }
    public Transform PlayerTrm { get; private set; }
    [SerializeField] private Transform _testPlayerTrm;
    protected int _currentIdx;
    private CapsuleCollider _collider;

    #region Compo

    public EnemyMovement MovementCompo { get; private set; }
    public EnemyAnimator AnimatorCompo { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public BehaviorTree BehaviourTree { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public EnemyAttackCompo EnemyAttackCompo { get; private set; }

    #endregion

    #region PoolFunc

    public virtual void SetUpPool(Pool pool)
    {
        Pool = pool;
        PoolManager = _poolManager;
    }

    public virtual void ResetItem()
    {
        HealthCompo.IsDead = false;
        AnimatorCompo.SetParam(_DeadParam, false);
        AnimatorCompo.EnemyAnimatorTrigger.sRenderer.material.SetFloat("_DissolveAmount", 0);

        _collider.enabled = true;

        gameObject.layer = 6;
        BehaviourTree.DisableBehavior();
        BehaviourTree.enabled = true;
        NavAgent.enabled = false;

        HealthCompo.enabled = true;
        HealthCompo.SetMaxHealth((int)GetStat().MaxHealth.GetValue());

        CanAttack = true;
        CanAction = true;
    }

    public void SetSlow(bool vlaue, float slowValue = 1)
    {
        Slow = vlaue;
        MovementCompo.NavAgentCompo.speed = slowValue;
        AnimatorCompo.AnimatorCompo.speed = slowValue;
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        _poolManager = Managers.Addressable.Load<PoolManagerSO>("PoolManager");

        _collider = GetComponent<CapsuleCollider>();

        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<EnemyAnimator>();


        MovementCompo = GetComponent<EnemyMovement>();
        MovementCompo.Initialize(this);

        Transform attackTrm = transform.Find("EnemyAttack");
        if (attackTrm)
            EnemyAttackCompo = attackTrm.GetComponent<EnemyAttackCompo>();

        BehaviourTree = GetComponent<BehaviorTree>();
        NavAgent = GetComponent<NavMeshAgent>();

        Transform casterTrm = transform.Find("DamageCaster");
        if (casterTrm)
        {
            DamageCasterCompo = casterTrm.GetComponent<DamageCaster>();
            DamageCasterCompo.InitCaster(this);
        }
    }

    public virtual void Start()
    {
        PlayerTrm = (Managers.Scene.CurrentScene as GameScene).Player.transform;

        CanAttack = true;
        CanAction = true;
    }

    private void Update(){
        if(HealthCompo.IsDead && AnimatorCompo.AnimatorCompo.GetBool("Dead") == false){
            DeadEvent();
        }
    }

    public virtual void Attack()
    {
        // CastDamage
    }

    public abstract void DeadFunc();

    public new EnemyStat GetStat() => _agentStat as EnemyStat;

    public void ChangeNavAgentState(bool value)
    {
        if (!value)
        {
            NavAgent.velocity = Vector3.zero;
            NavAgent.path.ClearCorners();
        }

        NavAgent.enabled = value;
    }

    protected void DeadEvent()
    {
        MovementCompo.ManualRotation = false;
        CanAttack = false;
        CanAction = false;

        _collider.enabled = false;
        gameObject.layer = 30;
        BehaviourTree.enabled = false;
        NavAgent.enabled = false;
        HealthCompo.enabled = false;

        AnimatorCompo.AllBoolParamatorToFlase();
        AnimatorCompo.SetParam(_DeadParam, true);
        EnemySpawnManager.Instance.DeleteObject(this);

        StartDelayCallback(0.5f, () =>
        {
            AnimatorCompo.AllBoolParamatorToFlase();
            AnimatorCompo.SetParam(_DeadParam, true);
        });
    }

    public HitDir CalculateHitPointDir()
    {
        HitDir currentDir = HitDir.Front;
        Transform playerTrm = PlayerTrm;
        Vector3 dir = playerTrm.position - transform.position;

        Vector3 crossLR = Vector3.Cross(transform.forward, dir);
        Vector3 crossF = Vector3.Cross(transform.right, dir);

        if ((crossF.y < 0.3f && crossF.y > -0.3f)
            && (crossLR.y < 0.3f && crossLR.y > -0.3f))
        {
            if (crossLR.y < 0)
            {
                currentDir = HitDir.Left;
            }
            else
            {
                currentDir = HitDir.Right;
            }
        }

        if (crossF.y > 0)
        {
            currentDir = HitDir.Back;
        }
        else if (crossF.y < 0)
        {
            currentDir = HitDir.Front;
        }
        else if (crossF.y < 0.3f && crossF.y > -0.3f)
        {
            if (crossLR.y < 0)
            {
                currentDir = HitDir.Left;
            }
            else
            {
                currentDir = HitDir.Right;
            }
        }

        return currentDir;
    }
}