using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace YU.Template
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject viewScreenPlay;
        [SerializeField] private GameObject viewScreenUpgrade;
        [SerializeField] private GameObject viewScreenWin;
        [SerializeField] private GameObject viewScreenFail;

        private bool bIsRematch = false;

        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnPlaneTakingOff += OnPlaneTakingOff;
            LevelManager.Instance.controller.OnPlaneGrounded += OnPlaneGrounded;
            LevelManager.Instance.controller.OnLevelFailed += OnLevelFailed;
            LevelManager.Instance.controller.OnLevelCompleted += OnLevelCompleted;

            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;

        }

        //___________________________________________________________________________________________________

        void OnDisable()
        {
            LevelManager.Instance.controller.OnPlaneTakingOff -= OnPlaneTakingOff;
            LevelManager.Instance.controller.OnPlaneGrounded -= OnPlaneGrounded;
            LevelManager.Instance.controller.OnLevelFailed -= OnLevelFailed;
            LevelManager.Instance.controller.OnLevelCompleted += OnLevelCompleted;

            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;
        }

        //___________________________________________________________________________________________________

        public void OnButtonPlayClick()
        {
            GameEngine.Instance.StartGameInstantly(bIsRematch);
        }


        //___________________________________________________________________________________________________
        //
        // EVENTS
        //___________________________________________________________________________________________________

        private void OnPrepareNewGame(bool bIsRematch)
        {
            if (!viewScreenPlay.activeInHierarchy)
            {
                viewScreenPlay.SetActive(true);
            }

            viewScreenUpgrade.SetActive(true);
            viewScreenFail.SetActive(false);
            viewScreenWin.SetActive(false);
        }

        //___________________________________________________________________________________________________

        private void OnLevelFailed()
        {
            bIsRematch = true;
            viewScreenPlay.SetActive(false);
            viewScreenUpgrade.SetActive(false);
            viewScreenFail.SetActive(true);
            viewScreenWin.SetActive(false);
        }

        //___________________________________________________________________________________________________

        private void OnLevelCompleted()
        {
            bIsRematch = false;
            viewScreenPlay.SetActive(false);
            viewScreenUpgrade.SetActive(false);
            viewScreenFail.SetActive(false);
            viewScreenWin.SetActive(true);
        }

        //___________________________________________________________________________________________________


        private void OnPlaneTakingOff()
        {
            if (!viewScreenPlay.activeInHierarchy)
            {
                viewScreenPlay.SetActive(true);
            }

            if (viewScreenUpgrade.activeInHierarchy)
            {
                viewScreenUpgrade.SetActive(false);
            }

            viewScreenFail.SetActive(false);
            viewScreenWin.SetActive(false);
        }

        //___________________________________________________________________________________________________

        private void OnPlaneGrounded()
        {

            if (!viewScreenPlay.activeInHierarchy)
            {
                viewScreenPlay.SetActive(true);
            }

            if (!viewScreenUpgrade.activeInHierarchy)
            {
                viewScreenUpgrade.SetActive(true);
            }

            viewScreenFail.SetActive(false);
            viewScreenWin.SetActive(false);
        }
    }

}
