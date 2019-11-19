using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrowth : MonoBehaviour
{
    public GameObject plantPrefab;
    private Vector3 point;

    private void Start()
    {
        StartCoroutine(GrowPlant());
    }
    IEnumerator GrowPlant()
    {
        point = transform.position;
        point.y += .8f;
        yield return new WaitForSeconds(10);
        Instantiate(plantPrefab, point, Quaternion.identity);
    }
}
