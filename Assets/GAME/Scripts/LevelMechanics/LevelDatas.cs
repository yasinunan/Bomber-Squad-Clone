using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YU.Template
{

    public class LevelDatas : MonoBehaviour
    {
        //___________________________________________________________________________________________________

        [SerializeField] private LevelDataSO LevelData;
        public List<GameObject> EnemyList = new List<GameObject>();
        public int levelNumber = 0;
        public bool isLevelFinished = false;
        [SerializeField] private int nMoney = 0;
        [SerializeField] private int moneyIncreaseAmount = 4;


        private int bombAmount = 30;
        [SerializeField] private int defaultBombAmount;
        [SerializeField] private int currentBombAmount = 30;

        [SerializeField] private int destroyedEnemy;
        private float maxHealth;
        private float currentHealth;
        private float healAmount;
        private float armor;
        private float damage;

        private bool isHealing = false;

        private Coroutine HealCoroutine;

        //___________________________________________________________________________________________________


        void Start()
        {
            ResetValues(true);

            // InventoryManager.Instance.SetBombsCount(30);
          //  FindEnemies();
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

            LevelManager.Instance.controller.OnBombDropped += OnBombDropped;
            LevelManager.Instance.controller.OnPlaneGrounded += OnPlaneGrounded;
            LevelManager.Instance.controller.OnPlaneTakingOff += OnPlaneTakingOff;
            LevelManager.Instance.controller.OnBombCapacityUpgraded += OnBombCapacityUpgraded;
            LevelManager.Instance.controller.OnArmorUpgraded += OnArmorUpgraded;
            LevelManager.Instance.controller.OnEnemyAttack += OnEnemyAttack;
            LevelManager.Instance.controller.OnDestroyedEnemy += OnDestroyedEnemy;
            LevelManager.Instance.controller.OnCollectedMoney += OnCollectedMoney;

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

            LevelManager.Instance.controller.OnBombDropped -= OnBombDropped;
            LevelManager.Instance.controller.OnPlaneGrounded -= OnPlaneGrounded;
            LevelManager.Instance.controller.OnPlaneTakingOff -= OnPlaneTakingOff;
            LevelManager.Instance.controller.OnBombCapacityUpgraded -= OnBombCapacityUpgraded;
            LevelManager.Instance.controller.OnArmorUpgraded -= OnArmorUpgraded;
            LevelManager.Instance.controller.OnEnemyAttack -= OnEnemyAttack;
            LevelManager.Instance.controller.OnDestroyedEnemy -= OnDestroyedEnemy;
            LevelManager.Instance.controller.OnCollectedMoney -= OnCollectedMoney;

        }

        //___________________________________________________________________________________________________

        void ResetValues(bool bIncreaseLevel = false)
        {
            bombAmount = InventoryManager.Instance.GetBombsCount();
            if (bombAmount < 1)
            {
                bombAmount = defaultBombAmount; ;
                InventoryManager.Instance.SetBombsCount(bombAmount);
            }
            currentBombAmount = bombAmount;
            LevelManager.Instance.controller.BombAmountChanged(currentBombAmount, bombAmount);

            maxHealth = LevelData.maxHealth;
            currentHealth = maxHealth;
            LevelManager.Instance.controller.HealthChanged(currentHealth, maxHealth);

            moneyIncreaseAmount = LevelData.moneyIncreaseAmount;

            isLevelFinished = false;
            destroyedEnemy = 0;


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

        private IEnumerator HealOnGround()
        {
            while (isHealing)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += 10f;
                    LevelManager.Instance.controller.HealthChanged(currentHealth, maxHealth);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    currentHealth = maxHealth;
                    LevelManager.Instance.controller.HealthChanged(currentHealth, maxHealth);
                    isHealing = false;
                    yield return null;
                }
            }

            yield return null;
        }

        //___________________________________________________________________________________________________

        private IEnumerator FindEnemies()
        {
            yield return new WaitForEndOfFrame();
            if (EnemyList.Count > 0)
            {
                EnemyList.Clear();
                EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            }
            else
            {
                EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            }
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

            // save money
            InventoryManager.Instance.IncreaseCoinsCount(nMoney);

        }


        //___________________________________________________________________________________________________

        //
        // MONEY
        //
        //___________________________________________________________________________________________________


        public int GetMoney()
        {
            return PlayerPrefs.GetInt("Money");
        }

        //___________________________________________________________________________________________________

        public void SetMoney(int nValue)
        {
            nMoney = nValue;
            PlayerPrefs.SetInt("Money", nMoney);
            LevelManager.Instance.controller.MoneyChanged(nMoney);
        }

        //___________________________________________________________________________________________________

        public void IncreaseMoney(int nIncOrDec = 1)
        {
            nMoney += nIncOrDec;
            SetMoney(nMoney);
        }



        //
        // BOMB
        //
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________

        public bool HasEnoughBombs()
        {
            if (currentBombAmount > 0)
            { return true; }
            else
            { return false; }
        }




        //
        // EVENTS
        //
        //___________________________________________________________________________________________________


        void OnPrepareNewGame(bool bIsRematch = false)
        {
            ResetValues(!bIsRematch);
            StartCoroutine(FindEnemies());
        }

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

        void OnBombDropped()
        {
            currentBombAmount--;
            LevelManager.Instance.controller.BombAmountChanged(currentBombAmount, bombAmount);
        }

        //___________________________________________________________________________________________________

        void OnPlaneGrounded()
        {
            currentBombAmount = bombAmount;
            LevelManager.Instance.controller.BombAmountChanged(currentBombAmount, bombAmount);

            isHealing = true;
            if (HealCoroutine != null)
            {
                StopCoroutine(HealCoroutine);
                HealCoroutine = StartCoroutine(HealOnGround());
            }
            else
            { HealCoroutine = StartCoroutine(HealOnGround()); }
        }

        //___________________________________________________________________________________________________

        private void OnPlaneTakingOff()
        {
            isHealing = false;
        }

        //___________________________________________________________________________________________________

        void OnBombCapacityUpgraded(int newCapacity)
        {
            bombAmount = newCapacity;
            InventoryManager.Instance.SetBombsCount(bombAmount);
            currentBombAmount = InventoryManager.Instance.GetBombsCount();

            LevelManager.Instance.controller.BombAmountChanged(currentBombAmount, bombAmount);
        }

        //___________________________________________________________________________________________________

        void OnEnemyAttack(float _damage)
        {
            float calculatedDamage = _damage - (_damage * armor);
            if (currentHealth - calculatedDamage <= 0f)
            {
                currentHealth = 0f;
                LevelManager.Instance.controller.HealthChanged(currentHealth, maxHealth);
                LevelManager.Instance.controller.FailLevel();
            }
            else
            {
                currentHealth -= calculatedDamage;
                LevelManager.Instance.controller.HealthChanged(currentHealth, maxHealth);
            }
        }

        //___________________________________________________________________________________________________


        //___________________________________________________________________________________________________

        void OnArmorUpgraded(float damage)
        {
            armor = InventoryManager.Instance.GetArmor();
        }

        //___________________________________________________________________________________________________

        private void OnCollectedMoney()
        {
            IncreaseMoney(moneyIncreaseAmount);



        }

        private void OnDestroyedEnemy()
        {
            destroyedEnemy++;
            LevelManager.Instance.controller.ChangeLevelProgressValue(destroyedEnemy, EnemyList.Count);

            if (destroyedEnemy == EnemyList.Count)
            {
                LevelManager.Instance.controller.EnableNextLevel();
            }
        }

    }

}
