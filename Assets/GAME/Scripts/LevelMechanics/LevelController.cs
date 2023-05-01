using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YU.Template
{
    public class LevelController : MonoBehaviour
    {
        //
        // Level
        //
        public delegate void OnLevelFailedDelegate();
        public event OnLevelFailedDelegate OnLevelFailed;

        public delegate void OnLevelCompletedDelegate();
        public event OnLevelCompletedDelegate OnLevelCompleted;

        public delegate void OnLevelDrawDelegate();
        public event OnLevelDrawDelegate OnLevelDraw;

        public delegate void OnStageCompletedDelegate(int nStage);
        public event OnStageCompletedDelegate OnStageCompleted;

        public delegate void OnStageChangedDelegate(int nStage);
        public event OnStageChangedDelegate OnStageChanged;

        public delegate void OnLevelChangedDelegate(int nLevel);
        public event OnLevelChangedDelegate OnLevelChanged;

        //
        // Generic
        //
        public delegate void OnLevelProgressValueChangedDelegate(float fCur, float fMax);
        public event OnLevelProgressValueChangedDelegate OnLevelProgressValueChanged;

        public delegate void OnScoreValueChangedDelegate();
        public event OnScoreValueChangedDelegate OnScoreValueUpdated;

        //
        // Events
        //

        
        public delegate void OnPlaneTakingOffDelegate();
        public event OnPlaneTakingOffDelegate OnPlaneTakingOff;

        public delegate void OnStartLandingDelegate();
        public event OnStartLandingDelegate OnStartLanding;

        public delegate void OnPlaneGroundedDelegate();
        public event OnPlaneGroundedDelegate OnPlaneGrounded;


        public delegate void OnRevertCrosshairVisibilityDelegate();
        public event OnRevertCrosshairVisibilityDelegate OnRevertCrosshairVisibility;

        public delegate void OnChangeCrosshairMaterialDelegate(int materialIndex);
        public event OnChangeCrosshairMaterialDelegate OnChangeCrosshairMaterial;


        public delegate void OnDropBombsDelegate(GameObject enemy, bool bIsObjectEnemy);
        public event OnDropBombsDelegate OnDropBombs;

        public delegate void OnBombDroppedDelegate();
        public event OnBombDroppedDelegate OnBombDropped;

        public delegate void OnBombAmountChangedDelegate(int currentAmount, int maxAmount);
        public event OnBombAmountChangedDelegate OnBombAmountChanged;



        public delegate void OnDestroyedEnemyDelegate();
        public event OnDestroyedEnemyDelegate OnDestroyedEnemy;

        public delegate void OnCollectedMoneyDelegate();
        public event OnCollectedMoneyDelegate OnCollectedMoney;

        public delegate void OnMoneyChangedDelegate(int currentMoney);
        public event OnMoneyChangedDelegate OnMoneyChanged;



        public delegate void OnBombCapacityUpgradedDelegate(int newCapacity);
        public event OnBombCapacityUpgradedDelegate OnBombCapacityUpgraded;

        public delegate void OnArmorUpgradedDelegate(float newArmor);
        public event OnArmorUpgradedDelegate OnArmorUpgraded;

        public delegate void OnDamageUpgradedDelegate(float newDamage);
        public event OnDamageUpgradedDelegate OnDamageUpgraded;



        public delegate void OnEnemyAttackDelegate(float damage);
        public event OnEnemyAttackDelegate OnEnemyAttack;

        public delegate void OnHealthChangedDelegate(float currentHealth, float maxHealth);
        public event OnHealthChangedDelegate OnHealthChanged;

        public delegate void   OnEnableNextLevelDelegate ();
        public event OnEnableNextLevelDelegate OnEnableNextLevel;
       

        private bool isLevelEnded;

        //___________________________________________________________________________________________________

        private void OnEnable()
        {
            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;
        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;
        }

        //___________________________________________________________________________________________________

        private void OnPrepareNewGame(bool bIsRematch = false)
        {

            isLevelEnded = false;
        }

        //___________________________________________________________________________________________________

        public void FailLevel()
        {
            if (!isLevelEnded)
            {
                isLevelEnded = true;
                Debug.Log("<color='purple'>LevelEventsManager OnLevelFailed</color>");
                OnLevelFailed?.Invoke();
            }
        }

        //___________________________________________________________________________________________________

        public void CompleteLevel()
        {
            if (!isLevelEnded)
            {
                isLevelEnded = true;
                Debug.Log("<color='purple'>LevelEventsManager OnLevelCompleted</color>");
                OnLevelCompleted?.Invoke();
            }
        }

        //___________________________________________________________________________________________________


        public void DrawLevel()
        {
            if (!isLevelEnded)
            {
                isLevelEnded = true;
                Debug.Log("<color='purple'>LevelEventsManager OnLevelDraw</color>");
                OnLevelDraw?.Invoke();
            }
        }

        //___________________________________________________________________________________________________


        public void CompleteStage(int nStage)
        {
            Debug.Log("<color='purple'>LevelEventsManager OnStageCompleted: </color>" + nStage);

            OnStageCompleted?.Invoke(nStage);
        }

        //___________________________________________________________________________________________________


        public void ChangeStage(int nStage)
        {
            Debug.Log("<color='purple'>LevelEventsManager OnStageChanged: </color>" + nStage);

            OnStageChanged?.Invoke(nStage);
        }

        //___________________________________________________________________________________________________


        public void ChangeLevel(int nLevel)
        {
            Debug.Log("<color='purple'>LevelEventsManager OnLevelChanged: </color>" + nLevel);

            OnLevelChanged?.Invoke(nLevel);
        }

        //___________________________________________________________________________________________________


        public void ChangeLevelProgressValue(float fCur, float fMax)
        {
            //Debug.Log("<color='purple'>LevelProgressValueChanged</color> " + fMin + " " + fMax + " " + fVal);

            OnLevelProgressValueChanged?.Invoke(fCur, fMax);
        }

        //___________________________________________________________________________________________________


        public void UpdateScoreValue()
        {
            //Debug.Log("<color='purple'>ScoreValueChanged</color>");

            OnScoreValueUpdated?.Invoke();
        }

         //___________________________________________________________________________________________________

        public void PlaneTakingOff()
        {
            OnPlaneTakingOff?.Invoke();
        }

        //___________________________________________________________________________________________________

        public void StartLanding()
        {
            OnStartLanding?.Invoke();
        }

        //___________________________________________________________________________________________________

        public void RevertCrosshairVisibility()
        {
            OnRevertCrosshairVisibility?.Invoke();
        }

        //___________________________________________________________________________________________________

        public void ChangeCrosshairMaterial(int material)
        {
            OnChangeCrosshairMaterial?.Invoke(material);
        }

        //___________________________________________________________________________________________________

        public void DropBombs(GameObject enemy,bool bIsObjectEnemy)
        {
            OnDropBombs?.Invoke(enemy, bIsObjectEnemy);
        }

        //___________________________________________________________________________________________________

        public void BombDropped()
        {
            OnBombDropped?.Invoke();
        }
        //___________________________________________________________________________________________________

        public void CollectedMoney()
        {
            OnCollectedMoney?.Invoke();
        }

        public void MoneyChanged(int currentMoney)
        {
            OnMoneyChanged?.Invoke(currentMoney);
        }

        //___________________________________________________________________________________________________

        public void DestroyedEnemy()
        {
            OnDestroyedEnemy?.Invoke();
        }

        //___________________________________________________________________________________________________

        public void PlaneGrounded()
        {
            OnPlaneGrounded?.Invoke();
        }

        //___________________________________________________________________________________________________

        public void BombAmountChanged(int currentAmount, int maxAmount)
        {
            OnBombAmountChanged?.Invoke(currentAmount, maxAmount);
        }

        //___________________________________________________________________________________________________

        public void BombCapacityUpgraded(int newCapacity)
        {
            OnBombCapacityUpgraded?.Invoke(newCapacity);
        }

        //___________________________________________________________________________________________________

        public void ArmorUpgraded(float newArmor)
        {
            OnArmorUpgraded?.Invoke(newArmor);
        }

        //___________________________________________________________________________________________________

        public void DamageUpgraded(float newDamage)
        {
            OnDamageUpgraded?.Invoke(newDamage);
        }

        //___________________________________________________________________________________________________

        public void EnemyAttack(float damage)
        {
            OnEnemyAttack?.Invoke(damage);
        }

         //___________________________________________________________________________________________________

        public void HealthChanged(float currentHealth, float maxHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

         //___________________________________________________________________________________________________
        
        public void EnableNextLevel()
        {
            OnEnableNextLevel?.Invoke();
        }
    }
}




