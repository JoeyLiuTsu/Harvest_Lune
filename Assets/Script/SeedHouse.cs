using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedHouse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Worker>().PlantTime();
    }
}
