using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class PlayerController : MonoBehaviour
    {
        private int _detectedEnemyCount;
        private int detectedEnemyCount
        {
            get { return _detectedEnemyCount; }
            set
            {
                if (_detectedEnemyCount != value)
                {
                    _detectedEnemyCount = value;

                    Debug.Log("Detected enemy count changed, bomb event triggered");
                    LevelManager.Instance.controller.DropBombs(_detectedEnemyCount);

                    if (_detectedEnemyCount > 0)
                    { LevelManager.Instance.controller.ChangeCrosshairMaterial(1); }  //calling a function from controller script to trigger an event to change crosshair material to black
                    else
                    { LevelManager.Instance.controller.ChangeCrosshairMaterial(0); }
                }
            }
        }

        [SerializeField] private Animator planeAnimator;
        [SerializeField] private VariableJoystick variableJoystick;
        [SerializeField] private Transform airfieldStartPoint, airfieldEndPoint;

        [Space]

        [SerializeField] private LayerMask attackableLayerMask;
        [SerializeField] private LayerMask airFieldLayerMask;

        [Space]

        [SerializeField] private float flySpeed;
        [SerializeField] private float playerHeight = 3f;
        [SerializeField] private float sphereCastRadius = 1.5f;

        [SerializeField] private float raycastDistance = 2f;
        [SerializeField] private float rotationSpeed = 720f;
        [SerializeField] private float takeOffDuration = 0.5f;

        [Space]

        [SerializeField] private bool canFly = false;
        [SerializeField] private bool isFlying = false;
        [SerializeField] private bool isLanding = false;


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
            if (!GameEngine.Instance.IsPlaying())
            {
                return;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // sending a raycast forward to detect when to start landing
            if (Physics.Raycast(transform.position, forward, raycastDistance, airFieldLayerMask) && !isLanding)
            {
                // Debug.Log("hit detected");
                isLanding = true;
                isFlying = false;
                canFly = false;

                LevelManager.Instance.controller.StartLanding();
            }

            // sending a sphere cast on the ground to detect any enemies or money
            Collider[] hitColliders = Physics.OverlapSphere(transform.position - playerHeight * Vector3.up, sphereCastRadius, attackableLayerMask);
            int detectedEnemy = 0;

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.TryGetComponent(out CollectibleMoney collectibleMoney))
                {
                    collectibleMoney.Interact();
                }
                else if (hitColliders[i].gameObject.TryGetComponent(out Enemy enemy))
                {
                    detectedEnemy++;
                    Debug.Log("Enemy count in range: " + detectedEnemy.ToString());
                    enemy.Interact();
                }
            }

            detectedEnemyCount = detectedEnemy;
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
            Gizmos.DrawSphere(transform.position - playerHeight * Vector3.up, sphereCastRadius);
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

            transform.DOMove(new Vector3(0, 0, 2f), takeOffDuration * 1.2f).SetEase(Ease.InSine).OnComplete(() =>
            {
                transform.DORotate(new Vector3(-45f, 0f, 0f), takeOffDuration * 2f).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(0f, 0f, 0f), takeOffDuration * 2f).OnComplete(() =>
                    {
                        canFly = true;
                        isLanding = false;
                        // calling a function from controller controller script to trigger an event that disables crosshair 
                        LevelManager.Instance.controller.RevertCrosshairVisibility();
                    });
                });

                transform.DOMove(new Vector3(0f, 3f, 9f), takeOffDuration * 4f).SetEase(Ease.Unset);
            });


        }

        //___________________________________________________________________________________________________

        private void LandThePlane()
        {

            transform.DOMove(new Vector3(0f, 3f, 9f), takeOffDuration * 3f).OnComplete(() =>
            {
                transform.DORotate(new Vector3(45f, 180f, 0f), takeOffDuration * 2f).OnComplete(() =>
                {
                    transform.DORotate(Vector3.zero, takeOffDuration * 2f).OnComplete(() =>
                    {
                        isLanding = false;

                        LevelManager.Instance.controller.PlaneGrounded();
                    });
                });

                transform.DOMove(Vector3.zero, takeOffDuration * 5f);
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
