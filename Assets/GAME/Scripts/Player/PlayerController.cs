using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class PlayerController : MonoBehaviour
    {
        const string strLayerBorder = "Border";
        const string strLayerAirField = "AirField";
        const string strLayerAttackable = "Attackable";
        const string strLayerMoney = "Money";

        private int nLayerBorder;
        private int nLayerAirField;
        private int nLayerAttackable;
        private int nLayerMoney;

        // [SerializeField] private Animator planeAnimator;
        [SerializeField] private VariableJoystick variableJoystick;
        [SerializeField] private Transform airfieldStartPoint, airfieldEndPoint;
        [SerializeField] private Transform visuals;
        [SerializeField] private ParticleSystem upgradeParticle;
        [SerializeField] private ParticleSystem crashParticle;


        [Space]

        [SerializeField] private LayerMask attackableLayerMask;
        [SerializeField] private LayerMask airFieldLayerMask;

        [Space]

        [SerializeField] private float flySpeed;
        [SerializeField] private float playerHeight = 3f;
        [SerializeField] private float sphereCastRadius = 1.5f;

        [SerializeField] private float crashRotationSpeed = 10f;


        [SerializeField] private float raycastDistance = 2f;
        [SerializeField] private float rotationSpeed = 720f;
        [SerializeField] private float takeOffDuration = 0.5f;
        [SerializeField] private float maxRollAngle = 30f;
        [SerializeField] private float smoothness = 10f;

        [Space]

        [SerializeField] private bool canFly = false;
        [SerializeField] private bool isFlying = false;
        [SerializeField] private bool isLanding = false;
        [SerializeField] private bool isCrashed = false;


        private int _detectedEnemyCount;
        private int detectedEnemyCount
        {
            get { return _detectedEnemyCount; }
            set
            {
                if (value != _detectedEnemyCount && value > 0)
                {
                    _detectedEnemyCount = value;

                    LevelManager.Instance.controller.ChangeCrosshairMaterial(1);
                }
                else if (value == 0 && value != _detectedEnemyCount)
                {
                    _detectedEnemyCount = value;
                    LevelManager.Instance.controller.ChangeCrosshairMaterial(0);
                }
            }
        }

        //___________________________________________________________________________________________________

        private void OnEnable()
        {
            LevelManager.Instance.controller.OnPlaneTakingOff += OnPlaneTakingOff;
            LevelManager.Instance.controller.OnStartLanding += OnStartLanding;
            LevelManager.Instance.controller.OnBombCapacityUpgraded += OnBombCapacityUpgraded;
            LevelManager.Instance.controller.OnArmorUpgraded += OnArmorUpgraded;
            LevelManager.Instance.controller.OnDamageUpgraded += OnDamageUpgraded;
            LevelManager.Instance.controller.OnCrashPlane += OnCrashPlane;

            GameEngine.Instance.OnPrepareNewGame += OnPrepareNewGame;
        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            LevelManager.Instance.controller.OnPlaneTakingOff -= OnPlaneTakingOff;
            LevelManager.Instance.controller.OnStartLanding -= OnStartLanding;
            LevelManager.Instance.controller.OnBombCapacityUpgraded -= OnBombCapacityUpgraded;
            LevelManager.Instance.controller.OnArmorUpgraded -= OnArmorUpgraded;
            LevelManager.Instance.controller.OnDamageUpgraded -= OnDamageUpgraded;
            LevelManager.Instance.controller.OnCrashPlane -= OnCrashPlane;

            GameEngine.Instance.OnPrepareNewGame -= OnPrepareNewGame;
        }

        //___________________________________________________________________________________________________

        void Start()
        {
            nLayerBorder = LayerMask.NameToLayer(strLayerBorder);
            nLayerAirField = LayerMask.NameToLayer(strLayerAirField);
            nLayerAttackable = LayerMask.NameToLayer(strLayerAttackable);
            nLayerMoney = LayerMask.NameToLayer(strLayerMoney);
        }

        //___________________________________________________________________________________________________

        private void FixedUpdate()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                return;
            }

            if (isCrashed)
            {
                return;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // sending a raycast forward to detect when to start landing or border
            if (Physics.Raycast(transform.position, forward, out RaycastHit hit, raycastDistance, airFieldLayerMask) && !isLanding)
            {
                if (hit.collider.gameObject.layer.Equals(nLayerAirField))
                {
                    isLanding = true;
                    isFlying = false;
                    canFly = false;

                    LevelManager.Instance.controller.StartLanding();
                }
                else if (hit.collider.gameObject.layer.Equals(nLayerBorder))
                {
                    Quaternion toRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
                    transform.rotation = toRotation;
                }
            }

            // sending a sphere cast on the ground to detect any enemies or money
            Collider[] hitColliders = Physics.OverlapSphere(transform.position - playerHeight * Vector3.up, sphereCastRadius, attackableLayerMask);

            int detectedEnemy = 0;
            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].gameObject.layer.Equals(nLayerAttackable))
                    {
                        detectedEnemy++;
                        LevelManager.Instance.controller.TargetDetected(hitColliders[i].gameObject, true);
                    }
                    else if (hitColliders[i].gameObject.layer.Equals(nLayerMoney))
                    {
                        LevelManager.Instance.controller.TargetDetected(hitColliders[i].gameObject, false);
                    }
                }
                detectedEnemyCount = detectedEnemy;
            }
            else
            {
                detectedEnemyCount = detectedEnemy;
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

                // Trigger take off event
                LevelManager.Instance.controller.PlaneTakingOff();
            }

            if (canFly)
            {
                PlaneMovement(moveDirection);
                ApplyRollingAnimation(moveDirection);
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
            if (moveDir != Vector3.zero && !isCrashed)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        //___________________________________________________________________________________________________

        private void ApplyRollingAnimation(Vector3 moveDir)
        {
            // Calculate the roll angle based on the input direction
            float rollAngle = -Mathf.Clamp(moveDir.x + moveDir.z, -1f, 1f) * maxRollAngle;

            // Apply the roll animation to the visuals GameObject
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, rollAngle);
            visuals.localRotation = Quaternion.Lerp(visuals.localRotation, targetRotation, Time.deltaTime * smoothness);
        }

        //___________________________________________________________________________________________________

        private void OnPlaneTakingOff()
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

                transform.DOMove(airfieldEndPoint.position, takeOffDuration * 4f).SetEase(Ease.Unset);
            });
        }

        //___________________________________________________________________________________________________

        private void LandThePlane()
        {
            float fTime = Vector3.Distance(transform.position, airfieldEndPoint.position) / flySpeed;

            transform.DOMove(airfieldEndPoint.position, fTime).OnComplete(() =>
            {
                Quaternion targetRotation = Quaternion.Euler(Vector3.zero);
                visuals.localRotation = targetRotation;

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

            transform.DOLookAt(airfieldStartPoint.position, fTime);
        }

        //___________________________________________________________________________________________________

        private void PlayParticle()
        {
            if (upgradeParticle != null)
            {
                if (upgradeParticle.isPlaying)
                {
                    upgradeParticle.Stop();
                    upgradeParticle.Play();
                }
                else
                {
                    upgradeParticle.Play();
                }
            }
        }

        //___________________________________________________________________________________________________
        private void StartCrashSequence()
        {
            if (isCrashed)
            {
                return;
            }

            isCrashed = true;
            CrashSequence();
        }

        //___________________________________________________________________________________________________

        private void CrashSequence()
        {
            crashParticle.Play();
            Vector3 startPosition = transform.position;

            Sequence crashSequence = DOTween.Sequence();

            // Move the plane downwards
            crashSequence.Append(transform.DOMoveY(0f, 3f));

            // Rotate the plane randomly
            crashSequence.Append(transform.DORotate(new Vector3(0f,
                Random.Range(-crashRotationSpeed, crashRotationSpeed),
                Random.Range(-crashRotationSpeed, crashRotationSpeed)), 3f));

            // TO DO: implement a crashing FX here
            crashSequence.OnComplete(() =>
            {
                LevelManager.Instance.controller.FailLevel();
                crashParticle.Stop();
            });
        }


        // EVENTS
        //___________________________________________________________________________________________________
        //

        private void OnPrepareNewGame(bool bIsRematch)
        {
            canFly = false;
            isFlying = false;
            isLanding = false;
            isCrashed = false;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            visuals.transform.localRotation = Quaternion.identity;

        }

        //___________________________________________________________________________________________________

        private void OnStartLanding()
        {
            LevelManager.Instance.controller.RevertCrosshairVisibility();
            LandThePlane();
        }

        //___________________________________________________________________________________________________

        void OnBombCapacityUpgraded(int newCapacity)
        {
            PlayParticle();
        }

        //___________________________________________________________________________________________________

        void OnDamageUpgraded(float damage)
        {
            PlayParticle();
        }

        //___________________________________________________________________________________________________

        void OnArmorUpgraded(float damage)
        {
            PlayParticle();
        }

        //___________________________________________________________________________________________________

        void OnCrashPlane()
        {
            StartCrashSequence();
        }
    }
}

