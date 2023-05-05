using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YU.Template
{
    public class UpgradeScreenManager : MonoBehaviour
    {
        [SerializeField] private UpgradeDataSO upgradeData;
        [SerializeField] private Button armorUpgradeButton;
        [SerializeField] private Button bombCapacityUpgradeButton;
        [SerializeField] private Button damageUpgradeButton;
        [SerializeField] private Button nextLevelButton;

        [SerializeField] private TextMeshProUGUI capacityPriceText;
        [SerializeField] private TextMeshProUGUI capacityLevelText;

        [SerializeField] private TextMeshProUGUI armorPriceText;
        [SerializeField] private TextMeshProUGUI armorLevelText;

        [SerializeField] private TextMeshProUGUI damagePriceText;
        [SerializeField] private TextMeshProUGUI damageLevelText;

        private int capacityUpgradePrice, armorUpgradePrice, damageUpgradePrice;


        //___________________________________________________________________________________________________

        void Start()
        {
            nextLevelButton.interactable = false;
            nextLevelButton.image.color = Color.white;

            int tempCapacity = InventoryManager.Instance.GetBombsCount();
            if (tempCapacity == 0)
            {
                InventoryManager.Instance.SetBombsCount(upgradeData.initialBombCapacity);
            }

            float tempArmor = InventoryManager.Instance.GetArmor();
            if (tempArmor == 0f)
            {
                InventoryManager.Instance.SetArmor(upgradeData.initialArmor);
            }
            float tempDamage = InventoryManager.Instance.GetDamage();
            if (tempDamage == 0f)
            {
                InventoryManager.Instance.SetDamage(upgradeData.initialDamage);
            }
        }

        //___________________________________________________________________________________________________

        void OnEnable()
        {
            LevelManager.Instance.controller.OnPlaneGrounded += OnPlaneGrounded;
            LevelManager.Instance.controller.OnEnableNextLevel += OnEnableNextLevel;

        }

        //___________________________________________________________________________________________________

        void OnDisable()
        {
            LevelManager.Instance.controller.OnPlaneGrounded -= OnPlaneGrounded;
            LevelManager.Instance.controller.OnEnableNextLevel -= OnEnableNextLevel;

        }

        //___________________________________________________________________________________________________

        private void PrepareCapacityUpgrade()
        {
            int money = LevelManager.Instance.datas.GetMoney();

            int lastPrice = InventoryManager.Instance.GetLastCapacityUpgradePrice();
            int lastUpgradeCount = InventoryManager.Instance.GetLastCapacityUpgradeCount();

            capacityUpgradePrice = lastPrice + upgradeData.nextUpgradePriceIncreaseAmount;
            capacityPriceText.text = capacityUpgradePrice.ToString() + "$";
            capacityLevelText.text = "Lv " + (lastUpgradeCount + 1).ToString();

            if (lastUpgradeCount < upgradeData.maxUpgradeCount && money >= capacityUpgradePrice)
            {
                bombCapacityUpgradeButton.interactable = true;
            }
            else
            {
                bombCapacityUpgradeButton.interactable = false;
            }
        }

        //___________________________________________________________________________________________________

        private void PrepareArmorUpgrade()
        {
            int money = LevelManager.Instance.datas.GetMoney();

            int lastPrice = InventoryManager.Instance.GetLastArmorUpgradePrice();
            int lastUpgradeCount = InventoryManager.Instance.GetLastArmorUpgradeCount();

            armorUpgradePrice = lastPrice + upgradeData.nextUpgradePriceIncreaseAmount;
            armorPriceText.text = armorUpgradePrice.ToString() + "$";
            armorLevelText.text = "Lv " + (lastUpgradeCount + 1).ToString();

            if (lastUpgradeCount < upgradeData.maxUpgradeCount && money >= armorUpgradePrice)
            {
                armorUpgradeButton.interactable = true;
            }
            else
            {
                armorUpgradeButton.interactable = false;
            }
        }

        //___________________________________________________________________________________________________

        private void PrepareDamageUpgrade()
        {
            int money = LevelManager.Instance.datas.GetMoney();

            int lastPrice = InventoryManager.Instance.GetLastDamageUpgradePrice();
            int lastUpgradeCount = InventoryManager.Instance.GetLastDamageUpgradeCount();

            damageUpgradePrice = lastPrice + upgradeData.nextUpgradePriceIncreaseAmount;
            damagePriceText.text = damageUpgradePrice.ToString() + "$";
            damageLevelText.text = "Lv " + (lastUpgradeCount + 1).ToString();

            if (lastUpgradeCount < upgradeData.maxUpgradeCount && money >= damageUpgradePrice)
            {
                damageUpgradeButton.interactable = true;
            }
            else
            {
                damageUpgradeButton.interactable = false;
            }
        }

        //___________________________________________________________________________________________________


        public void UpgradeBombCapacityClick()
        {
            int bombsCount = InventoryManager.Instance.GetBombsCount();
            InventoryManager.Instance.SetBombsCount(bombsCount + upgradeData.bombCapacityIncreaseAmount);

            int lastPrice = InventoryManager.Instance.GetLastCapacityUpgradePrice();
            InventoryManager.Instance.SetLastCapacityUpgradePrice(lastPrice + upgradeData.nextUpgradePriceIncreaseAmount);

            int lastUpgradeCount = InventoryManager.Instance.GetLastCapacityUpgradeCount();
            InventoryManager.Instance.SetLastCapacityUpgradeCount(lastUpgradeCount + 1);


            int money = LevelManager.Instance.datas.GetMoney();
            money -= capacityUpgradePrice;
            LevelManager.Instance.datas.SetMoney(money);

            LevelManager.Instance.controller.BombCapacityUpgraded(bombsCount + upgradeData.bombCapacityIncreaseAmount);

            PrepareCapacityUpgrade();
            PrepareArmorUpgrade();
            PrepareDamageUpgrade();

        }

        //___________________________________________________________________________________________________

        public void UpgradeArmorClick()
        {
            float armor = InventoryManager.Instance.GetArmor();
            InventoryManager.Instance.SetArmor(armor + upgradeData.armorIncreaseAmount);

            int lastPrice = InventoryManager.Instance.GetLastArmorUpgradePrice();
            InventoryManager.Instance.SetLastArmorUpgradePrice(lastPrice + upgradeData.nextUpgradePriceIncreaseAmount);

            int lastUpgradeCount = InventoryManager.Instance.GetLastArmorUpgradeCount();
            InventoryManager.Instance.SetLastArmorUpgradeCount(lastUpgradeCount + 1);


            int money = LevelManager.Instance.datas.GetMoney();
            money -= armorUpgradePrice;
            LevelManager.Instance.datas.SetMoney(money);

            LevelManager.Instance.controller.ArmorUpgraded(armor + upgradeData.armorIncreaseAmount);

            PrepareArmorUpgrade();
            PrepareCapacityUpgrade();
            PrepareDamageUpgrade();

        }


        //___________________________________________________________________________________________________

        public void UpgradeDamageClick()
        {
            float damage = InventoryManager.Instance.GetArmor();
            InventoryManager.Instance.SetArmor(damage + upgradeData.damageIncreaseAmount);

            int lastPrice = InventoryManager.Instance.GetLastDamageUpgradePrice();
            InventoryManager.Instance.SetLastDamageUpgradePrice(lastPrice + upgradeData.nextUpgradePriceIncreaseAmount);

            int lastUpgradeCount = InventoryManager.Instance.GetLastDamageUpgradeCount();
            InventoryManager.Instance.SetLastDamageUpgradeCount(lastUpgradeCount + 1);


            int money = LevelManager.Instance.datas.GetMoney();
            money -= damageUpgradePrice;
            LevelManager.Instance.datas.SetMoney(money);

            LevelManager.Instance.controller.DamageUpgraded(damage + upgradeData.damageIncreaseAmount);

            PrepareDamageUpgrade();
            PrepareArmorUpgrade();
            PrepareCapacityUpgrade();
        }

        //___________________________________________________________________________________________________
        //
        // EVENTS
        //___________________________________________________________________________________________________

        private void OnPlaneGrounded()
        {
            PrepareCapacityUpgrade();
            PrepareArmorUpgrade();
            PrepareDamageUpgrade();
        }

        //___________________________________________________________________________________________________

        private void OnEnableNextLevel()
        {

            nextLevelButton.interactable = true;
            nextLevelButton.image.color = Color.blue;
        }

        public void OnButtonClick()
        {
            nextLevelButton.interactable = false;
            nextLevelButton.image.color = Color.white;
            LevelManager.Instance.controller.CompleteLevel();
        }
    }

}

