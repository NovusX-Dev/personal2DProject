using UnityEngine;

namespace Novus.PlayerEntity.Skills
{
    public class DashSkill : Skill
    {
        #region EXPOSED_VARIABLES
        
        [SerializeField] private float cooldown = 0.25f;

        #endregion

        #region PRIVATE_VARIABLES

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        #endregion

        #region PRIVATE_METHODS
        
        protected override void SetCooldownTimer()
        {
            cooldownTimer = cooldown;
        }

        #endregion

        #region PUBLIC_METHODS
        
        public override void UseSkill()
        {
            //TODO: Implement dash
        }

        #endregion
        
    }
}