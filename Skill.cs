using System;
using Novus.PlayerInput;
using UnityEngine;

namespace Novus.PlayerEntity.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES

        protected float cooldownTimer = 0f;
        protected InputReader _inputReader = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS


        protected virtual void OnDisable()
        {
            
        }

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            SetCooldownTimer();
            _inputReader = GameServices.Instance.inputReader;
        }
        
        protected virtual void Update()
        {
            if(cooldownTimer > 0f)
                cooldownTimer -= Time.deltaTime;
        }

        #endregion

        #region PRIVATE_METHODS
        
        protected abstract void SetCooldownTimer();

        #endregion

        #region PUBLIC_METHODS

        public bool CanUseSkill()
        {
            if (!(cooldownTimer < 0f)) return false;
            
            UseSkill();
            SetCooldownTimer();
            return true;
        }
        
        public abstract void UseSkill();

        #endregion

    }
}