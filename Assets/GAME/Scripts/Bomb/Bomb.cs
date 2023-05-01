using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class Bomb : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private LayerMask bombTriggerLayerMask;

        [SerializeField] private float dropHeight = 3f;
        [SerializeField] private float dropDuraiton = 0.4f;
        [SerializeField] private float sphereCastRadius = 0.5f;
        [SerializeField] private float damage = 1f;


        [SerializeField] private bool canExplode = false;

        Collider[] hitColliders;




        //___________________________________________________________________________________________________

        private void FixedUpdate()
        {

            if (canExplode)
            {
                canExplode = false;

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereCastRadius, bombTriggerLayerMask);

                damage = InventoryManager.Instance.GetDamage();

                if (hitColliders.Length > 0)
                {
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
                else
                {
                    StartCoroutine(PlayParticle());
                }

            }
        }

        //___________________________________________________________________________________________________

        private IEnumerator PlayParticle()
        {
            particle.Play();
            Debug.Log("Bomb exploded");

            yield return new WaitUntil(() => particle.isStopped == true);

            PoolingManager.Instance.ReleasePooledObject(this.gameObject);
        }

        //___________________________________________________________________________________________________

        public void Interact()
        {
            canExplode = false;
            _renderer.enabled = true;
            transform.DOMove(transform.position - Vector3.up * dropHeight, dropDuraiton).SetEase(Ease.Flash).OnComplete(() =>
             {
                 _renderer.enabled = false;
                 canExplode = true;


             });
        }
    }

}
