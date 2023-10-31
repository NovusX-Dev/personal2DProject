using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Novus.CameraSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundParallax : MonoBehaviour
    {
        #region EXPOSED_VARIABLES
        
        [SerializeField] private float parallaxEffectMultiplier;
        [SerializeField] private bool infiniteScroll = false;
        
        #endregion

        #region PRIVATE_VARIABLES

        private Transform _thisTransform = null;
        private Transform _cameraTransform = null;
        private Camera _camera = null;
        private float _startPos;
        private float _length;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void Start()
        {
            _camera = Camera.main;
            _thisTransform = this.transform;
            _cameraTransform = _camera.transform;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            if(_camera == null) return;

            var camPosition = _cameraTransform.position;
            var distanceToMove = camPosition.x * parallaxEffectMultiplier;
            _thisTransform.position = new Vector3(_startPos + distanceToMove, transform.position.y);

            //infinite scroll
            if (!infiniteScroll) return;
            var distanceMoved = camPosition.x * (1 - parallaxEffectMultiplier);
            if (distanceMoved > _startPos + _length)
                _startPos += _length;
            else if (distanceMoved < _startPos + _length)
                _startPos -= _length;

        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        #endregion

        
    }
}