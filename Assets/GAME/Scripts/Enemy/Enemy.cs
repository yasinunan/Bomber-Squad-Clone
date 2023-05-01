using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        private const string MONEY = "Money";
        private const string BULLET = "Bullet";
        [SerializeField] private GameObject player;

        [SerializeField] private EnemySO data;
        [SerializeField] private ParticleSystem particle;

        [SerializeField] private Renderer skinnedMeshRenderer;

        [SerializeField] private float currentHealth;
        [SerializeField] private float damage;
        [SerializeField] private float fireTimeRate;
        [SerializeField] private int moneyPrefabCount;
        [SerializeField] private float circleRadiusToDropMoney = 1.5f;
        [SerializeField] private float duration = 0.6f;
        [SerializeField] private float attackRange;

        [SerializeField] private float attackDuration;
        private float shootingTime;

        private bool isAttacking = false;
        private bool isDestroyed = false;
        //___________________________________________________________________________________________________

        private void Awake()
        {
            player = GameObject.Find("Player");
            skinnedMeshRenderer = GetComponentInChildren<Renderer>();

            currentHealth = data.maxHealth;
            damage = data.damage;
            fireTimeRate = data.fireTimeRate;
            moneyPrefabCount = data.moneyPrefabCount;
            attackRange = data.attackRange;
        }

        //___________________________________________________________________________________________________

        private void Start()
        {

        }

        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnDropBombs += OnDropBombs;
        }

        void OnDisable()
        {
            LevelManager.Instance.controller.OnDropBombs -= OnDropBombs;

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

                if (Time.time > shootingTime)
                {
                    shootingTime = Time.time + fireTimeRate;

                    if (isPlayerInRange())
                    {
                        particle.transform.LookAt(player.transform);

                        TriggerDamageEvent();
                        PlayParticle();
                    }

                    Debug.Log("enemy shooted");

                    /*GameObject bullet = PoolingManager.Instance.GetPooledObject(BULLET);
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);*/

                }
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

        public void TakeDamage(float damage)
        {
            //Debug.Log(damage);
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                skinnedMeshRenderer.enabled = false;
                for (int i = 0; i < moneyPrefabCount; i++)
                {
                    Vector2 v2RandomPoint = Random.insideUnitCircle * circleRadiusToDropMoney;
                    Vector3 v3RandomPoint = transform.position + new Vector3(v2RandomPoint.x, 0f, v2RandomPoint.y);

                    GameObject money = PoolingManager.Instance.GetPooledObject(MONEY);
                    money.transform.position = transform.position;
                    money.SetActive(true);
                    money.transform.DOMove(v3RandomPoint, duration).SetEase(Ease.OutQuint).OnComplete(() =>
                    {
                        if (!isDestroyed)
                        {
                            isDestroyed = true;
                            LevelManager.Instance.controller.DestroyedEnemy();
                            this.gameObject.SetActive(false);
                        }
                    });
                }
            }
        }

        //___________________________________________________________________________________________________

        private IEnumerator StopAttack()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                yield return null; ;
            }

            yield return new WaitForSeconds(attackDuration);

            isAttacking = false;
        }

        //___________________________________________________________________________________________________

        private void TriggerDamageEvent()
        {
            LevelManager.Instance.controller.EnemyAttack(damage);
        }

        //___________________________________________________________________________________________________

        private bool isPlayerInRange()
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance <= attackRange)
            { return true; }
            else
            { return false; }
        }

        //___________________________________________________________________________________________________

        private void PlayParticle()
        {
            if (particle != null && particle.isPlaying == false)
            {
                particle.Play();
            }
        }

        //___________________________________________________________________________________________________

        private void OnDropBombs(GameObject enemy, bool isObjectEnemy)
        {
            if (isObjectEnemy && ReferenceEquals(this.gameObject, enemy))
            {
                Interact();
            }
        }
    }
}


