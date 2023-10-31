using System;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Novus.PlayerEntity.Skills
{
    public class ClonePlayer : MonoBehaviour
    {
        #region EXPOSED_VARIABLES
        
        [SerializeField] private Transform attackCheck = null;
        [SerializeField] private float attackCheckRadius = 15f;

        #endregion

        #region PRIVATE_VARIABLES
        
        private bool _setupFinished = false;
        private bool _cloneFaded = false;
        private bool _canAttack = false;
        private float _cloneDuration = 0f;
        private float _colorFadeDuration = 0f;
        private float _attackPower = 0f;
        private readonly int _attackHash = Animator.StringToHash("attackCount");
        private Collider2D[] _hits = Array.Empty<Collider2D>();
        private SpriteRenderer _spriteRenderer = null;
        private Animator _animator = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void Update()
        {
            if (!_setupFinished) return;

            if (_cloneDuration > 0f)
            {
                _cloneDuration -= Time.deltaTime;
            }
            else if(!_cloneFaded)
            {
                _spriteRenderer.DOFade(0f, _colorFadeDuration).OnComplete(() =>
                {
                    _cloneFaded = true;
                });
            }

            if (!_cloneFaded) return;
            DOTween.KillAll();
            Destroy(gameObject);
        }

        #endregion

        #region PRIVATE_METHODS
        
        private Collider2D[] GetHits()
        {
            return Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        }

        private void FaceClosestEnemy(Collider2D[] hits)
        {
            Transform closestEnemy = null;
            var closestDistance = float.MaxValue;
            
            foreach (var hit in hits)
            {
                if(hit.CompareTag(GlobalConstants.PlayerTag))
                    continue;
                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    var closestDistanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                    if (closestDistanceToEnemy < closestDistance)
                    {
                        closestDistance = closestDistanceToEnemy;
                        closestEnemy = hit.transform;
                    }
                }
            }
            
            if(closestEnemy != null && transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0f, 180f, 0f);
            }
        }

        #endregion

        #region PUBLIC_METHODS

        public void Setup(Vector3 startPos, float cloneDuration, float colorFadeDuration, bool canAttack, float attackPower)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            transform.position = startPos;
            _cloneDuration = cloneDuration;
            _colorFadeDuration = colorFadeDuration;
            _attackPower = attackPower;
            _setupFinished = true;
            _cloneFaded = false;
            _canAttack = canAttack;
            _hits = GetHits();
            if(_hits.Length == 0) return;
            FaceClosestEnemy(_hits);
            
            if (_canAttack)
            {
                _animator.SetInteger(_attackHash, Random.Range(1, 4));
            }
        }
        
        public void OnAnimationFinishedTrigger()
        {
            _cloneDuration = -1f;
            _animator.SetInteger(_attackHash, 0);
        }

        public void OnTriggerDoDamage()
        {
            foreach (var hit in _hits)
            {
                if(hit.CompareTag(GlobalConstants.PlayerTag))
                    continue;
                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_attackPower);
                }
            }
        }

        #endregion

        
    }
}