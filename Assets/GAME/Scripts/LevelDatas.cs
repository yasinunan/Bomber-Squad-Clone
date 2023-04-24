using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YU.Template
{

    public class LevelDatas : MonoBehaviour
    {
       //___________________________________________________________________________________________________


        public int levelNumber = 0;
        public bool isLevelFinished = false;
        public int nScore = 0;

        
        public int nCoins;

        //___________________________________________________________________________________________________


        void Awake()
        {
            ResetValues(true);
        }

        //___________________________________________________________________________________________________


        void OnEnable()
        {
            //GameEngine.Instance.OnGameInitialize += OnGameInitialize;
            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;
 
            GameEngine.Instance.OnFinishGame += OnFinishGame;
            //GameEngine.Instance.OnExitGame += OnExitGame;

            //LevelManager.Instance.controller.OnLevelFailed += OnLevelFailed;
            //LevelManager.Instance.controller.OnLevelCompleted += OnLevelCompleted;
            //LevelManager.Instance.controller.OnLevelDraw += OnLevelDraw;
            //LevelManager.Instance.controller.OnStageCompleted += OnStageCompleted;
            //LevelManager.Instance.controller.OnStageChanged += OnStageChanged;
            //LevelManager.Instance.controller.OnLevelChanged += OnLevelChanged;
            //LevelManager.Instance.controller.OnLevelProgressValueChanged += OnLevelProgressValueChanged;
            //LevelManager.Instance.controller.OnScoreValueUpdated += OnScoreValueChanged;
        }

        //___________________________________________________________________________________________________


        void OnDisable()
        {
            //GameEngine.Instance.OnGameInitialize -= OnGameInitialize;
            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;

            //GameEngine.Instance.OnPauseGame -= OnPauseGame;
            //GameEngine.Instance.OnResumeGame -= OnResumeGame;
            //GameEngine.Instance.OnReviveGame -= OnReviveGame;
            //GameEngine.Instance.OnFinishGameWaitUserAction -= OnFinishGameWaitUserAction;
            GameEngine.Instance.OnFinishGame -= OnFinishGame;
            //GameEngine.Instance.OnExitGame -= OnExitGame;

            //LevelManager.Instance.controller.OnLevelFailed -= OnLevelFailed;
            //LevelManager.Instance.controller.OnLevelCompleted -= OnLevelCompleted;
            //LevelManager.Instance.controller.OnLevelDraw -= OnLevelDraw;
            //LevelManager.Instance.controller.OnStageCompleted -= OnStageCompleted;
            //LevelManager.Instance.controller.OnStageChanged -= OnStageChanged;
            //LevelManager.Instance.controller.OnLevelChanged -= OnLevelChanged;
            //LevelManager.Instance.controller.OnLevelProgressValueChanged -= OnLevelProgressValueChanged;
            //LevelManager.Instance.controller.OnScoreValueUpdated -= OnScoreValueChanged;
        }

        //___________________________________________________________________________________________________


        //___________________________________________________________________________________________________


        void ResetValues(bool bIncreaseLevel = false)
        {
            nScore = 0;


            isLevelFinished = false;

           
            if (bIncreaseLevel)
            {
                int nMaxFinishedLevel = PlayerStats.Instance.GetMaxFinishedLevel();
                SetCurrentLevel(++nMaxFinishedLevel);
            }
        }

        //___________________________________________________________________________________________________


        public void SetCurrentLevel(int nVal)
        {
            levelNumber = nVal;
        }

        //___________________________________________________________________________________________________


        public int GetCurrentLevel()
        {
            return levelNumber;
        }


        //___________________________________________________________________________________________________

        //
        // UPDATE PLAYER STATS
        //
        //___________________________________________________________________________________________________


        public void UpdatePlayerStats()
        {
            Debug.Log("GameInstanceData: UpdatePlayerStats");

            int nLastFinishedLevel = levelNumber;

            // register last finished level
            PlayerStats.Instance.SetLastFinishedLevel(nLastFinishedLevel);

            // register if max level
            PlayerStats.Instance.SetMaxFinishedLevel(nLastFinishedLevel);

            SetCurrentLevel(nLastFinishedLevel + 1);

          
        }

        //___________________________________________________________________________________________________

        //
        // CURRENTs INSTANCE EARNINGS
        //
        //___________________________________________________________________________________________________


        public void SaveEarningsOfCurrentInstance()
        {
            Debug.Log("GameInstanceData: SaveEarningsOfCurrentInstance");

            // save coins
            InventoryManager.Instance.IncreaseCoinsCount(nCoins);

        }


        //___________________________________________________________________________________________________

        //
        // SCORE
        //
        //___________________________________________________________________________________________________


        public int GetScore()
        {
            return nScore;
        }

        //___________________________________________________________________________________________________


        public void SetScore(int nValue)
        {
            nScore = nValue;
        }

        //___________________________________________________________________________________________________


        public void IncreaseScore(int nIncOrDec = 1)
        {
            nScore += nIncOrDec;
        }

        //___________________________________________________________________________________________________

        //
        // EVENTS TO IMPLEMENT
        //
        //___________________________________________________________________________________________________


        void OnPrepareNewGame(bool bIsRematch = false)
        {
            ResetValues(!bIsRematch);
        }

        //___________________________________________________________________________________________________



        //___________________________________________________________________________________________________


        void OnFinishGame(bool bWin = true)
        {
            if (bWin)
            {
                Debug.Log("UpdatePlayerStats level:" + GetCurrentLevel());
                UpdatePlayerStats();
            }

           
        }

        //___________________________________________________________________________________________________






    }
    
}
