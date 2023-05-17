using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YU.Template
{
    public class EnemyTank : Enemy
    {
        [SerializeField] private EnemySO data;
        [SerializeField] private Renderer _renderer;


        //___________________________________________________________________________________________________

        private void Awake()
        {
            player = GameObject.Find("Player");
            _renderer = GetComponentInChildren<Renderer>();

            currentHealth = data.maxHealth;
            damage = data.damage;
            fireTimeRate = data.fireTimeRate;
            moneyPrefabCount = data.moneyPrefabCount;
            attackRange = data.attackRange;
        }

        //___________________________________________________________________________________________________


        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnTargetDetected += OnTargetDetected;
        }

        void OnDisable()
        {
            LevelManager.Instance.controller.OnTargetDetected -= OnTargetDetected;

        }

        //___________________________________________________________________________________________________

        private void Update()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                return;
            }

            if (isAttacking)
            {
                Shoot();
            }
            else
            {
                StopShooting();
            }
        }

        //___________________________________________________________________________________________________

        public void Interact()
        {

            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(StopAttack());
            }
        }

        //___________________________________________________________________________________________________

        protected override void Shoot()
        {
            LookAtTarget();

            if (Time.time > shootingTime)
            {
                shootingTime = Time.time + fireTimeRate;

                if (isPlayerInRange())
                {
                    Shoot();
                    TriggerDamageEvent();
                    PlayParticle();
                }

                Debug.Log("enemy shooted");

                /*GameObject bullet = PoolingManager.Instance.GetPooledObject(BULLET);
                bullet.transform.position = transform.position;
                bullet.SetActive(true);*/
            }
        }

        //___________________________________________________________________________________________________

        protected override void StopShooting()
        {
            StopParticle();
        }

        //___________________________________________________________________________________________________

        public override void TakeDamage(float damage)
        {
            //Debug.Log(damage);
            currentHealth -= damage;

            if (currentHealth <= 0f && !isDestroyed)
            {
                isDestroyed = true;
                currentHealth = 0f;

                _renderer.enabled = false;
               
                SpreadMoney();
                
                LevelManager.Instance.controller.DestroyedEnemy();
                this.gameObject.SetActive(false);
            }
        }

        //___________________________________________________________________________________________________

        private void OnTargetDetected(GameObject enemy, bool isObjectEnemy)
        {
            if (isObjectEnemy && ReferenceEquals(this.gameObject, enemy))
            {
                Interact();
            }
        }
    }
}


