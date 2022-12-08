using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoldierSO : ScriptableObject
{
    [HideInInspector] public enum SoldierType { Ranger1, Ranger2, Warrior1, Warrior2, Elite, Special }
    public SoldierType soldierType;
    public bool isRunning;
    public bool isAttacking;
    public float range;
    public float health;
    public float attackSpeed;
    public float attackPower;
    public float walkSpeed;
}
