using System;
using System.Collections;
using UnityEngine;

namespace Novus
{
    public class EntityVfx : MonoBehaviour
    {
        #region EXPOSED_VARIABLES
        
        [Header("Flash VFX")]
        [SerializeField] private int flashRepeat = 1;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private Material hitMaterial = null;

        #endregion

        #region PRIVATE_VARIABLES
        
        private WaitForSeconds _flashWait = null;
        private Coroutine _flashCoroutine = null;
        private Coroutine _counteredRedFlashCoroutine = null;
        private Material _defaultMaterial = null;
        private SpriteRenderer _spriteRenderer = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
            _flashWait = new WaitForSeconds(flashDuration);
        }

        #endregion

        #region PRIVATE_METHODS
        
        private IEnumerator FlashVfx()
        {
            var repeat = flashRepeat;
            while(repeat > 0)
            {
                _spriteRenderer.material = hitMaterial;
                yield return _flashWait;
                _spriteRenderer.material = _defaultMaterial;
                yield return _flashWait;
                repeat--;
            }

            _flashCoroutine = null;
        }
        
        private IEnumerator CounterRedFlashVfx()
        {
            while(true)
            {
                _spriteRenderer.color = Color.red;
                yield return _flashWait;
                _spriteRenderer.color = Color.white;
                yield return _flashWait;
            }
        }

        #endregion

        #region PUBLIC_METHODS
        
        public void StartFlashRoutine()
        {
            if(_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);
            _flashCoroutine = StartCoroutine(FlashVfx());
        }
        
        public void SetCounterRedFlashRoutine(bool start)
        {
            if (start)
            {
                if(_counteredRedFlashCoroutine != null)
                    StopCoroutine(_counteredRedFlashCoroutine);
                _counteredRedFlashCoroutine = StartCoroutine(CounterRedFlashVfx());
            }
            else
            {
                if(_counteredRedFlashCoroutine != null)
                    StopCoroutine(_counteredRedFlashCoroutine);
                _spriteRenderer.color = Color.white;
                _counteredRedFlashCoroutine = null;
            }
        }

        #endregion

        
    }
}