using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ReSettingValue : Action
{
	public SharedBool isRun;
	public override void OnStart()
	{
		isRun.Value = false;
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}