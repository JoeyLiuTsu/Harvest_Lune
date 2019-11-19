using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject worker;
    bool spawnCheck;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(worker, transform.position, Quaternion.identity);
        }
        
    }

    
    void Update()
    {
        if (!spawnCheck)
        {
            StartCoroutine(Spawner());
        }
       
    }

    IEnumerator Spawner()
    {
        spawnCheck = true;
        Instantiate(worker, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        spawnCheck = false;
    }
}
