using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pixelplacement;

namespace YU.Template
{
    public class CameraController : Singleton<CameraController>
    {
        //===================================================================================

        public GameObject goPlayer;

        Vector3 v3StartPos;
        Vector3 v3StartEulerAngles;
        Vector3 v3StartOffset;

        [SerializeField] private bool bFollowingPlayer = true;
        [SerializeField] float fSmoothTime = 0.15f;
        [SerializeField] ParticleSystem psConfettiLeft;
        [SerializeField] ParticleSystem psConfettiMiddle;
        [SerializeField] ParticleSystem psConfettiRight;



        Vector3 v3Velocity = Vector3.zero;

        public Camera camera;


        private Tweener camShakeTweener;

        //===================================================================================

        void Awake()
        {
            camera = Camera.main;

            goPlayer = GameObject.Find("Player");
            /*
            if (goPlayer == null)
            {
                Debug.LogAssertion("There is no player character in the scene!");
            }
            */

            v3StartPos = transform.position;
            v3StartEulerAngles = transform.localEulerAngles;
            if (goPlayer != null) v3StartOffset = transform.position - goPlayer.transform.position;
        }

        //===================================================================================

        void OnEnable()
        {
            /*
           
            GameMotor.Instance.OnReviveGame += OnReviveGame;
            //GameMotor.Instance.OnExitGame += OnExitGame;

            ResetCameraPosition();
            */
            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;
        }

        //===================================================================================

        void OnDisable()
        {
            /*
           
            GameMotor.Instance.OnExitGame -= OnExitGame;

            */
            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;
        }

        //===================================================================================

        void LateUpdate()
        {
            if (goPlayer == null || !IsFollowingPlayer())
            {
                return;
            }

            Vector3 v3TargetPosition = goPlayer.transform.position + v3StartOffset;
            Vector3 v3NewPos = Vector3.SmoothDamp(transform.position, v3TargetPosition, ref v3Velocity, fSmoothTime);
            v3NewPos = new Vector3(v3NewPos.x, v3NewPos.y, v3NewPos.z);
            transform.position = v3NewPos;
        }

        //===================================================================================

        void SetFollowingPlayer(bool bVal = true)
        {
            bFollowingPlayer = bVal;
        }

        //===================================================================================

        bool IsFollowingPlayer()
        {
            return bFollowingPlayer;
        }

        //===================================================================================

        public void ShakeCamera(float fDelay, float fDuration, float fStrength = 3f, int nVibrato = 10, float fRandomness = 90f, bool bFadeOut = true)
        {
            StartCoroutine(Shake(fDelay, fDuration, fStrength, nVibrato, fRandomness, bFadeOut));
        }

        //===================================================================================

        public IEnumerator Shake(float fDelay, float fDuration, float fStrength = 3f, int nVibrato = 10, float fRandomness = 90f, bool bFadeOut = true)
        {
            if (fDelay > 0)
            {
                yield return new WaitForSeconds(fDelay);
            }
            else
            {
                yield return null;
            }

            camShakeTweener = Camera.main.DOShakePosition(fDuration, fStrength, nVibrato, fRandomness, bFadeOut);
        }

        //===================================================================================

        void ResetCameraPosition()
        {
            StopAllCoroutines();

            if (camShakeTweener != null)
                camShakeTweener.Complete();
            transform.position = v3StartPos;
        }

        //===================================================================================

        void ResetCameraRotation()
        {
            transform.localEulerAngles = v3StartEulerAngles;
        }

        //===================================================================================

        void OnPrepareNewGame(bool isLevelPassed)
        {
            ResetCameraPosition();
            ResetCameraRotation();
            //  SetFollowingPlayer();

        }

        //===================================================================================
        //
        // EVENTS TO IMPLEMENT
        //
        IEnumerator OnPrepareNewGame()
        {
            ResetCameraPosition();
            ResetCameraRotation();
            SetFollowingPlayer();

            yield return null;
        }

        //===================================================================================

        IEnumerator OnReviveGame()
        {
            ResetCameraPosition();
            ResetCameraRotation();
            SetFollowingPlayer();

            yield return null;
        }

        //===================================================================================

        IEnumerator OnExitGame()
        {
            ResetCameraPosition();
            ResetCameraRotation();
            SetFollowingPlayer();

            yield return null;
        }

        //===================================================================================
        /*
        bool OnExitGame()
        {
            return true;
        }
        */
        //===================================================================================


        //===================================================================================
        //
        // Player
        //
        public IEnumerator OnPlayerDied(Vector3 v3EnemyHitPos)
        {
            Debug.Log("cameracontroller player died");

            SetFollowingPlayer(false);

            ShakeCamera(0, 0.25f, 0.75f, 30);

            yield return null;
        }

        //===================================================================================

        public IEnumerator OnPlayerPassedLevel()
        {
            Debug.Log("cameracontroller player passed level");

            SetFollowingPlayer(false);

            Pixelplacement.Tween.LocalPosition(transform, new Vector3(0, transform.localPosition.y + 20.5f, 5.1f), 2f, 0, Pixelplacement.Tween.EaseInOutStrong);

            Pixelplacement.Tween.LocalRotation(transform, new Vector3(9.45f, -180f, 0), 2f, 0, Pixelplacement.Tween.EaseInOutStrong);

            yield return new WaitForSeconds(2f);

            yield return null;
        }

        //===================================================================================



    }
}
