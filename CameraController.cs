using System;
using UnityEngine;
using Cinemachine;
using Managers;
using Novus.PlayerEntity;

namespace Novus.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        #region EXPOSED_VARIABLES
        
        [SerializeField] private CinemachineVirtualCamera mainVirtualCamera = null;
        
        [Header("Y Change")]
        [SerializeField] private float yChangeSpeed = 2f;
        [SerializeField] private float originalYOffset = 0.7f;
        [SerializeField] private Vector2 yChangeOffset = new Vector2(0f, 1.25f);

        #endregion

        #region PRIVATE_VARIABLES

        private float _yOffsetToChange = 0f;
        private CinemachineFramingTransposer _mainVirtualCameraTransposer = null;

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS

        private void OnEnable()
        {
            Player.OnYInputChanged += OnYInputChanged;
        }

        private void OnDisable()
        {
            Player.OnYInputChanged -= OnYInputChanged;
        }

        private void Awake()
        {
            _mainVirtualCameraTransposer = mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _yOffsetToChange = originalYOffset;
        }

        private void Update()
        {
            UpdateYOffset();
        }

        #endregion

        #region PRIVATE_METHODS

        private void OnYInputChanged(float yInput)
        {
            _yOffsetToChange = yInput switch
            {
                > 0 => yChangeOffset.y,
                < 0 => yChangeOffset.x,
                _ => originalYOffset
            };
        }

        private void UpdateYOffset()
        {
            if(Math.Abs(_mainVirtualCameraTransposer.m_ScreenY - _yOffsetToChange) > 0.01f)
                _mainVirtualCameraTransposer.m_ScreenY = Mathf.Lerp(_mainVirtualCameraTransposer.m_ScreenY, _yOffsetToChange, yChangeSpeed * Time.deltaTime);
        }

        #endregion

        #region PUBLIC_METHODS

        #endregion

        
    }
}