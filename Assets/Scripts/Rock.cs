using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIPlayer") || other.CompareTag("HPlayer"))
        {
            other.GetComponent<ISoldierBase>().TakeDamage(damage);
        }
    }
}
