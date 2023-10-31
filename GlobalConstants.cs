using UnityEngine;

namespace Novus
{
    public class GlobalConstants 
    {
        #region Tags

        public const string PlayerTag = "Player";
        public const string EnemyTag = "Enemy";

        #endregion
        #region Player_Animator_Hashes
       
        public static readonly int PlayerJumpAnimBoolHash = Animator.StringToHash("jump");
        public static readonly int PlayerDashAnimBoolHash = Animator.StringToHash("dash");
        public static readonly int PlayerWallSlideAnimBoolHash = Animator.StringToHash("wallSlide");
        public static readonly int PlayerPrimaryComboCounter = Animator.StringToHash("primaryComboCounter");
        public static readonly int PlayerCounterAnimBoolHash = Animator.StringToHash("counterAttack");
        public static readonly int PlayerCounterSuccessAnimBoolHash = Animator.StringToHash("counterAttackSuccess");

        #endregion

        #region Player_Animator_Parameters

        public static readonly string PlayerYVelocityAnimParameter = "yVelocity";

        #endregion

        #region Common

         public static readonly int IdleAnimBoolHash = Animator.StringToHash("idle");
         public static readonly int MoveAnimBoolHash = Animator.StringToHash("move");
         public static readonly int PrimaryAttackAnimBoolHash = Animator.StringToHash("attack");
         public static readonly int StunnedAnimBoolHash = Animator.StringToHash("stunned");
         public static readonly int DieAnimBoolHash = Animator.StringToHash("die");

        #endregion
        
    }
}