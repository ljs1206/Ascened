using System;
using System.Collections;
using FIMSpace.FProceduralAnimation;
using PJH.Agent.Animation;
using UnityEngine;

namespace LJS
{
    public class EnemyAnimatorTrigger : MonoBehaviour
    {
        public LegsAnimator LegsAnimator { get; private set; }

        public event Action<bool> OnManualRotation;

        [SerializeField] protected Enemy _enemy;
        [HideInInspector] public SkinnedMeshRenderer sRenderer;
        [SerializeField, Range(0, 2)] private float _dissovleTime;
        [SerializeField, Range(0.1f, 0.9f)] private float _slowSpeed;
        private float _originAnimationSpeed;

        [Header("Anim Param")] [SerializeField]
        protected AnimParamSO _attackParam;

        [SerializeField] protected AnimParamSO _idleParam;
        [SerializeField] protected AnimParamSO _hitParam;

        private void Awake()
        {
            LegsAnimator = GetComponent<LegsAnimator>();
        }

        private void Start()
        {
            // LegsAnimator.User_FadeEnabled(0.1f);
        }

        public void SetAnimationSlow(){
            Animator animator = _enemy.AnimatorCompo.AnimatorCompo;
            _originAnimationSpeed = animator.speed;
            animator.speed = _slowSpeed;
        }

        public void RollBackAnimationSpeed(){
            _enemy.AnimatorCompo.AnimatorCompo.speed = _originAnimationSpeed;
        }

        public void AnimationEnd()
        {
            // LegsAnimator.User_FadeEnabled(0.1f);
            _enemy.AnimatorCompo.SetParam(_attackParam, false);
            _enemy.AnimatorCompo.SetParam(_idleParam, true);

            _enemy.StartDelayCallback(0.1f, () =>
            {
                _enemy.CanAttack = true;
                _enemy.CanAction = true;
                _enemy.lastAttackTime = Time.time;
            });
        }

        private void StartManualRot() => OnManualRotation?.Invoke(true);
        private void StopManualRot() => OnManualRotation?.Invoke(false);

        public void CastDamage()
        {
            _enemy.Attack();
        }

        public void EndHit()
        {
            _enemy.BehaviourTree.enabled = true;
            _enemy.AnimatorCompo.SetParam(_hitParam, false);
            _enemy.AnimatorCompo.SetParam(_idleParam, true);
            _enemy.CanAction = true;
            _enemy.CanAttack = true;
        }

        public void DeadEnd()
        {
            StartCoroutine(DissolveCoro());
        }

        private IEnumerator DissolveCoro()
        {
            float currentTime = 0;
            float value = 0;
            while(currentTime < _dissovleTime){
                currentTime += Time.deltaTime;
                value = Mathf.Lerp(0, 1, currentTime);
                sRenderer.material.SetFloat("_DissolveAmount", value);
                yield return null;
            }
            sRenderer.material.SetFloat("_DissolveAmount", 1);
            SpawnDeadParticle();
        }

        public void CastDamaged()
        {
            _enemy.DamageCasterCompo.CastDamage();
        }

        private void SpawnDeadParticle()
        {
            // 파티클 소환
            _enemy.Pool.Push(_enemy);
        }
    }
}