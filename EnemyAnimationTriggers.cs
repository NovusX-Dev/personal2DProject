using System;
using Interfaces;
using UnityEngine;

namespace Novus.Enemy.Skeleton
{
    public class EnemyAnimationTriggers : MonoBehaviour
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES
        
        private Enemy _enemy = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        public void AnimationFinishTrigger()
        {
            _enemy.OnCurrentStateAnimFinishTrigger();
        }
        
        public void OnAttackTrigger()
        {
            var hits = Physics2D.OverlapCircleAll(_enemy.attackCheck.position, _enemy.attackCheckRadius);
            foreach (var hit in hits)
            {
                if(hit.TryGetComponent(out IDamageable damageable) && hit.CompareTag(GlobalConstants.PlayerTag))
                {damageable.TakeDamage(_enemy.attackDamage);}
            }
        }

        public void OnOpenCounterWindow()
        {
            _enemy.SetCounterWindow(true);
        }
        
        public void OnCloseCounterWindow()
        {
            _enemy.SetCounterWindow(false);
        }

        #endregion

        
    }
}