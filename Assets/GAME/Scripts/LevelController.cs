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
        public delegate void OnLevelProgressValueChangedDelegate(float fMin, float fMax, float fVal);
        public event OnLevelProgressValueChangedDelegate OnLevelProgressValueChanged;

        public delegate void OnScoreValueChangedDelegate();
        public event OnScoreValueChangedDelegate OnScoreValueUpdated;

        //
        // Events
        //

        public delegate void OnStartLandingDelegate();
        public event OnStartLandingDelegate OnStartLanding;

        public delegate void OnRevertCrosshairVisibilityDelegate();
        public event OnRevertCrosshairVisibilityDelegate OnRevertCrosshairVisibility;

        public delegate void OnChangeCrosshairMaterialDelegate(int material);
        public event OnChangeCrosshairMaterialDelegate OnChangeCrosshairMaterial;

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


        public void ChangeLevelProgressValue(float fMin, float fMax, float fVal)
        {
            //Debug.Log("<color='purple'>LevelProgressValueChanged</color> " + fMin + " " + fMax + " " + fVal);

            OnLevelProgressValueChanged?.Invoke(fMin, fMax, fVal);
        }

        //___________________________________________________________________________________________________


        public void UpdateScoreValue()
        {
            //Debug.Log("<color='purple'>ScoreValueChanged</color>");

            OnScoreValueUpdated?.Invoke();
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

    }
}




