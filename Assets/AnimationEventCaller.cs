using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCaller : MonoBehaviour
{
    private ISoldierBase soldierBase;
    private void Awake()
    {
        soldierBase = GetComponentInParent<ISoldierBase>();
    }
    public void ThrowRockEventCallback()
    {
        if (soldierBase != null)
            soldierBase.ProjectileReleased();
    }
}
