using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YU.Template
{
    public class PlayScreenManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bombText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Image levelProgressImage;
        Coroutine ProgressBarCoroutine;
        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnBombAmountChanged += OnBombAmountChanged;
            LevelManager.Instance.controller.OnMoneyChanged += OnMoneyChanged;
            LevelManager.Instance.controller.OnLevelProgressValueChanged += OnLevelProgressValueChanged;
        }

        //___________________________________________________________________________________________________

        void OnDisable()
        {
            LevelManager.Instance.controller.OnBombAmountChanged -= OnBombAmountChanged;
            LevelManager.Instance.controller.OnMoneyChanged -= OnMoneyChanged;
            LevelManager.Instance.controller.OnLevelProgressValueChanged -= OnLevelProgressValueChanged;
        }

        //___________________________________________________________________________________________________


        void Start()
        {

        }

        //___________________________________________________________________________________________________

        void Update()
        {

        }

        private void SetProgressBar(float currentHealth, float maxHealth)
        {

            //healthBar.fillAmount = currentHealth / maxHealth;
            if (ProgressBarCoroutine != null)
            {
                StopCoroutine(ProgressBarCoroutine);
            }

            ProgressBarCoroutine = StartCoroutine(ChangeImageFil(levelProgressImage, currentHealth / maxHealth));
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

        private void OnBombAmountChanged(int currentBombAmount, int maxBombAmount)
        {
            bombText.text = currentBombAmount.ToString() + "/" + maxBombAmount.ToString();
        }

        //___________________________________________________________________________________________________

        private void OnMoneyChanged(int currentMoney)
        {
            moneyText.text = currentMoney.ToString();

        }

        private void OnLevelProgressValueChanged(float current, float max)
        {
            SetProgressBar(current, max);
        }
    }

}
