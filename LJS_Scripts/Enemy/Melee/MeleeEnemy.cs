using PJH.Agent.Animation;
using UnityEngine;

namespace LJS
{
    public class MeleeEnemy : Enemy, IAttackMoveable
    {
        [SerializeField] private float _attackMoveSpeed = 0.2f;
        [SerializeField] private float[] _attack1FrontMoves;
        [SerializeField] private float[] _attack2FrontMoves;
        [SerializeField] private float[] _attack3FrontMoves;

        [SerializeField] private AnimParamSO _attackNumberParam;

        #region overrided

        public override void Start()
        {
            HealthCompo.DeathEvent += HandleDeadFunc;

            base.Start();
        }

        public override void Attack()
        {
            base.Attack();
        }

        public override void DeadFunc()
        {
            DeadEvent();
        }

        public void AttackMove()
        {
            int attackNumber = AnimatorCompo.AnimatorCompo.GetInteger(_attackNumberParam.hashValue);
            float[] arr = new float[] { };
            switch (attackNumber)
            {
                case 0:
                    arr = _attack1FrontMoves;
                    break;
                case 1:
                    arr = _attack2FrontMoves;
                    break;
                case 2:
                    arr = _attack3FrontMoves;
                    break;
            }

            if (_currentIdx + 1 >= arr.Length)
            {
                _currentIdx = 0;
            }

            MovementCompo.SetForce(transform.forward, arr[_currentIdx], _attackMoveSpeed);
            _currentIdx++;
        }

        #endregion

        private void HandleDeadFunc()
        {
            DeadFunc();
        }

        public override void ResetItem()
        {
            base.ResetItem();
        }

        public override void SetUpPool(Pool pool)
        {
            base.SetUpPool(pool);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            HealthCompo.DeathEvent -= HandleDeadFunc;

        }
    }
}