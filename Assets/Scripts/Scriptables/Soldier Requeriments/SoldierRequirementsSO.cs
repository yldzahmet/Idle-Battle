using UnityEngine;
[CreateAssetMenu]
public class SoldierRequirementsSO : ScriptableObject
{
    public new string name;
    public int age;
    public int weapCost;
    public int armorCost;
    public int healthCost;
    public GameObject soldierPrefab;

}
