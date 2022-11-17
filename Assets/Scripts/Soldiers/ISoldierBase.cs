using UnityEngine;

public interface ISoldierBase
{
    public SoldierSO Soldier { get; set; }
    public float Range { get; set; }
    public float Health { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackPower { get; set; }
    public float WalkSpeed { get; set; }
    public GameObject CheckRange();
    public void GoDestination(Vector3 destPosition);
    public void Attack(GameObject target);
    public void TakeDamege(int damage);
    public void Die();
}
