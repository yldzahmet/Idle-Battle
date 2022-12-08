using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIPlayer"))
        {
            other.GetComponent<ISoldierBase>().TakeDamage(damage);
        }
    }
}
