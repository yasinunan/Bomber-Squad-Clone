using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private LayerMask bombTriggerLayerMask;

        [SerializeField] private float dropHeight = 3.1f;
        [SerializeField] private float dropDuraiton = 0.4f;
        [SerializeField] private float sphereCastRadius = 0.5f;
        [SerializeField] private float damage = 1f;


        [SerializeField] private bool canExplode = false;

        Collider[] hitColliders;

        //___________________________________________________________________________________________________

        private void Awake()
        {
            //particle.Stop();
        }

        //___________________________________________________________________________________________________

        private void Start()
        {
            transform.DOMove(transform.position - Vector3.up * dropHeight, dropDuraiton).SetEase(Ease.Flash).OnComplete(() =>
            {
                _renderer.enabled = false;
                canExplode = true;

                Debug.Log("Bomb exploded");

            });
        }

        //___________________________________________________________________________________________________

        private void FixedUpdate()
        {

            if (canExplode)
            {
                canExplode = false;

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereCastRadius, bombTriggerLayerMask);

                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.TakeDamage(damage);
                    }

                    if (i == hitColliders.Length - 1)
                    {
                        StartCoroutine(PlayParticle());
                    }
                }
            }
        }

        //___________________________________________________________________________________________________

        private IEnumerator PlayParticle()
        {
            particle.Play();

            yield return new WaitUntil(() => particle.isStopped == true);

            PoolingManager.Instance.ReleasePooledObject(this.gameObject);
        }



    }

}
