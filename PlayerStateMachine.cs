using System;
using System.Collections.Generic;
using UnityEngine;

namespace Novus.PlayerEntity
{
    public class PlayerStateMachine 
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES
        
        private Player _player;

        #endregion

        #region PUBLIC_VARIABLES
        
        public PlayerState CurrentState { get; private set; }

        #endregion

        #region UNITY_CALLS
        
        public void Init(PlayerState startState)
        {
            CurrentState = startState;
            _player.CurrentState = CurrentState;
            CurrentState.Enter();
        }

        public void DeInit()
        {
            CurrentState.DeInit();
        }
        
        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        
        public PlayerStateMachine(Player player)
        {
            _player = player;
        }
        
        public void ChangeState(PlayerState newState)
        {
            // Debug.Log("Changing state from " + CurrentState._stateName + " to " + newState._stateName);
            CurrentState.Exit();
            CurrentState = newState;
            _player.CurrentState = CurrentState;
            CurrentState.Enter();
        }

        #endregion

        
    }
}