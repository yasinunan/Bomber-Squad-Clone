using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace YU.Template
{
    [DefaultExecutionOrder(-1)]
    public class GameEngine : Singleton<GameEngine>
    {
        public enum GameState
        {
            NONE,
            INITIALIZING,
            INITIALIZED,
            PREPARING_NEW_GAME,
            PLAYING,
            PAUSED,
            FINISHED,
            RESUMING,
            READY_TO_RUN,
            STARTING_TO_RUN
        }

        [SerializeField] public GameState gameState;

        //___________________________________________________________________________________________________

        public delegate void OnGameInitializeDelegate();
        public event OnGameInitializeDelegate OnGameInitialize;

        public delegate void OnGameStateChangeDelegate(GameState oldGamestate, GameState newgameState);
        public event OnGameStateChangeDelegate OnGameStateChange;

        public delegate void OnPrepareNewGameDelegate(bool isRematch = false);
        public event OnPrepareNewGameDelegate OnPrepareNewGame;

        public delegate void OnReadyToPlayDelegate();
        public event OnReadyToPlayDelegate OnReadyToPlay;

        public delegate void OnStartGameDelegate();
        public event OnStartGameDelegate OnStartGame;

        public delegate void OnGamePauseDelegate();
        public event OnGamePauseDelegate OnGamePause;

        public delegate void OnGameResumeDelegate();
        public event OnGameResumeDelegate OnGameResume;

        public delegate void OnFinishGameDelegate(bool didWin = true);
        public event OnFinishGameDelegate OnFinishGame;

        public delegate void OnExitGameDelegate();
        public event OnExitGameDelegate OnExitGame;

        //___________________________________________________________________________________________________


        private void Awake()
        {
            //Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        //___________________________________________________________________________________________________

        public GameState GetState()
        {
            return gameState;
        }

        //___________________________________________________________________________________________________

        public void SetState(GameState gameStateNext)
        {
            Debug.Log("<color='blue'>GameEngine.SetState</color>: " + gameStateNext.ToString());

            GameState gameStateOld = GetState();
            GameState gameStateCurrent = gameStateNext;

            gameState = gameStateCurrent;

            OnGameStateChange?.Invoke(gameStateOld, gameStateCurrent);
        }

        //___________________________________________________________________________________________________

        public void PrepareNewGame(bool didLose = false)
        {
            Debug.Log("<color='cyan'>GameEngine.PrepareNewGame</color>");

            if (GetState() != GameState.READY_TO_RUN)
            {
                SetState(GameState.PREPARING_NEW_GAME);
                OnPrepareNewGame?.Invoke(didLose);

                SetState(GameState.READY_TO_RUN);
                OnReadyToPlay?.Invoke();
            }
        }

        //___________________________________________________________________________________________________


        public void StartGame()
        {
            Debug.Log("<color='cyan'>GameEngine.StartGame</color>");

            if (GetState() == GameState.READY_TO_RUN)
            {
                SetState(GameState.STARTING_TO_RUN);
                OnStartGame?.Invoke();

                SetState(GameState.PLAYING);
            }
        }

        //___________________________________________________________________________________________________

        public void StartGameInstantly(bool didLose = false)
        {
            Debug.Log("<color='cyan'>GameEngine.StartGameInstantly</color>");

            PrepareNewGame(didLose);

            StartGame();
        }

        //___________________________________________________________________________________________________

        public void GamePause()
        {
            Debug.Log("<color='cyan'>GameEngine.GamePause</color>");

            if (IsPlaying())
            {
                SetState(GameState.PAUSED);

                OnGamePause?.Invoke();
            }
        }

        //___________________________________________________________________________________________________

        public void GameResume()
        {

            Debug.Log("<color='cyan'>GameEngine.ResumeGame</color>");

            if (GetState() == GameState.PAUSED)
            {
                SetState(GameState.RESUMING);
                OnGameResume?.Invoke();

                SetState(GameState.PLAYING);
            }

        }

        //___________________________________________________________________________________________________

          public void FinishGame(bool didWin = true)
        {
            Debug.Log("<color='cyan'>GameEngine.FinishGame</color>");

            if (GetState() != GameState.FINISHED)
            {
                SetState(GameState.FINISHED);
                OnFinishGame?.Invoke(didWin);
            }
        }

        //___________________________________________________________________________________________________

        public void ExitGame(bool bPrepareNewGame = true)
        {
            Debug.Log("<color='cyan'>GameEngine.ExitGame</color>");

            SetState(GameState.NONE);
            OnExitGame?.Invoke();

            if (bPrepareNewGame)
            {
                PrepareNewGame();
            }
        }

        //___________________________________________________________________________________________________

        public bool IsPlaying()
        {
            GameState gameStateCurrent = GetState();

            if (gameStateCurrent == GameState.PLAYING)
            {
                return true;
            }

            return false;
        }

    }
}

