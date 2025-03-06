using BehaviorDesigner.Runtime.Tasks;

public class IsGround : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}