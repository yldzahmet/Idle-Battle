using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatsSO : ScriptableObject
{
    //[NonSerialized]     
    public int rawMetarials = 0;
    //[NonSerialized]
    public int health = 0;
    //[NonSerialized]
    public int weapon = 0;
    //[NonSerialized]
    public int armor = 0;
    //[NonSerialized]
    public int money = 0;

    public void IncreaseMetarials(int c)
    {
        rawMetarials += c;
        Debug.Log("Raw Mets: " + rawMetarials);
    }
    public void IncreaseHealth(int amount, int cost)
    {
        if (cost <= rawMetarials)
        {
            rawMetarials -= cost;
            health += amount;
            Debug.Log("Health: " + health);
        }
    }
    public void IncreaseWeapon(int amount, int cost)
    {
        if(cost <= rawMetarials)
        {
            rawMetarials -= cost;
            weapon += amount;
            Debug.Log("Weapon: " + weapon);
        }
    }
    public void IncreaseArmor(int amount, int cost)
    {
        if (cost <= rawMetarials)
        {
            rawMetarials -= cost;
            armor += amount;
            Debug.Log("Armor: " + armor);
        }
    }


    
}
