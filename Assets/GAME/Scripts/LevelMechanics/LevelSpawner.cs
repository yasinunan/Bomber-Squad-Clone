using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;



namespace YU.Template
{
    public class LevelSpawner : Singleton<LevelSpawner>
    {

        [SerializeField] private Transform trLevelEnvironmentHolder;
        [SerializeField] private Transform trLevelDestructiblesHolder;

        [SerializeField] private List<NewLevelSO> levels;

        [SerializeField] private int CurrentLevel;

        //___________________________________________________________________________________________________

        void OnEnable()
        {
            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;

            LevelManager.Instance.controller.OnLevelFailed += OnLevelFailed;
        }
        //___________________________________________________________________________________________________

        void OnDisable()
        {
            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;

            LevelManager.Instance.controller.OnLevelFailed -= OnLevelFailed;
        }

        //___________________________________________________________________________________________________

        private void LevelGetter()
        {
            CurrentLevel = LevelManager.Instance.datas.GetCurrentLevel();
        }

        //___________________________________________________________________________________________________

        public void SpawnNewLevel()
        {
            // Spawned objects must be located under one of the placeholder transforms in LevelManager!
            CreateEnvironment();
            SpawnObstacles();
            CreateCharacters();

        }

        //___________________________________________________________________________________________________

        private void SpawnObstacles()
        {

        }

        //___________________________________________________________________________________________________

        private void CreateEnvironment()
        {

            LevelGetter();

            NewLevelSO levelData;

            // Getting the current level's chunks from scriptable object
            if (CurrentLevel > levels.Count)
            {
                levelData = levels[levels.Count - 1];
            }
            else
            {
                levelData = levels[CurrentLevel - 1];
            }

            GameObject spawnedLevel = Instantiate(levelData.levelPrefab);

            spawnedLevel.transform.SetParent(trLevelEnvironmentHolder);


        }

        //___________________________________________________________________________________________________

        private void CreateCharacters()
        {

            //GameObject spawnedObstacle = Instantiate(player);
            //  spawnedObstacle.transform.SetParent(LevelManager.Instance.trLevelEnvironmentHolder);
            //Transform createdCharacterTransform = Instantiate(new GameObject("EmptyCharacter")).transform;
            //createdCharacterTransform.SetParent(LevelManager.Instance.trLevelPlatformHolder);
        }

        //___________________________________________________________________________________________________

        public void OnPrepareNewGame(bool x)
        {

            //SpawnNewLevel();
        }

        //___________________________________________________________________________________________________

        public void OnLevelFailed()
        {

        }
    }
}
