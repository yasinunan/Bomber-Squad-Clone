using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace YU.Template
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool isVisible = false;

        Coroutine healthBarCoroutine;

        //___________________________________________________________________________________________________

        void Start()
        {
            canvasGroup.alpha = 0;
        }

        //___________________________________________________________________________________________________

        void OnEnable()
        {

            LevelManager.Instance.controller.OnHealthChanged += OnHealthChanged;
            LevelManager.Instance.controller.OnRevertCrosshairVisibility += OnRevertHealthBarVisibility;

        }

        //___________________________________________________________________________________________________

        void OnDisable()
        {
            LevelManager.Instance.controller.OnHealthChanged -= OnHealthChanged;
            LevelManager.Instance.controller.OnRevertCrosshairVisibility -= OnRevertHealthBarVisibility;


        }

        //___________________________________________________________________________________________________


        void LateUpdate()
        {

            canvasGroup.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        }

        //___________________________________________________________________________________________________

        private void SetProgressBar(float currentHealth, float maxHealth)
        {

            //healthBar.fillAmount = currentHealth / maxHealth;
            if (healthBarCoroutine != null)
            {
                StopCoroutine(healthBarCoroutine);
            }
            healthBarCoroutine = StartCoroutine(ChangeImageFil(healthBar, currentHealth / maxHealth));

        }

        //___________________________________________________________________________________________________

        public void ResetProgressBar()
        {

            healthBar.fillAmount = 1f;
        }

        //___________________________________________________________________________________________________

        IEnumerator ChangeImageFil(Image image, float value)
        {
            float elapsedTime = 0f;
            float waitTime = 0.6f;
            while (elapsedTime < waitTime)
            {
                image.fillAmount = Mathf.Lerp(image.fillAmount, value, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            image.fillAmount = value;
            yield return null;
        }

        //___________________________________________________________________________________________________

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            SetProgressBar(currentHealth, maxHealth);
        }

        //___________________________________________________________________________________________________

        private void OnRevertHealthBarVisibility()
        {
            isVisible = !isVisible;
            canvasGroup.alpha = isVisible ? 1 : 0;
        }


    }
}
