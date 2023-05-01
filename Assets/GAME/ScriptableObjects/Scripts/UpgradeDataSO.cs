using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData")]
public class UpgradeDataSO : ScriptableObject
{
    public int maxUpgradeCount;
    public int nextUpgradePriceIncreaseAmount;
    public int bombCapacityIncreaseAmount;
    public float damageIncreaseAmount;
    public float armorIncreaseAmount;

    public int initialBombCapacity;
    public float initialDamage;
    public float initialArmor;


}
