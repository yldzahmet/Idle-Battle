using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experimental_EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", Random.Range(2, 10), 15);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
