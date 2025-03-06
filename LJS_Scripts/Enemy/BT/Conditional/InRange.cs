
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InRange : Conditional
{
	[SerializeField] private SharedFloat _radius;
	[SerializeField] private SharedLayerMask _whatIsPlayer;
	[SerializeField] private bool _showGizmos;

	public override TaskStatus OnUpdate()
	{
		bool isScucces = Physics.CheckSphere(Owner.transform.position, _radius.Value, _whatIsPlayer.Value);
		if(isScucces) return TaskStatus.Success;
		else return TaskStatus.Failure;
	}

	public override void OnDrawGizmos()
	{
		if(!_showGizmos) return;
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(Owner.transform.position, _radius.Value);
	}
}