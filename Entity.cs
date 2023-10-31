using System;
using UnityEngine;

namespace Novus
{
    [RequireComponent(typeof(EntityVfx))]
    public class Entity : MonoBehaviour
    {
        #region EXPOSED_VARIABLES
        
        [Header("Ground Check")]
        [SerializeField] protected Transform groundCheck = null;
        [SerializeField] protected float groundCheckRadius = 0.3f;
        [SerializeField] protected Transform wallCheck = null;
        [SerializeField] protected float wallCheckRadius = 0.3f;
        [SerializeField] protected LayerMask groundLayer;
        
        [Header("Attack Check")]
        [field: SerializeField] public Transform attackCheck = null;
        [field: SerializeField] public float attackCheckRadius = 2f;

        #endregion

        #region PRIVATE_VARIABLES

        protected bool _isKnockedBack = false;
        protected Health _health = null;

        #endregion

        #region PUBLIC_VARIABLES
        
        public int FacingDirection { get; protected set; }
        public bool IsFacingRight  { get; protected set; }

        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public EntityVfx entityVfx { get; private set; }

        #endregion

        #region UNITY_CALLS
        
        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }

        protected virtual void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            entityVfx = GetComponent<EntityVfx>();
            _health = GetComponent<Health>();
            IsFacingRight = true;
            
            _health.InitAwake(this, entityVfx, rb);
        }

        protected virtual void Start()
        {
            FacingDirection = 1;
        }
        
        protected virtual void Update()
        {
            
        }
        
        protected virtual void FixedUpdate()
        {
            
        }

        #endregion

        #region PRIVATE_METHODS
        
        private void FlipController(float xInput)
        {
            if(xInput > 0 && !IsFacingRight)
                Flip();
            else if(xInput < 0 && IsFacingRight)
                Flip();
        }

        #endregion

        #region PUBLIC_METHODS
        
        public bool IsGrounded()
        {
            return Physics2D.Raycast(groundCheck.position,  Vector2.down, groundCheckRadius ,groundLayer);
        }
        
        public bool IsWallDetected()
        {
            return Physics2D.Raycast(wallCheck.position,  Vector2.right * FacingDirection, wallCheckRadius ,groundLayer);
        }
        
        public void Flip()
        {
            FacingDirection *= -1;
            IsFacingRight = !IsFacingRight;
            
            transform.Rotate(0f, 180f, 0f);
        }
        
        public void SetVelocity(float xVelocity, float yVelocity)
        {
            if(_isKnockedBack) return;
            
            var direction = new Vector2(xVelocity, yVelocity);
            rb.velocity = direction;
            FlipController(xVelocity);
        }

        public void SetKnockBack(bool isKnocked)
        {
            _isKnockedBack = isKnocked;
        }

        #endregion

        protected virtual void OnDrawGizmos()
        {
            if(groundCheck != null)
                Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckRadius));;
            
            if(wallCheck != null)
                Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckRadius * FacingDirection, wallCheck.position.y));

            if (attackCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius); 
            }
        }
    }
}