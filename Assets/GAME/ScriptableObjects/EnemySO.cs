using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
   public float maxHealth;
   public float damage;
   public float fireTimeRate;
   public int moneyPrefabCount;
   public float attackRange;

}
