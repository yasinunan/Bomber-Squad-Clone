using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    protected const string MONEY = "Money";
    protected const string BULLET = "Bullet";

    [SerializeField] protected GameObject player;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected float damage;
    [SerializeField] protected float fireTimeRate;
    [SerializeField] protected int moneyPrefabCount;
    [SerializeField] protected float circleRadiusToDropMoney = 1.5f;
    [SerializeField] protected float duration = 0.6f;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDuration;



    protected virtual void LookAtTarget() { }
    public virtual void TakeDamage(float damage) { }
    protected virtual void Shoot() { }
    protected virtual void StopShooting() { }

}
