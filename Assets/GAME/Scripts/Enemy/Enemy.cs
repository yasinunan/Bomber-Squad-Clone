using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class Enemy : MonoBehaviour, IInteractable, IDamagable
    {
        private const string MONEY = "Money";

        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private int moneyPrefabCount;
        [SerializeField] private float circleRadius = 1.5f;
        [SerializeField] private float duration = 0.6f;

        //___________________________________________________________________________________________________

        private void Awake()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        //___________________________________________________________________________________________________

        public void Interact()
        {



        }

        //___________________________________________________________________________________________________

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                meshRenderer.enabled = false;
                for (int i = 0; i <= moneyPrefabCount; i++)
                {
                    Vector2 v2RandomPoint = Random.insideUnitCircle * circleRadius;
                    Vector3 v3RandomPoint = transform.position + new Vector3(v2RandomPoint.x, 0f, v2RandomPoint.y);

                    GameObject money = PoolingManager.Instance.GetPooledObject(MONEY);
                    money.transform.position = transform.position;
                    money.SetActive(true);
                    money.transform.DOMove(v3RandomPoint, duration).SetEase(Ease.OutQuint).OnComplete(() =>
                    {
                        this.gameObject.SetActive(false);
                    });

                }
            }



        }

        //___________________________________________________________________________________________________

    }
}
