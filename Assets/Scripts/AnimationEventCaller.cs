using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCaller : MonoBehaviour
{
    private ISoldierBase soldierBase;
    public float damage;
    private void Awake()
    {
        soldierBase = GetComponentInParent<ISoldierBase>();
    }
    public void AttackEventCallback(float damage)
    {
        if (soldierBase != null)
            soldierBase.AttackBegin(damage);
    }
}
