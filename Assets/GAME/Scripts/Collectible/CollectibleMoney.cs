using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YU.Template
{
    public class CollectibleMoney : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float animDuration = 0.5f;
        [SerializeField] private float maxLocalScale = 5f;
        private bool didInteract = false;

        //___________________________________________________________________________________________________

        private void Awake()
        {
            player = player = GameObject.Find("Player");
            didInteract = false;
        }

        //___________________________________________________________________________________________________

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            didInteract = false;
        }

        //___________________________________________________________________________________________________

        public void Interact()
        {
            if (!didInteract)
            {
                didInteract = true;


                transform.DOMove(player.transform.position, animDuration);

                transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), animDuration).OnComplete(() =>
                {
                    LevelManager.Instance.controller.CollectedMoney();
                    PoolingManager.Instance.ReleasePooledObject(this.gameObject);
                });
            }
        }
    }
}

