using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateRaws : MonoBehaviour
{
    public float startCoolDown;
    public StatsSO playerStats;
    public TextMeshProUGUI metarialsText;

    [SerializeField]
    private int rawMetarials = 0;
    [SerializeField]
    private float currentCoolDown = 0;
    [SerializeField]
    private bool generating = true;

    private void OnTriggerStay(Collider other)
    {
        string tag = other.tag;
        if (rawMetarials > 0)
        {
            if (tag == "AIPlayer" || tag == "Player")
            {
                playerStats.IncreaseMetarials(rawMetarials);
                rawMetarials = 0;
            }

        }
    }

    IEnumerator StartCreatingRaws()
    {
        while (currentCoolDown > 0 && generating)
        {
            if( currentCoolDown > 0)
            {
                yield return new WaitForSeconds(1);
                currentCoolDown -= 1;
                //Debug.Log("currentCoolDown" + currentCoolDown);
                if ( currentCoolDown <= 0)
                {
                    rawMetarials += 1;
                    currentCoolDown = startCoolDown;
                }
            }

        }
    }

    private void OnEnable()
    {
        currentCoolDown = startCoolDown;
        StartCoroutine(StartCreatingRaws());
    }
    private void OnDisable()
    {
        StopCoroutine(StartCreatingRaws());
    }

    private void Update()
    {
        metarialsText.text = rawMetarials.ToString();
    }
}
