using System;
using UnityEngine;
using UnityEngine.AI;

public class RangerLevel1 : MonoBehaviour, ISoldierBase, IMoveble, ISoldierEvents
{
    // Fields
    private Animator animator;
    private CapsuleCollider _collider;
    private NavMeshAgent agent;


    // Interface capsule fields
    public SoldierSO soldier;
    private GameObject target;
    private float range;
    [SerializeField] private float health;
    private float attackSpeed;
    private float attackPower;
    private float walkSpeed;
    private bool isRunning;
    private bool isAttacking;
    private bool isDead = false;
    private Vector3 destination;

    public event Action SoldierDeadEvent;

    // Custom Fields
    public GameObject Rock;
    public Transform HandPosition;


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
        Target = destObject;
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
    public void TakeDamage(float damage)
    {
        if (damage >= health) {
            health = 0;
            Die();
        }
        else
        {
            health -= damage;
            print(name +"'s health : "+ health);
        }
    }
    public void AttackBegin(float damage)
    {
        if (target.TryGetComponent(out ISoldierBase Ibase))
        {
            if (!Ibase.IsDead)
                ThrowRock();
        }
    }
    public void Die()
    {
        if (IsDead == false)
        {
            Debug.Log(name + " Died");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<SoldierController>().currentState = State.Dead;
            IsDead = true;
            agent.isStopped = true;
            gameObject.layer = 0;
            animator.SetTrigger("die");
            Destroy(gameObject, 2f);
            if (SoldierDeadEvent != null)
                SoldierDeadEvent();
            SoldierDeadEvent = null;
        }
    }
    public void EnterIdleState()
    {
        GetComponent<SoldierController>().currentState = State.Idle;
        animator.SetTrigger("idle");
    }

    /// <summary>
    /// Custom Methods
    /// </summary>
    public void ThrowRock()
    {
        GameObject projectile = Instantiate(Rock, HandPosition.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce((Target.transform.position + new Vector3(0,3   ,0) - HandPosition.position) * 100f);
        Destroy(rb.gameObject, 2f);
    }

    
}
