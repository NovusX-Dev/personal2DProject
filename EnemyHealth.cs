using Interfaces;
using UnityEngine;

namespace Novus.Enemy
{
    public class EnemyHealth : Health, ICounter
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES
        
        private Enemy _enemy = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS
        
        public void InitEnemyModules(Enemy enemy)
        {
            _enemy = enemy;
        }

        #endregion

        #region PRIVATE_METHODS

        protected override void Die()
        {
            base.Die();
            _enemy.EnemyDied();
        }

        #endregion

        #region PUBLIC_METHODS
        
        public void TakeCounterDamage(float damage)
        {
            if (_enemy.CanBeCountered())
            {
                _isCountered = true;
                TakeDamage(damage);
            }
        }

        #endregion


        
    }
}