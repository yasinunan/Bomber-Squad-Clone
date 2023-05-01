using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public int moneyIncreaseAmount;
    public float maxHealth;
}
