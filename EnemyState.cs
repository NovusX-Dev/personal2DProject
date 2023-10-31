using UnityEngine;

namespace Novus.Enemy
{
    [System.Serializable]
    public class EnemyState 
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES
        
        protected bool _triggerCalled;
        protected float _stateTimer;
        private int _animBoolHash;
        protected Enemy _enemyBase;
        protected EnemyStateMachine _stateMachine;
        
        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        public virtual void Update()
        {
            _stateTimer -= Time.deltaTime;
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        
        public EnemyState(Enemy enemyBaseBase, EnemyStateMachine stateMachine, int animBoolHash)
        {
            this._enemyBase = enemyBaseBase;
            this._stateMachine = stateMachine;
            this._animBoolHash = animBoolHash;
        }

        public virtual void Enter()
        {
            _triggerCalled = false; 
            _enemyBase.anim.SetBool(_animBoolHash, true);
        }
        
        public virtual void Exit()
        {
            _enemyBase.anim.SetBool(_animBoolHash, false);
        }

        public virtual void AnimationFinishTrigger()
        {
            _triggerCalled = true;
        }
        

        #endregion


    }
}