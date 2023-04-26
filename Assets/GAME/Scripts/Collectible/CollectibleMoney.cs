using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YU.Template
{
    public class CollectibleMoney : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            PoolingManager.Instance.ReleasePooledObject(this.gameObject);
        }
    }
}

