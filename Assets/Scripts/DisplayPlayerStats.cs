using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerStats : MonoBehaviour
{
    public StatsSO playerStats;

    public TextMeshProUGUI weapon;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI health;
    public TextMeshProUGUI money;
    public TextMeshProUGUI rawMetarials;

    // assing with ending spaces
    public string weapon_str;
    public string armor_str;
    public string health_str;
    public string money_str;
    public string rawMetarials_str;

    private void Update()
    {
        weapon.text = weapon_str + playerStats.weapon.ToString();
        armor.text = armor_str + playerStats.armor.ToString();
        health.text = health_str + playerStats.health.ToString();
        money.text = money_str + playerStats.money.ToString();
        rawMetarials.text = rawMetarials_str + playerStats.rawMetarials.ToString();
    }
}
