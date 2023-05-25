using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameState : MonoBehaviour
    {
        public enum GameStateMode { Gameplay, Pause }

        private void Awake()
        {
            if (_instance == null || _instance != this)
            {
                _instance = this;
                _crrState = GameStateMode.Gameplay;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private static GameState _instance;

        private GameStateMode _crrState;
        public static GameStateMode CrrState => _instance._crrState;

        public static event Action<GameStateMode> OnGameStateChanges;

        public static void SetState(GameStateMode state)
        {
            _instance._crrState = state;
            
            OnGameStateChanges?.Invoke(_instance._crrState);
        }
    }
}