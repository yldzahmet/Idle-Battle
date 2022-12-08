using System.Collections;
using System;
using UnityEngine;

public enum EquipmentType { Weapon, Armor, Health}

public class CreateEquipments : MonoBehaviour
{
    public Action OnPlayerEntered;
    public Action OnPlayerExited;

    public StatsSO playerStats;
    public EquipmentType equipmentType;
    internal bool startToCreate = false;

    private void OnEnable()
    {
        OnPlayerEntered += CreateEquipment;
        OnPlayerExited += FinishCreatingEquipment;
    }

    private void OnDisable()
    {
        OnPlayerEntered -= CreateEquipment;
        OnPlayerExited -= FinishCreatingEquipment;
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if((tag == "Player" || tag == "AIPlayer") && OnPlayerEntered != null)
        {
            OnPlayerEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(OnPlayerExited != null)
            OnPlayerExited();
    }

    public void CreateEquipment()
    {
        startToCreate = true;
        StartCoroutine(CreateEquipment_Delayed());
    }

    public void FinishCreatingEquipment()
    {
        startToCreate = false;
        StopCoroutine(CreateEquipment_Delayed());
    }

    IEnumerator CreateEquipment_Delayed()
    {
        do
        {
            switch (equipmentType)
            {
                case EquipmentType.Weapon:
                    playerStats.IncreaseWeapon(1, 1);
                    break;
                case EquipmentType.Armor:
                    playerStats.IncreaseArmor(1, 1);
                    break;
                case EquipmentType.Health:
                    playerStats.IncreaseHealth(1, 1);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1);
        } while (startToCreate);
    }
}
