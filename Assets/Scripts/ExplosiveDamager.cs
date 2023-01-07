using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDamager : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float damage;
    public float radius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIPlayer") || other.CompareTag("HPlayer"))
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, radius, enemyLayer);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<ISoldierBase>().TakeDamage(damage);
            }
        }
    }
}
