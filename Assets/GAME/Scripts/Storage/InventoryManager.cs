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
