using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SoldierRequerimentsSO : ScriptableObject
{
    public new string name;
    public int age;
    public int weapCost;
    public int armorCost;
    public int healthCost;

    public GameObject soldier;

}
