using System;
using UnityEngine;
using UnityEngine.AI;

public class WarriorLevel1 : MonoBehaviour, ISoldierBase, IMoveble, ISoldierEvents
{
    // Fields
    private Animator animator;
    private NavMeshAgent agent;
    private CapsuleCollider _collider;


    // Interface capsule fields
    public SoldierSO soldier;
    private GameObject target;
    private float range;
    private float health;
    private float attackSpeed;
    private float attackPower;
    private float walkSpeed;
    private bool isRunning;
    private bool isAttacking;
    private bool isDead = false;
    private Vector3 destination;

    public event Action SoldierDeadEvent;

    // Custom Fields

    public SoldierSO Soldier { get => soldier; set => soldier = value; }
    public GameObject Target { get => target; set => target = value; }

    public float Range { get => range; set => range = value; }
    public float Health { get => health; set => health = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }
    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    public Vector3 Destination { get { return destination; } set { destination = value; } }
    public bool IsDead { get => isDead; set => isDead = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        _collider = transform.GetChild(0).GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        Range = soldier.range;
        Health = soldier.health;
        AttackSpeed = soldier.attackSpeed;
        AttackPower = soldier.attackPower;
        WalkSpeed = soldier.walkSpeed;
        IsRunning = soldier.isRunning;
        isAttacking = soldier.isAttacking;
        _collider.radius = Range;
    }

    public void GoDestination(GameObject destObject)
    {
        Debug.Log("GoDestination to " + destObject.name);
        animator.ResetTrigger("heavy attack");
        animator.SetTrigger("walk");
        this.target = destObject;
        agent.SetDestination(destObject.transform.position);
        agent.isStopped = false;
    }
    public bool CheckReachedDestination(Vector3 destPosition)
    {
        if (Vector3.Distance(transform.position, destPosition) < 1f)
            return true;
        else
            return false;
    }

    public GameObject GetClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range, GetComponent<SoldierController>().enemyLayer);
        int indexOfTarget = 0;
        float distanceOfTarget = Mathf.Infinity;
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider hitCollider = hitColliders[i];
                float hitDistance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (hitDistance <= distanceOfTarget)
                {
                    distanceOfTarget = hitDistance;
                    indexOfTarget = i;
                }
            }
            return hitColliders[indexOfTarget].gameObject;
        }
        else
            return null;
    }
    public void Attack(GameObject target)
    {
        Debug.Log(name + " Attacked to " + target.name);
        animator.ResetTrigger("walk");
        animator.SetTrigger("heavy attack");
        target.GetComponent<ISoldierEvents>().SoldierDeadEvent += GetComponent<SoldierController>().SearchForNewEnemy;
        agent.isStopped = true;
        Target = target;
    }
    public void TakeDamage(int damage)
    {
        if (damage >= health)
        {
            health = 0;
            Die();
        }
        else
        {
            health -= damage;
            print(name + "'s health : " + health);
        }
    }
    public void ProjectileReleased()
    {
        throw new NotImplementedException();
    }
    public void Die()
    {
        if (IsDead == false)
        {
            Debug.Log(name + " Died");
            gameObject.layer = 0;
            GetComponent<SoldierController>().currentState = State.Dead;
            IsDead = true;
            animator.SetTrigger("die");
            Destroy(gameObject, 2f);
            if (SoldierDeadEvent != null)
                SoldierDeadEvent();
            SoldierDeadEvent = null;
        }
    }
    public void EnterIdleState()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Custom Methods
    /// </summary>
    
}
