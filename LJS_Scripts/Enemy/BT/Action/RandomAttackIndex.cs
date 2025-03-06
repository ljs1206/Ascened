using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomAttackIndex : Action
{
    public SharedInt min;
    public SharedInt max;
    public SharedInt storeResult;

    public override void OnStart()
    {
        storeResult.Value = Random.Range(min.Value, max.Value + 1);
    }
}
