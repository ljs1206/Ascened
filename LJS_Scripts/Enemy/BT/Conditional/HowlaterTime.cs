using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HowlaterTime : Conditional
{
    [SerializeField] private float _time;

    private float _currentTime;

    public override void OnAwake(){
        _currentTime = 0;
    }

    public override TaskStatus OnUpdate()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _time){
            _currentTime = 0;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
