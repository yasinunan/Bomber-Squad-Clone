using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace YU.Template
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        protected const string MONEY = "Money";
        protected const string BULLET = "Bullet";

        [SerializeField] protected GameObject player;
        [SerializeField] protected ParticleSystem particle;

        [SerializeField] protected float currentHealth;
        [SerializeField] protected float damage;
        [SerializeField] protected float fireTimeRate;
        [SerializeField] protected int moneyPrefabCount;
        [SerializeField] protected float circleRadiusToDropMoney = 1.5f;
        [SerializeField] protected float duration = 0.6f;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float attackDuration;


        protected float shootingTime;
        protected bool isAttacking = false;
        protected bool isDestroyed = false;

        //___________________________________________________________________________________________________



        public virtual void TakeDamage(float damage) { }
        protected virtual void Shoot() { }
        protected virtual void StopShooting() { }

        //___________________________________________________________________________________________________

        protected IEnumerator StopAttack()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                yield return null; ;
            }

            yield return new WaitForSeconds(attackDuration);
            isAttacking = false;
        }

        //___________________________________________________________________________________________________

        protected void TriggerDamageEvent()
        {
            LevelManager.Instance.controller.EnemyAttack(damage);
        }

        //___________________________________________________________________________________________________

        protected void LookAtTarget()
        {
            particle.transform.LookAt(player.transform);


            Vector3 playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
            transform.LookAt(playerPos);
        }

        //___________________________________________________________________________________________________

        protected bool isPlayerInRange()
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance <= attackRange)
            { return true; }
            else
            { return false; }
        }


        //___________________________________________________________________________________________________

        protected void PlayParticle()
        {
            if (particle != null && particle.isPlaying == false)
            {
                particle.Play();
            }
        }

        //___________________________________________________________________________________________________

        protected void StopParticle()
        {
            if (particle != null && particle.isPlaying)
            {
                particle.Stop();
            }
        }

        //___________________________________________________________________________________________________

        protected void SpreadMoney()
        {
            for (int i = 0; i < moneyPrefabCount; i++)
            {
                Vector2 v2RandomPoint = Random.insideUnitCircle * circleRadiusToDropMoney;
                Vector3 v3RandomPoint = transform.position + new Vector3(v2RandomPoint.x, 0f, v2RandomPoint.y);

                GameObject money = PoolingManager.Instance.GetPooledObject(MONEY);
                money.transform.position = transform.position;
                money.SetActive(true);
                money.transform.DOMove(v3RandomPoint, duration).SetEase(Ease.OutQuint);
            }
        }
    }
}

