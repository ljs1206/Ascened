using PJH.Stat;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Enemy Stat", fileName = "Enemy Stat")]
public class EnemyStat : AgentStat
{
        public Stat MaxHealth;
        public Stat ChaseSpeed;
        public Stat WalkSpeed;
        public Stat power;
        public Stat attackCooldown;

        // protected Dictionary<EPlayerStatType, Stat> _statDictionary;

        // private void OnEnable()
        // {
        //     RegisterStat(out _statDictionary);
        // }

        // public void AddModifier(EPlayerStatType type, int value)
        // {
        //     _statDictionary[type].AddModifier(value);
        // }

        // public void RemoveModifier(EPlayerStatType type, int value)
        // {
        //     _statDictionary[type].AddModifier(value);
        // }
}
