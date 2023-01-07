using System;
using UnityEngine;
using UnityEngine.AI;
public class EliteLevel3 : MonoBehaviour, ISoldierBase, IMoveble, ISoldierEvents
{
    // Fields
    private Animator animatorChar;
    private Animator animatorDino;
    private NavMeshAgent agent;
    private CapsuleCollider _collider;


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
    public BoxCollider AttackArea;

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
        _collider = transform.GetChild(0).GetComponent<CapsuleCollider>();
        animatorDino = transform.GetChild(1).GetComponent<Animator>();
        animatorChar = transform.GetChild(1).GetChild(0).GetChild(6).GetChild(2).GetComponent<Animator>();

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

        animatorChar.ResetTrigger("idle");
        animatorDino.ResetTrigger("idle");
        animatorChar.ResetTrigger("heavy attack");
        animatorDino.ResetTrigger("heavy attack");

        animatorChar.SetTrigger("walk");
        animatorDino.SetTrigger("walk");
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
        animatorChar.ResetTrigger("walk");
        animatorDino.ResetTrigger("walk");
        animatorChar.SetTrigger("heavy attack");
        animatorDino.SetTrigger("heavy attack");
        target.GetComponent<ISoldierEvents>().SoldierDeadEvent += GetComponent<SoldierController>().SearchForNewEnemy;
        agent.isStopped = true;
        Target = target;
    }
    public void TakeDamage(float damage)
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
    public void AttackBegin(float damage)
    {
        if (target.TryGetComponent(out ISoldierBase Ibase))
        {
            if (!Ibase.IsDead)
                Ibase.TakeDamage(damage * attackPower);
        }
    }
    public void Die()
    {
        if (IsDead == false)
        {
            Debug.Log(name + " Died");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<SoldierController>().currentState = State.Dead;
            gameObject.layer = 0;
            IsDead = true;
            agent.isStopped = true;
            animatorChar.SetTrigger("die");
            animatorDino.SetTrigger("die");
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
