using UnityEngine;

namespace Novus.PlayerEntity
{
    public class PlayerAttack : MonoBehaviour
    {
        #region EXPOSED_VARIABLES

        [Header("Primary Attack")] 
        [field: SerializeField] public float[] primaryAttackDamage;
        [field: SerializeField] public float[] primaryAttachKnockBack;
        [field: SerializeField] public Vector2[] primaryAttacksMovement;
        
        [Header("Counter")]
        [field: SerializeField] public float counterDamage = 5f;
        [field: SerializeField] public float counterDuration = 0.25f;

        #endregion

        #region PRIVATE_VARIABLES
        
        private Player _player;
        private int _currentAttackIndex = 0;

        #endregion

        #region PUBLIC_VARIABLES
        
        public float CurrentPrimaryAttackDamage { get; private set; }

        #endregion

        #region UNITY_CALLS

        public void InitAwake(Player player)
        {
            _player = player;
        }

        public void InitStart()
        {
            
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        
        public void SetCurrentAttackIndex(int index)
        {
            _currentAttackIndex = index;
            CurrentPrimaryAttackDamage = primaryAttackDamage[_currentAttackIndex];
        }

        #endregion

        
    }
}