using System;
using Interfaces;
using UnityEngine;

namespace Novus.PlayerEntity
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES

        private Player _player = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        #endregion

        #region PRIVATE_METHODS
        
        private Collider2D[] GetHits()
        {
            return Physics2D.OverlapCircleAll(_player.attackCheck.position, _player.attackCheckRadius);
        }

        #endregion

        #region PUBLIC_METHODS

        public void OnAnimationFinishedTrigger()
        {
            _player.playerStatesController.StateMachine.CurrentState.SetAnimationFinishedTrigger();
        }

        public void OnTriggerDoDamage()
        {
            var hits = GetHits();
            foreach (var hit in hits)
            {
                if(hit.CompareTag(GlobalConstants.PlayerTag))
                    continue;
                if(hit.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_player.playerAttack.CurrentPrimaryAttackDamage);
            }
        }
        
        public void OnTriggerDoCounterDamage()
        {
            var hits = GetHits();
            foreach (var hit in hits)
            {
                if(hit.CompareTag(GlobalConstants.PlayerTag))
                    continue;
                if (hit.TryGetComponent(out ICounter countered))
                {
                    countered.TakeCounterDamage(_player.playerAttack.counterDamage);
                    _player.InvokeCounterSuccessful();
                }
            }
        }

        #endregion

        
    }
}