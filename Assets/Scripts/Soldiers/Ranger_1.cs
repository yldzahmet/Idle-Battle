using UnityEngine;
using UnityEngine.AI;

public class Ranger_1 : MonoBehaviour, ISoldierBase
{
    public NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public SoldierSO soldier;
    public SoldierSO Soldier { get => soldier; set => _ = soldier; }
    public float Range { get => soldier.range; set => _ = soldier.range; }
    public float Health { get => soldier.health; set => _ = soldier.health; }
    public float AttackSpeed { get => soldier.attackSpeed; set => _ = soldier.attackSpeed; }
    public float AttackPower { get => soldier.attackPower; set => _ = soldier.attackPower; }
    public float WalkSpeed { get => soldier.walkSpeed; set => _ = soldier.walkSpeed; }

    public void Attack(GameObject target)
    {
        Debug.Log("Attaced");
    }

    public GameObject CheckRange()
    {
        Debug.Log("CheckRange");
        return null;
    }

    public void Die()
    {
        Debug.Log("Died");
    }

    public void GoDestination(Vector3 destPosition)
    {
        Debug.Log("GoDestination");
        agent.SetDestination(destPosition);
        animator.SetTrigger("walk");
        if (agent.remainingDistance < 1)
            animator.SetTrigger("idle");
    }

    public void TakeDamege(int damage)
    {
        Debug.Log("TakenDamege");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
