using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


namespace YU.Template
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        //===============================================================================================

        [Header("User Inventory Non-Consumables")]

        [SerializeField] private int nCoinsCount;


        //===================================================================================

        private void Start()
        {
            UpdateDebugInfo();
        }

        //===================================================================================

        public void UpdateDebugInfo()
        {

            nCoinsCount = GetCoinsCount();
        }



        //===================================================================================
        //
        // COINS
        //
        //===================================================================================

        public int GetCoinsCount()
        {
            int nRet = PlayerPrefs.GetInt("CoinsCount", 0);

            return nRet;
        }

        //===================================================================================

        public void SetCoinsCount(int nCount)
        {
            PlayerPrefs.SetInt("CoinsCount", nCount);

            UpdateDebugInfo();


        }

        //===================================================================================

        public void IncreaseCoinsCount(int nCount = 1)
        {
            PlayerPrefs.SetInt("CoinsCount", GetCoinsCount() + nCount);

            UpdateDebugInfo();
        }

        //===================================================================================

        public void DecreaseCoinsCount(int nCount = 1)
        {
            PlayerPrefs.SetInt("CoinsCount", GetCoinsCount() - nCount);

            UpdateDebugInfo();
        }

        //===================================================================================

        public bool HasEnoughCoins(int nCount)
        {
            int nCurrent = GetCoinsCount();
            if (nCurrent >= nCount)
            {
                return true;
            }

            return false;
        }

        //===================================================================================

        public int CalculateRequiredCoinsCount(int nCount)
        {
            int nCurrent = GetCoinsCount();
            if (nCurrent - nCount > 0)
            {
                return 0;
            }

            return nCount - nCurrent;
        }

        //===================================================================================
    }
}
