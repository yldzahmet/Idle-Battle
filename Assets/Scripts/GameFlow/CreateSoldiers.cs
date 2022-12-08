using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateSoldiers : MonoBehaviour
{
    public Action SoldierCreated;

    public StatsSO playerStats;
    public SoldierRequirementsSO soldierRequirement;
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if(tag == "Player" || tag == "AIPlayer")
        {
            if (CheckIsPlayerCapableToCreate(soldierRequirement.weapCost, soldierRequirement.armorCost, soldierRequirement.healthCost))
            {
                CreateSoldier();
            }
        }
    }

    public void CreateSoldier()
    {
        Debug.Log("Soldider Created");
        Instantiate(soldierRequirement.soldierPrefab, transform.position, Quaternion.identity);
    }

    public bool CheckIsPlayerCapableToCreate(int weapon, int armor, int health)
    {

        if (playerStats.weapon >= weapon 
            && playerStats.armor > armor
            && playerStats.health > health)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
