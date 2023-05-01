using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;


namespace HEYGames
{
    public class HapticsHelper : MonoBehaviour
    {
        //===============================================================================================

        public static void VibrateHandheld()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                Handheld.Vibrate();
            }
#endif
        }

        //===============================================================================================

        public static void CollectibleGrabbed()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
            }
#endif
        }

        //===============================================================================================

        public static void ShortLightPulse()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
            }
#endif
        }

        //=================================
        public static void MidLightPulse()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
#endif
        }
        
        public static void StartContinuousHaptic()
        {
#if !UNITY_EDITOR
            MMVibrationManager.ContinuousHaptic(0.3f, 0f, 600f);
#endif
        }
        public static void StopContinous()
        {
#if !UNITY_EDITOR
       
            MMVibrationManager.StopContinuousHaptic();
#endif
        }

        //===============================================================================================

        public static void ShortStrongPulse()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);  // eski hali mediumImpact idi
            }
#endif
        }

        //===============================================================================================

        public static void LevelFail()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
            }
#endif
        }

        //===============================================================================================

        public static void LevelSuccess()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.Success);
            }
#endif
        }

        //===============================================================================================

        public static void StrongPulse()
        {
#if !UNITY_EDITOR
            if (GameSettingsManager.Instance.GetVibrationIsEnabled() > 0)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            }
#endif
        }

        //===============================================================================================

    }
}