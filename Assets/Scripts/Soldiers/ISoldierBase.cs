using UnityEngine;
using System;

public interface ISoldierBase
{
    public SoldierSO Soldier { get; set; }
    public GameObject Target { get; set; }
    public bool IsRunning{ get; set; }
    public bool IsAttacking { get; set; }
    public float Range { get; set; }
    public float Health { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackPower { get; set; }
    public float WalkSpeed { get; set; }
    public bool IsDead { get ; set ; }

    public GameObject GetClosestEnemy();
    public void Attack(GameObject target);
    public void TakeDamage(int damage);
    public void ProjectileReleased();
    public void Die();
    public void EnterIdleState();
}

public interface IMoveble
{
    public Vector3 Destination { get; set; }
    float Range { get; set; }

    public void GoDestination(GameObject destObject);
    public bool CheckReachedDestination(Vector3 destPosition);
}

public interface ISoldierEvents
{
    public event Action SoldierDeadEvent;
}