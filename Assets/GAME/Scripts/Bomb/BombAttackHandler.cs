using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YU.Template
{
    public class BombAttackHandler : MonoBehaviour
    {

        private const string BOMB_TO_DROP = "Bomb";

        [Space]

        [SerializeField] private float dropTimeRate = 0.5f;

        [Space]

        [SerializeField] private float attackDuration = 1.1f;
        [SerializeField] private float shootingTime;
        [SerializeField] private bool isAttacking = false;
        [SerializeField] private bool canAttack = false;
        [SerializeField] private bool attackStarted = false;

        private Coroutine coroutine, stopCoroutine;

        //___________________________________________________________________________________________________

        private void OnEnable()
        {
            LevelManager.Instance.controller.OnDropBombs += OnDropBombs;
        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            LevelManager.Instance.controller.OnDropBombs -= OnDropBombs;
        }

        //___________________________________________________________________________________________________

        void Start()
        {
            coroutine = null;
            stopCoroutine = null;
        }

        //___________________________________________________________________________________________________
        void Update()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                return;
            }

            if (Time.time > shootingTime)
            {
                if (isAttacking)
                {
                    Debug.Log("DROPPING BOMB");
     
                    GameObject bomb = PoolingManager.Instance.GetPooledObject(BOMB_TO_DROP);

                    bomb.transform.position = this.transform.position;
                    bomb.gameObject.SetActive(true);
                    if(bomb.TryGetComponent<Bomb>(out Bomb _bomb))
                    {
                        _bomb.Interact();
                    }

                    LevelManager.Instance.controller.BombDropped();
                }
                shootingTime = Time.time + dropTimeRate;

            }
        }


        //___________________________________________________________________________________________________

        private IEnumerator StopAttack()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                yield break;
            }

            yield return new WaitForSeconds(attackDuration);

            isAttacking = false;
        }


        //___________________________________________________________________________________________________

        private void OnDropBombs(GameObject enemy, bool bIsObjectEnemy)
        {
            // Debug.Log("bomb event triggered");
            if (LevelManager.Instance.datas.HasEnoughBombs() && bIsObjectEnemy)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(StopAttack());
                }
            }
        }

        //___________________________________________________________________________________________________

    }
}

