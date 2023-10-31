using System;
using Novus;
using Novus.PlayerEntity.Skills;
using UnityEngine;

namespace Managers
{
    public class SkillsManager : SingletonT<SkillsManager>
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES


        #endregion

        #region PUBLIC_VARIABLES
        
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }


        #endregion

        #region UNITY_CALLS

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        #endregion


    }
}