using System.Collections;
using PJH.Agent;
using PJH.Combat;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, INavigationable
{
    private NavMeshAgent _navAgent;
    private Rigidbody _rbCompo;
    public NavMeshAgent NavAgentCompo => _navAgent;
    public Rigidbody RigidbodyCompo => _rbCompo;

    [HideInInspector] public bool IsGround;
    public Vector3 Velocity => _navAgent.velocity;
    [HideInInspector] public bool ManualRotation;

    private Enemy _enemy;

    [SerializeField] private float _turnSpeed;

    private bool IsForce = false;

    public void Initialize(Agent agent)
    {
        _enemy = agent as Enemy;
        _rbCompo = GetComponent<Rigidbody>();
        _navAgent = GetComponent<NavMeshAgent>();
        // _navAgent.speed = _enemy.GetStat().moveSpeed.GetValue();

        _enemy.HealthCompo.ApplyDamagedEvent += HandleApplyDamageFunc;
        _enemy.AnimatorCompo.EnemyAnimatorTrigger.OnManualRotation += HandleManualRotFunc;
    }

    private void Update()
    {
        if (ManualRotation)
        {
            LookToTarget(_enemy.PlayerTrm.position);
        }
        if(IsForce == false){
            _rbCompo.isKinematic = true;
        }
    }

    private void HandleManualRotFunc(bool value) => ManualRotation = value;

    private void HandleApplyDamageFunc(int currentHealth)
    {
        CombatData combatData = _enemy.HealthCompo.combatData;
        StopImmediately();
        if (combatData.knockBackPower != 0)
            SetForce(combatData.knockBackDir, combatData.knockBackPower, combatData.knockBackDuration);
    }

    public void SetDirectMovement(Vector3 pos, bool setRot)
    {
        if (_navAgent.enabled == false) return;

        _navAgent.isStopped = false;
        if (setRot)
            LookToTarget(pos);

        _navAgent.SetDestination(pos);
    }

    public void StopImmediately()
    {
        if (!_navAgent.enabled) return;
        _navAgent.isStopped = true;
        _navAgent.velocity = Vector3.zero;
    }

    public void SetForce(Vector3 dir, float power, float duration)
    {
        dir.y = 0;
        StartCoroutine(AddForceCoro(dir, power, duration));
    }

    private IEnumerator AddForceCoro(Vector3 dir, float power, float duration)
    {
        IsForce = true;
        StopImmediately();
        _rbCompo.isKinematic = false;
        NavAgentCompo.enabled = false;
        _rbCompo.AddForce(dir * power, ForceMode.Impulse);

        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        NavAgentCompo.enabled = true;
        IsForce = false;
        if (_rbCompo.isKinematic) yield break;
        _rbCompo.linearVelocity = Vector3.zero;
        _rbCompo.isKinematic = true;
    }

    public void LookToTarget(Vector3 targetPos)
    {
        Vector3 toward = (targetPos - transform.position).normalized;
        toward.y = 0;
        if (toward.sqrMagnitude > 0.0001f)
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(toward), Time.deltaTime * _turnSpeed);
    }


    public Vector3 GetNextPathPoint()
    {
        NavMeshPath path = NavAgentCompo.path;

        if (path.corners.Length < 2)
        {
            return NavAgentCompo.destination;
        }

        for (int i = 0; i < path.corners.Length; ++i)
        {
            float distance = Vector3.Distance(NavAgentCompo.transform.position, path.corners[i]);
            if (distance < 1 && i < path.corners.Length - 1)
                return path.corners[i + 1];
        }

        return NavAgentCompo.destination;
    }


    /// <summary>
    /// When receiving a knockback use this Function
    /// later use this function
    /// </summary>
    private void DisableRigidbody()
    {
        _rbCompo.linearVelocity = Vector3.zero;
        _rbCompo.angularVelocity = Vector3.zero;
        _rbCompo.useGravity = false;
        _rbCompo.isKinematic = true;
    }
}