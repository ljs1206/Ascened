using System;
using PJH.Agent.Player;
using PJH.Combat;
using PJH.Equipment.Weapon;
using PJH.Manager;
using PJH.Scene;
using UnityEngine;

public class GhostHelath : Health
{
        private GhostEnemy _ghostEnemy;
        private Player _player;
        public event Action BlockedHitEvent;

        protected override void Awake()
        {
            base.Awake();
            _ghostEnemy = GetComponent<GhostEnemy>();
            SetMaxHealth((int)_ghostEnemy.GetStat().MaxHealth.GetValue());
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
            var playerWeapon = _player.GetCompo<PlayerEquipmentController>().GetWeapon();
            if (_ghostEnemy.Hide && (playerWeapon is Hammer || playerWeapon is Sword))
            {
                combatData.damage = 0;
                this.combatData = combatData;

                ShowPopupDamageText();
            }
            else
            {
                base.ApplyDamage(combatData);
            }
        }
}
