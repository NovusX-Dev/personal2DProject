using System;
using System.Collections;
using Managers;
using Novus.PlayerEntity.States;
using Novus.PlayerEntity.States.Attack;
using Novus.PlayerInput;
using UnityEngine;

namespace Novus.PlayerEntity
{
    public class Player : Entity
    {
        public event Action OnCounterSuccessful = null;
        public static event Action<float> OnYInputChanged = null;
        
        #region EXPOSED_VARIABLES
        
        [Header("Movement")]
        [field: SerializeField] public float moveSpeed = 12f;
        [field: SerializeField] public float jumpForce = 12f;
        [field: SerializeField] public float wallJumpForce = 5f;
        [field: SerializeField] public float airDrag = 0.8f;
        [field: SerializeField] public float wallSlideDrag = 0.7f;
        
        [Header("Dash")]
        [field: SerializeField] public float dashForce = 12f;
        [field: SerializeField] public float dashDuration = 1.5f;
        
        [Header("References")] 
        [field: SerializeField] public PlayerAttack playerAttack = null;
        [field: SerializeField] public PlayerStatesController playerStatesController = null;

        #endregion

        #region PRIVATE_VARIABLES
        
        private Coroutine _busyCoroutine = null;
        //for debugging
        public PlayerState CurrentState { get; set; }
        private InputReader _inputReader = null;

        #endregion

        #region PUBLIC_VARIABLES
        
        public float XInput { get; private set; }
        public float YInput { get; private set; }
        public float DashDirection { get; private set; }
        public bool IsDashing { get; set; }
        public bool IsBusy { get; private set; }
        
        

        #endregion

        #region UNITY_CALLS

        protected override void OnEnable()
        {
            _inputReader.OnDashEvent += OnDash;
            _inputReader.OnMoveEvent += OnMove;
            _inputReader.OnCounterAttackEvent += OnCounterAttack;
        }

        protected override void OnDisable()
        {
            _inputReader.OnDashEvent -= OnDash;
            _inputReader.OnMoveEvent -= OnMove;
            _inputReader.OnCounterAttackEvent -= OnCounterAttack;
        }

        protected override void Awake()
        {
            base.Awake();
            _inputReader = GameServices.Instance.inputReader;
            
            playerStatesController.InitAwake(this, _inputReader);
            
            playerAttack.InitAwake(this);
        }

        protected override void Start()
        {
            base.Start();
            playerStatesController.InitStart();
            playerAttack.InitStart();
        }

        protected override void Update()
        {
            base.Update();
            playerStatesController.OnUpdate();
            anim.SetFloat(GlobalConstants.PlayerYVelocityAnimParameter, rb.velocity.y);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            playerStatesController.OnFixedUpdate();
        }

        #endregion

        #region PRIVATE_METHODS
        
        private void OnMove(Vector2 moveDirection)
        {
            XInput = moveDirection.x == 0f ? 0f : Mathf.Sign(moveDirection.x);
            YInput = IsGrounded() ? 0f :moveDirection.y;
            
            if(playerStatesController.StateMachine.CurrentState == playerStatesController.IdleState)
                OnYInputChanged?.Invoke(moveDirection.y);
        }
        
        private void OnDash()
        {
            if(SkillsManager.Instance.dashSkill.CanUseSkill() || IsWallDetected()) return;
            IsDashing = true;
            DashDirection = XInput;
            if(DashDirection == 0f) DashDirection = FacingDirection;
            playerStatesController.StateMachine.ChangeState(playerStatesController.DashState);
        }
        
        private void OnCounterAttack()
        {
            playerStatesController.StateMachine.ChangeState(playerStatesController.CounterAttackState);
            BlockGameInput(true);
        }

        #endregion

        #region PUBLIC_METHODS

        public bool IsRunningSameDirection()
        {
            return Math.Abs(XInput - FacingDirection) < 0.01f;
        }

        public void SetBusyCoroutine(bool start, float duration = 0f)
        {
            if (start)
                _busyCoroutine = StartCoroutine(BusyRoutine(duration));
            else
            {
                if(_busyCoroutine!= null) StopCoroutine(_busyCoroutine);
            }

            IEnumerator BusyRoutine(float duration)
            {
                IsBusy = true;
                yield return new WaitForSeconds(duration);
                IsBusy = false;
                _busyCoroutine = null;
            }
            
        }
        
        public void BlockGameInput(bool blocked)
        {
            _inputReader.BlockGameInput(blocked);
        }
        
        public void InvokeCounterSuccessful()
        {
            OnCounterSuccessful?.Invoke();
        }
        
        #endregion

        
    }

}
