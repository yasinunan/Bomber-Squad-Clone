
using System.Collections;
using UnityEngine;
using Pixelplacement;
using Random = UnityEngine.Random;

namespace YU.Template
{

    [DefaultExecutionOrder(-1)]
    public class LevelManager : Singleton<LevelManager>
    {
        //___________________________________________________________________________________________________

        public LevelController controller;
        public LevelDatas datas;


        [Header("Level Object Holders")]
        public Transform levelEnvironmentHolder;
        public Transform levelDestructiblesHolder;


        [Space]

        private int level;

        //___________________________________________________________________________________________________

        private void Awake()
        {
            controller = gameObject.AddComponent<LevelController>();
            datas = gameObject.AddComponent<LevelDatas>();
        }

        //___________________________________________________________________________________________________

        private void OnEnable()
        {
            GameEngine.Instance.OnGameInitialize += OnGameInitialize;
            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;
            GameEngine.Instance.OnStartGame += OnStartGame;
            GameEngine.Instance.OnGamePause += OnGamePause;
            GameEngine.Instance.OnGameResume += OnGameResume;
            GameEngine.Instance.OnFinishGame += OnFinishGame;
            GameEngine.Instance.OnExitGame += OnExitGame;

            controller.OnLevelFailed += OnLevelFailed;
            controller.OnLevelCompleted += OnLevelCompleted;
            controller.OnLevelDraw += OnLevelDraw;
            controller.OnLevelChanged += OnLevelChanged;

        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            GameEngine.Instance.OnGameInitialize -= OnGameInitialize;
            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;
            GameEngine.Instance.OnGamePause -= OnGamePause;
            GameEngine.Instance.OnGameResume -= OnGameResume;
            GameEngine.Instance.OnFinishGame -= OnFinishGame;
            GameEngine.Instance.OnExitGame -= OnExitGame;

            controller.OnLevelFailed -= OnLevelFailed;
            controller.OnLevelCompleted -= OnLevelCompleted;
            controller.OnLevelDraw -= OnLevelDraw;
            controller.OnLevelChanged -= OnLevelChanged;

        }

        //___________________________________________________________________________________________________

        void Start()
        {
            GameEngine.Instance.SetState(GameEngine.GameState.INITIALIZED);
            GameEngine.Instance.StartGameInstantly();
        }

        //___________________________________________________________________________________________________

        private void OnExitGame()
        {
        }

        //___________________________________________________________________________________________________


        private void OnFinishGame(bool didWin = true)
        {
            controller.UpdateScoreValue();

            //int nCoins = datas.GetCoins();


        }


        //___________________________________________________________________________________________________


        private void OnGameResume()
        {
        }

        //___________________________________________________________________________________________________


        private void OnGamePause()
        {
        }

        //___________________________________________________________________________________________________


        private void OnStartGame()
        {

        }

        //___________________________________________________________________________________________________


        private void OnPrepareNewGame(bool bIsRematch = false)
        {
            level = LevelManager.Instance.datas.GetCurrentLevel();
            PrepareLevel();
        }

        //___________________________________________________________________________________________________


        public void OnGameInitialize()
        {
        }

        //___________________________________________________________________________________________________



        //___________________________________________________________________________________________________

        //
        // Event Handlers - LevelEventsManager
        //

        private void OnLevelFailed()
        {
            datas.isLevelFinished = true;

            // datas.SetMoney(0);
            controller.UpdateScoreValue();


            GameEngine.Instance.FinishGame(false);

        }

        //___________________________________________________________________________________________________


        private void OnLevelCompleted()
        {
            datas.isLevelFinished = true;

            controller.UpdateScoreValue();


            datas.SaveEarningsOfCurrentInstance();


            GameEngine.Instance.FinishGame();

        }

        //___________________________________________________________________________________________________


        private void OnLevelDraw()
        {
            GameEngine.Instance.FinishGame();
        }

        //___________________________________________________________________________________________________


        //___________________________________________________________________________________________________


        public void OnLevelChanged(int nLevel)
        {
        }


        //___________________________________________________________________________________________________



        //___________________________________________________________________________________________________




        //___________________________________________________________________________________________________

        //___________________________________________________________________________________________________
        //
        // LEVEL MECHANICS
        //

        void PrepareLevel()
        {
            Debug.Log(Time.time + " <color='white'>Current Level</color>: " + datas.levelNumber);

            ClearPreviousLevel();

            CreateLevelObjects();
        }

        //___________________________________________________________________________________________________

        void ClearPreviousLevel()
        {


            ClearPreviousLevelObjects();
        }

        //___________________________________________________________________________________________________

        void ClearPreviousLevelObjects()
        {
             PoolingManager.Instance.ReleaseAllPooledObjects();

            // clear prev. level environment
            if (levelEnvironmentHolder != null)
            {
                Transform[] atrCurLevelEnvObjects = levelEnvironmentHolder.GetComponentsInChildren<Transform>(true);
                foreach (Transform trChild in atrCurLevelEnvObjects)
                {
                    if (trChild.parent == levelEnvironmentHolder)
                    {
                        Destroy(trChild.gameObject);
                    }
                }
            }

            // clear prev. level objects            
            if (levelDestructiblesHolder != null)
            {
                Transform[] atrLevels = levelDestructiblesHolder.GetComponentsInChildren<Transform>(true);
                foreach (Transform trLevel in atrLevels)
                {
                    if (trLevel.parent == levelDestructiblesHolder)
                    {
                        Destroy(trLevel.gameObject);
                    }
                }
            }

        }

        //___________________________________________________________________________________________________

        void CreateLevelObjects()
        {

            LevelSpawner.Instance.SpawnNewLevel();
        }
    }
}

