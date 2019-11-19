using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public GameObject seed;
    public GameObject headSeed;
    public GameObject sleepEffect;
    private GameObject seedHouse;
    public GameObject bed;
    public GameObject plane;
    bool isWorking;
    bool isSleeping;
    public enum WorkerStates { RetreiveSeed, Planting, ReturningHome, Sleep }
    public WorkerStates currentState;
    private void Start()
    {
        seedHouse = GameObject.Find("SeedHousu");
        bed = GameObject.Find("Bed");
        plane = GameObject.Find("FarmLand");
        currentState = WorkerStates.RetreiveSeed;
    }
    void Update()
    {
        var agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(RandomPointOnFarm(plane.transform.position,plane.transform.localScale));


        if (currentState == WorkerStates.RetreiveSeed)
        {
            agent.SetDestination(seedHouse.transform.position);
            
        }

        if (currentState == WorkerStates.Planting)
        {
            if (isWorking == false)
            {
                StartCoroutine(ToPlant());
                headSeed.gameObject.SetActive(true);
                isWorking = true;
            }
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        Instantiate(seed, transform.position, Quaternion.identity);
                        headSeed.gameObject.SetActive(false);
                        isWorking = false;
                        currentState = WorkerStates.ReturningHome;
                    }
                }
            }
        }

        if (currentState == WorkerStates.ReturningHome)
        {
            agent.SetDestination(bed.transform.position);
        }
        if (currentState == WorkerStates.Sleep)
        {
            if (isSleeping == false)
            {
                isSleeping = true;
                StartCoroutine(NightNight());
            }
        }
    }

    private static Vector3 RandomPointOnFarm(Vector3 center, Vector3 size)
    { 
        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    public void PlantTime()
    {
        currentState = WorkerStates.Planting;
    }

    public void SleepTime()
    {
        if (currentState == WorkerStates.ReturningHome)
        {
            currentState = WorkerStates.Sleep;
        }
        
    }
    
    IEnumerator ToPlant()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(RandomPointOnFarm(plane.transform.position, plane.transform.localScale));
        yield return null;
    }

    IEnumerator NightNight()
    {
        sleepEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        sleepEffect.gameObject.SetActive(false);
        isSleeping = false;
        currentState = WorkerStates.RetreiveSeed;
    }
}

