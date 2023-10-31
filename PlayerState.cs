using Novus.PlayerEntity.States;
using Novus.PlayerInput;
using UnityEngine;

namespace Novus.PlayerEntity
{
    [System.Serializable]
    public class PlayerState
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES

        private int _animBoolName;
        protected float _stateTimer = 0f;
        protected bool _animFinishedTrigger = false;
        public string _stateName = "";
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        protected Player _player;
        protected PlayerStatesController _statesController;
        protected PlayerStateMachine _stateMachine;
        protected Rigidbody2D _rb;
        protected InputReader _inputReader;
        #endregion

        #region PUBLIC_VARIABLES
        
        public bool StateEntered = false;
        
        #endregion

        #region UNITY_CALLS
        
        public PlayerState(Player player, PlayerStateMachine stateMachine, InputReader inputReader, int animBoolHash)
        {
            _stateName = GetType().Name;
            _player = player;
            _statesController = _player.playerStatesController;
            _rb = _player.rb;
            _stateMachine = stateMachine;
            _animBoolName = animBoolHash;
            _inputReader = inputReader;
        } 
        
        public virtual void Update()
        {
            _stateTimer -= Time.deltaTime;
            _player.anim.SetFloat(YVelocity, _rb.velocity.y);
        }
        
        public virtual void FixedUpdate()
        {
            
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        
        public virtual void Enter()
        {
            StateEntered = true;

            _player.anim.SetBool(_animBoolName, true);
            _animFinishedTrigger = false;
        }
        
        public virtual void Exit()
        {
            _player.anim.SetBool(_animBoolName, false);
            StateEntered = false;

        }

        public virtual void DeInit()
        {
            
        }

        public virtual void SetAnimationFinishedTrigger()
        {
            _animFinishedTrigger = true; 
        }

        #endregion

    }
}