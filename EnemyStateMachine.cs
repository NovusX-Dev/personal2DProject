using UnityEngine;

namespace Novus.Enemy
{
    public class EnemyStateMachine 
    {
        #region EXPOSED_VARIABLES

        #endregion

        #region PRIVATE_VARIABLES
        
        public EnemyState CurrentState { get; private set; }

        #endregion

        #region PUBLIC_VARIABLES

        #endregion

        #region UNITY_CALLS
        
        public void Init(EnemyState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        
        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        #endregion

    
    }
}
