using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YU.Template
{
    public class CrosshairHandler : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        [SerializeField] Sprite[] sprites;

        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnRevertCrosshairVisibility += OnRevertCrosshairVisibility;
            LevelManager.Instance.controller.OnChangeCrosshairMaterial += OnChangeCrosshairMaterial;
        }

        //___________________________________________________________________________________________________

        void OnDisable()
        {
            LevelManager.Instance.controller.OnRevertCrosshairVisibility -= OnRevertCrosshairVisibility;
            LevelManager.Instance.controller.OnChangeCrosshairMaterial -= OnChangeCrosshairMaterial;
        }

        //___________________________________________________________________________________________________

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }

        //___________________________________________________________________________________________________

        private void OnRevertCrosshairVisibility()
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            Debug.Log("crosshair visiblity reverted");
        }

        //___________________________________________________________________________________________________

        private void OnChangeCrosshairMaterial(int material)
        {
            if (spriteRenderer.sprite = sprites[0])
            {
                spriteRenderer.sprite = sprites[1];
            }
            else
            {
                spriteRenderer.sprite = sprites[0];
            }
        }

        //___________________________________________________________________________________________________



    }
}

