using Managers;
using UnityEngine;

namespace Novus.PlayerEntity.Skills
{
    public class CloneSkill : Skill
    {
        #region EXPOSED_VARIABLES
        
        [SerializeField] private float cooldown = 1.5f;
        
        [Header("Clone Info")]
        [SerializeField] private ClonePlayer clonePrefab = null;
        [SerializeField] private float cloneDuration = 1.5f;
        [SerializeField] private float cloneFadeDuration = 1f;
        [SerializeField] private float cloneAttackPower = 2f;

        [Header("DEBUG")] [SerializeField] private bool canAttack = false;

        #endregion

        #region PRIVATE_VARIABLES

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        protected override void Start()
        {
            base.Start();
            
            _inputReader.OnCloneSkillEvent += UseSkill;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _inputReader.OnCloneSkillEvent -= UseSkill;
        }

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
            var newClone = Instantiate(clonePrefab);
            var startPos = PlayerManager.Instance.player.transform.position;
            
            newClone.Setup(startPos, cloneDuration, cloneFadeDuration, canAttack, cloneAttackPower);
        }

        #endregion
       
    }
}