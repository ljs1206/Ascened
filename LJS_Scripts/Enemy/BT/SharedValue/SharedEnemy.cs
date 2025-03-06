using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedEnemy : SharedVariable<Enemy>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedEnemy(Enemy value) { return new SharedEnemy { mValue = value }; }
}