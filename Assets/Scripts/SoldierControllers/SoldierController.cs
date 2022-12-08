using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public enum State { Running, Attacking, Idle, Dead};

public class SoldierController : MonoBehaviour
{
    public bool willPerformAttack = true;
    public bool willMoveDestination = true;
    internal State currentState;
    [SerializeField] internal GameObject target;
    [SerializeField] private GameObject startDestination;
    public string startDestinationTag;
    public List<string> enemyTags;
    public LayerMask enemyLayer;


    private void Awake()
    {
        Application.targetFrameRate = 180;
        startDestination = GameObject.FindGameObjectWithTag(startDestinationTag);
    }

    private void Start()
    {
        currentState = State.Idle;
        GoDefaultDestination();
    }

    public void GoDefaultDestination() {
        target = startDestination;
        if (target && willMoveDestination)
        {
            IMoveble moveble = GetComponent<IMoveble>();
            if (moveble != null)
                moveble.GoDestination(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (willPerformAttack)
        {
            if(currentState != State.Dead && currentState != State.Attacking) { 
            
                string tag = other.tag;

                if (enemyTags.Contains(tag))
                {
                    target = other.gameObject;
                    currentState = State.Attacking;
                    GetComponent<ISoldierBase>().Attack(target);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (enemyTags.Contains(tag) && currentState != State.Attacking)
        {
            Debug.Log(other.gameObject + " Exit from range ");
            SearchForNewEnemy();
        }
    }

    public void SearchForNewEnemy()
    {
        print("SearchForNewEnemy");
        GameObject newTarget = GetComponent<ISoldierBase>().GetClosestEnemy();

        if (newTarget)
                Debug.Log("GetClosestEnemy is " + newTarget.name);
        else
            Debug.Log("GetClosestEnemy is null ");

        if (newTarget != null)
        {
            currentState = State.Attacking;
            target = newTarget;
            GetComponent<ISoldierBase>().Attack(target);
        }
        else
        {
            currentState = State.Running;
            GoDefaultDestination();
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Running:
                IMoveble moveble = GetComponent<IMoveble>();
                if (moveble != null)
                {
                    moveble.CheckReachedDestination(target.transform.position);
                    currentState = State.Idle;
                }
                break;
            case State.Attacking:
                if (target)
                {
                    Vector3 direction = target.transform.position - transform.position;
                    direction.y = 0;
                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 360 * Time.deltaTime);
                }
                else
                {
                    Debug.Log("target null ");
                    currentState = State.Running;
                    GoDefaultDestination();
                }
                break;
            case State.Idle:
                break;
        }
    }
}
