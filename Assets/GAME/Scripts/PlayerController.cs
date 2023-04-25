using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private Animator planeAnimator;
        [SerializeField] private VariableJoystick variableJoystick;

        [SerializeField] private float flySpeed;
        [SerializeField] private float rotationSpeed = 720f;
        [SerializeField] private float takeOffDuration = 0.5f;

        [SerializeField] private bool canFly = false;
        [SerializeField] private bool isFlying = false;
        [SerializeField] private bool isLanding = false;



        private Vector3 moveDir;

        //___________________________________________________________________________________________________

        private void OnEnable()
        {
            LevelManager.Instance.controller.OnStartLanding += OnStartLanding;
        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            LevelManager.Instance.controller.OnStartLanding -= OnStartLanding;
        }

        //___________________________________________________________________________________________________

        void Start()
        {

        }

        //___________________________________________________________________________________________________

        private void FixedUpdate()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, forward, 2f) && !isLanding)
            {
                Debug.Log("hit detected");
                isLanding = true;
                isFlying = false;
                canFly = false;

               
                LevelManager.Instance.controller.StartLanding();
            }
        }

        //___________________________________________________________________________________________________


        void Update()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                return;
            }

            float horizontal = variableJoystick.Horizontal;
            float vertical = variableJoystick.Vertical;

            Vector3 moveDirection = Vector3.right * horizontal + Vector3.forward * vertical;

            if (moveDirection != Vector3.zero && !isFlying && !isLanding)
            {
                isFlying = true;
                TakeOff();

            }

            if (canFly)
            {
                PlaneMovement(moveDirection);
            }

        }

        //___________________________________________________________________________________________________

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 2f;
            Gizmos.DrawRay(transform.position, forward);
        }

        //___________________________________________________________________________________________________


        private void PlaneMovement(Vector3 moveDir)
        {
            transform.position += transform.forward * flySpeed * Time.deltaTime;
            if (moveDir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        //___________________________________________________________________________________________________

        private void TakeOff()
        {
            Debug.Log("PLANE IS TAKING OFF");

            transform.DORotate(new Vector3(-45f, 0f, 0f), takeOffDuration).OnComplete(() =>
            {
                transform.DOMove(new Vector3(0f, 3f, 9f), takeOffDuration * 5f).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(0f, 0f, 0f), takeOffDuration / 2f).OnComplete(() =>
                    {
                        canFly = true;
                        isLanding = false;
                        LevelManager.Instance.controller.RevertCrosshairVisibility();
                    });
                });
            });
        }

        //___________________________________________________________________________________________________

        private void LandThePlane()
        {

            transform.DOMove(new Vector3(0f, 3f, 9f), takeOffDuration * 1f).OnComplete(() =>
            {
                  
                Debug.Log("1");
                transform.DORotate(new Vector3(45f, 180f, 0f), takeOffDuration * 1f).OnComplete(() =>
                {
                    Debug.Log("2");
                    transform.DOMove(Vector3.zero, takeOffDuration * 5f).OnComplete(() =>
                    {
                        Debug.Log("3");
                        transform.DORotate(Vector3.zero, takeOffDuration * 1f).OnComplete(() =>
                        {
                            Debug.Log("4");
                            isLanding = false;
                        });

                    });
                });
            });
        }

        // EVENTS
        //___________________________________________________________________________________________________
        //

        private void OnStartLanding()
        {
           LevelManager.Instance.controller.RevertCrosshairVisibility();
            LandThePlane();
        }

        //___________________________________________________________________________________________________


    }

}
