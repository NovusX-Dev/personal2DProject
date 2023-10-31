using System;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Novus
{
    public abstract class Health : MonoBehaviour, IDamageable
    {
        #region EXPOSED_VARIABLES

        [SerializeField] private float maxHealth = 10f;
        
        [Header("KnockBack")]
        [SerializeField] private float knockBackForce = 10f;
        [SerializeField] private float knockBackCounterMultiplier = 2.5f;
        [SerializeField] private float knockBackDuration = 0.1f;
        [SerializeField] private ForceMode2D knockBackForceMode = ForceMode2D.Impulse;
        
        #endregion

        #region PRIVATE_VARIABLES
        
        protected float _currentHealth = 0f;
        protected bool _isKnockedBack = false;
        protected bool _isCountered = false;
        private WaitForSeconds _knockBackWait = null;
        protected Rigidbody2D _rb = null;
        protected Entity _entity = null;
        protected EntityVfx _entityVfx = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS
        
        public virtual void InitAwake(Entity entity, EntityVfx vfx, Rigidbody2D rb)
        {
            _entity = entity;
            _entityVfx = vfx;
            _rb = rb;
            _currentHealth = maxHealth;
            _knockBackWait = new WaitForSeconds(knockBackDuration);
        }

        #endregion

        #region PRIVATE_METHODS

        protected virtual void Die()
        {
            _currentHealth = 0f;
            // Destroy(gameObject);
        }

        protected virtual void DamagedVfx()
        {
            if(_entityVfx != null) _entityVfx.StartFlashRoutine();

            if(knockBackForce <= 0f) return;
            StartCoroutine(KnockBackRoutine());
        }

        private IEnumerator KnockBackRoutine()
        {
            var force = _isCountered ? knockBackForce * knockBackCounterMultiplier : knockBackForce;
            _isKnockedBack = true;
            _entity.SetKnockBack(true);
            _entity.rb.AddForce(Vector2.one * (force * -_entity.FacingDirection), knockBackForceMode);
            yield return _knockBackWait;
            _isKnockedBack = false;
            _entity.SetKnockBack(false);
            _isCountered = false;
            if(_currentHealth <= 0f)
                Die();
        }

        #endregion

        #region PUBLIC_METHODS
        
        public virtual void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            DamagedVfx();
        }

        #endregion


        
    }
}