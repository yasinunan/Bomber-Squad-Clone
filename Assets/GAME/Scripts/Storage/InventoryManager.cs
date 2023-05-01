using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


namespace YU.Template
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        //___________________________________________________________________________________________________

        [Header("User Inventory Non-Consumables")]

        [SerializeField] private int nCoinsCount;


        //___________________________________________________________________________________________________

        private void Start()
        {
            UpdateDebugInfo();
        }

        //___________________________________________________________________________________________________

        public void UpdateDebugInfo()
        {

            nCoinsCount = GetCoinsCount();
        }



        //___________________________________________________________________________________________________
        //
        // BOMBS
        //
        //___________________________________________________________________________________________________

        public int GetBombsCount()
        {
            int nRet = PlayerPrefs.GetInt("BombsCount", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetBombsCount(int nCount)
        {
            PlayerPrefs.SetInt("BombsCount", nCount);

        }

        //___________________________________________________________________________________________________

        public int GetLastCapacityUpgradeCount()
        {
            int nRet = PlayerPrefs.GetInt("LastCapacityUpgradeCount", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastCapacityUpgradeCount(int nCount)
        {
            PlayerPrefs.SetInt("LastCapacityUpgradeCount", nCount);

        }


        //___________________________________________________________________________________________________

        public int GetLastCapacityUpgradePrice()
        {
            int nRet = PlayerPrefs.GetInt("LastCapacityUpgradePrice", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastCapacityUpgradePrice(int nCount)
        {
            PlayerPrefs.SetInt("LastCapacityUpgradePrice", nCount);

        }



        //___________________________________________________________________________________________________
        //
        // ARMOR
        //
        //___________________________________________________________________________________________________

        public float GetArmor()
        {
            float nRet = PlayerPrefs.GetFloat("MaxArmor", 0f);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetArmor(float nCount)
        {
            PlayerPrefs.SetFloat("MaxArmor", nCount);

        }

        public int GetLastArmorUpgradeCount()
        {
            int nRet = PlayerPrefs.GetInt("LastArmorUpgradeCount", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastArmorUpgradeCount(int nCount)
        {
            PlayerPrefs.SetInt("LastArmorUpgradeCount", nCount);

        }


        //___________________________________________________________________________________________________

        public int GetLastArmorUpgradePrice()
        {
            int nRet = PlayerPrefs.GetInt("LastArmorUpgradePrice", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastArmorUpgradePrice(int nCount)
        {
            PlayerPrefs.SetInt("LastArmorUpgradePrice", nCount);

        }
        //___________________________________________________________________________________________________
        //
        // DAMAGE
        //
        //___________________________________________________________________________________________________

        public float GetDamage()
        {
            float nRet = PlayerPrefs.GetFloat("MaxDamage", 0f);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetDamage(float nCount)
        {
            PlayerPrefs.SetFloat("MaxDamage", nCount);

        }

        //___________________________________________________________________________________________________

        public int GetLastDamageUpgradeCount()
        {
            int nRet = PlayerPrefs.GetInt("LastDamageUpgradeCount", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastDamageUpgradeCount(int nCount)
        {
            PlayerPrefs.SetInt("LastDamageUpgradeCount", nCount);

        }


        //___________________________________________________________________________________________________

        public int GetLastDamageUpgradePrice()
        {
            int nRet = PlayerPrefs.GetInt("LastDamageUpgradePrice", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetLastDamageUpgradePrice(int nCount)
        {
            PlayerPrefs.SetInt("LastDamageUpgradePrice", nCount);

        }
        //___________________________________________________________________________________________________


        //___________________________________________________________________________________________________
        //
        // COINs
        //
        //___________________________________________________________________________________________________

        public int GetCoinsCount()
        {
            int nRet = PlayerPrefs.GetInt("CoinsCount", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________

        public void SetCoinsCount(int nCount)
        {
            PlayerPrefs.SetInt("CoinsCount", nCount);

            UpdateDebugInfo();


        }

        //___________________________________________________________________________________________________

        public void IncreaseCoinsCount(int nCount = 1)
        {
            PlayerPrefs.SetInt("CoinsCount", GetCoinsCount() + nCount);

            UpdateDebugInfo();
        }

        //___________________________________________________________________________________________________

        public void DecreaseCoinsCount(int nCount = 1)
        {
            PlayerPrefs.SetInt("CoinsCount", GetCoinsCount() - nCount);

            UpdateDebugInfo();
        }

        //___________________________________________________________________________________________________

        public bool HasEnoughCoins(int nCount)
        {
            int nCurrent = GetCoinsCount();
            if (nCurrent >= nCount)
            {
                return true;
            }

            return false;
        }

        //___________________________________________________________________________________________________

        public int CalculateRequiredCoinsCount(int nCount)
        {
            int nCurrent = GetCoinsCount();
            if (nCurrent - nCount > 0)
            {
                return 0;
            }

            return nCount - nCurrent;
        }

        //___________________________________________________________________________________________________

    }
}
