using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    [SerializeField]
    private GameObject destination;
    

    private void Start()
    {
        GetComponent<ISoldierBase>().GoDestination(destination.transform.position);
    }

}
