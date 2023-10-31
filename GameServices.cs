using Novus.PlayerInput;
using UnityEngine;

namespace Novus
{
    public class GameServices : SingletonT<GameServices>
    {
        #region EXPOSED_VARIABLES
        
        [field: SerializeField] public InputReader inputReader { get; private set; } 

        #endregion

        
    }
}