using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class GhostHide : Action 
{
	[SerializeField] private EnemyScript _sharedEnemy;
    [SerializeField] private bool _StartORStop;

    private GhostEnemy _ghostEnmy;

    public override void OnAwake()
    {
        base.OnAwake();
        _ghostEnmy = _sharedEnemy.Value as GhostEnemy;
    }

    public override void OnStart()
    {
        base.OnStart();
        if(_StartORStop)
            _ghostEnmy.GhostHideCompo.StartHide();
        else
            _ghostEnmy.GhostHideCompo.StopHide();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
