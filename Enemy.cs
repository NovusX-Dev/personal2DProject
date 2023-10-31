using System;
using System.Collections;
using UnityEngine;

namespace Novus.Enemy
{
    public class Enemy : Entity
    {
        #region EXPOSED_VARIABLES
        
        [Header("Movement")]
        [field: SerializeField] public float moveSpeed;
        [field: SerializeField] public float idleTime;
        
        [Header("Countering")]
        [field: SerializeField] public float stunnedTime = 0.7f;
        [SerializeField] protected GameObject counterVfx = null;

        [Header("Attack")] 
        [field: SerializeField] public float attackDamage = 2f;
        [field: SerializeField] public float attachRange = 5f;
        [field: SerializeField] public float attackRate = 1f;
        [field: SerializeField] public float agroTime = 1f;
        [field: SerializeField] public float detectionRange = 30f;
        [SerializeField] protected LayerMask playerLayer;
        #endregion

        #region PRIVATE_VARIABLES

        protected bool _canBeCountered = true;
        private WaitForSeconds _flipWaitTime;
        protected Coroutine _flipCoroutine;
        private EnemyHealth _enemyHealth;
        protected Collider2D _mainCollider;

        #endregion

        #region PUBLIC_VARIABLES

        public float LastTimeAttack {get; set;}
        public Rigidbody2D rbody { get; private set; }
        public EnemyStateMachine StateMachine { get; private set; }

        #endregion

        #region UNITY_CALLS

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine();
            rbody = GetComponent<Rigidbody2D>();
            _flipWaitTime = new WaitForSeconds(idleTime);
            _enemyHealth = (EnemyHealth)_health;
            // _enemyHealth = GetComponent<EnemyHealth>();
            _mainCollider = GetComponent<Collider2D>();
            _enemyHealth.InitEnemyModules(this);
        }
        
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            StateMachine.CurrentState.Update();
        }

        #endregion

        #region PRIVATE_METHODS

        public virtual bool CanBeCountered()
        {
            if (!_canBeCountered) return false;
            
            SetCounterWindow(false);
            return true;
        }

        #endregion

        #region PUBLIC_METHODS

        public void StartFlipCoroutine()
        {
            _flipCoroutine = StartCoroutine(FlipRoutine());
            
            IEnumerator FlipRoutine()
            {
                yield return _flipWaitTime;
                Flip();
                _flipCoroutine = null;
            }
        }

        public RaycastHit2D IsPlayerInRange()
        {
            return Physics2D.CircleCast(wallCheck.position, detectionRange, FacingDirection * Vector2.right, 0f, playerLayer);
        }

        // public RaycastHit2D IsPlayerDetected()
        // {
        //     return Physics2D.Raycast(wallCheck.position, FacingDirection * Vector2.right, detectionRange, playerLayer);
        // }
        //
        // public bool IsPlayerBehindMe()
        // {
        //     return Physics2D.Raycast(wallCheck.position, -FacingDirection * Vector2.right, detectionRange / 2f, playerLayer);
        // }

        public void OnCurrentStateAnimFinishTrigger()
        {
            StateMachine.CurrentState.AnimationFinishTrigger();
        }
        
        public void SetCounterWindow(bool canBeCountered)
        {
            _canBeCountered = canBeCountered;
            counterVfx.SetActive(canBeCountered);
        }
        
        public virtual void EnemyDied()
        {
            SetVelocity(0f, 0f);
            rb.isKinematic = true;
            _mainCollider.enabled = false;
            counterVfx.SetActive(false);
        }

        #endregion

    
    }
}
