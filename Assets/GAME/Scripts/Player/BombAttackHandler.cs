using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YU.Template
{
    public class BombAttackHandler : MonoBehaviour
    {

        private const string BOMB_TO_DROP = "bombToDrop";

        [Space]

        [SerializeField] private float dropTimeRate = 0.5f;

        [Space]

        [SerializeField] private bool isAttacking = false;

        private Coroutine coroutine;

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

        }

        //___________________________________________________________________________________________________

        private IEnumerator DropBomb()
        {
            if (!GameEngine.Instance.IsPlaying())
            {
                yield break;
            }

            while (true)
            {
                if (isAttacking)
                {

                    Debug.Log("DROPPING BOMB");
                    // getting a bomb object from pooling manager
                    GameObject bomb = PoolingManager.Instance.GetPooledObject(BOMB_TO_DROP);

                    bomb.transform.position = this.transform.position;
                    bomb.gameObject.SetActive(true);

                    
                    yield return new WaitForSeconds(dropTimeRate);
                }
                else
                {
                    yield return new WaitForSeconds(dropTimeRate);
                }
            }
        }

        //___________________________________________________________________________________________________

        private void OnDropBombs(int detectedEnemyCount)
        {
            Debug.Log("bomb event triggered");

            if (detectedEnemyCount > 0)
            {
                if (!isAttacking)
                {
                    isAttacking = true;

                    if (coroutine == null)
                    {
                        coroutine = StartCoroutine(DropBomb());
                    }
                }
            }
            else
            {
                isAttacking = false;

            }
        }

        //___________________________________________________________________________________________________

    }
}

