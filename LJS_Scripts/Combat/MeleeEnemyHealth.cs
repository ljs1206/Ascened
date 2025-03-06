using System;
using LJS;
using PJH.Agent.Player;
using PJH.Combat;
using PJH.Equipment.Weapon;
using PJH.Manager;
using PJH.Scene;

public class MeleeEnemyHealth : Health
{
    private MeleeEnemy _meleeEnemy;
    private Player _player;
    public event Action BlockedHitEvent;

    protected override void Awake()
    {
        base.Awake();
        _meleeEnemy = GetComponent<MeleeEnemy>();
        SetMaxHealth((int)_meleeEnemy.GetStat().MaxHealth.GetValue());
    }

    protected override void Start()
    {
        base.Start();
        GameScene gameScene = (Managers.Scene.CurrentScene as PJH.Scene.GameScene);
        if(gameScene){
            _player = gameScene.Player;
        }
    }

    public override void ApplyDamage(CombatData combatData)
    {
        base.ApplyDamage(combatData);
    }
}