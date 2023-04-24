using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace YU.Template
{
    public class PlayerStats : Singleton<PlayerStats>
    {
        //___________________________________________________________________________________________________


        [Header("Player Stats")]

        [SerializeField] private int nHighscore;
        [SerializeField] private int nMaxFinishedLevel;
        [SerializeField] private int nLastFinishedLevel;


        //___________________________________________________________________________________________________


        private void Start()
        {
            UpdateDebugInfo();
        }

        //___________________________________________________________________________________________________


        public void UpdateDebugInfo()
        {
            nMaxFinishedLevel = GetMaxFinishedLevel();
            nLastFinishedLevel = GetLastFinishedLevel();
        }





        //___________________________________________________________________________________________________

        //
        // LEVEL STATS
        //
        //___________________________________________________________________________________________________


        public void SetMaxFinishedLevel(int nVal)
        {
            //int nCurMax = GetMaxFinishedLevel();
            //if (nVal > nCurMax)
            {
                PlayerPrefs.SetInt("Stats_MaxFinishedLevel", nVal);
            }
        }

        //___________________________________________________________________________________________________


        public int GetMaxFinishedLevel()
        {
            int nRet = PlayerPrefs.GetInt("Stats_MaxFinishedLevel", 0);

            return nRet;
        }

        //___________________________________________________________________________________________________


        public void SetLastFinishedLevel(int nVal)
        {
            PlayerPrefs.SetInt("Stats_LastFinishedLevel", nVal);
        }

        //___________________________________________________________________________________________________


        public int GetLastFinishedLevel()
        {
            int nRet = PlayerPrefs.GetInt("Stats_LastFinishedLevel", 0);

            return nRet;
        }


        //___________________________________________________________________________________________________

        
        
        
    }
}

