using BehaviorDesigner.Runtime;

[System.Serializable]
public class EnemyScript : SharedVariable<Enemy>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator EnemyScript(Enemy value) { return new EnemyScript { mValue = value }; }
}