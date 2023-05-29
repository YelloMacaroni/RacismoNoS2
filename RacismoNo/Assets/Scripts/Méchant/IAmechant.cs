using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; //important


public class IAmechant : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    [SerializeField]
    public Transform cam;
    [SerializeField] private bool active = false;
    [SerializeField] public float agrodDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(agent.transform.position, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
            
        SetAgro();
        
        


    }

    bool RandomPoint(Vector3 center,  out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    bool SetAgro()
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position,cam.TransformDirection(Vector3.forward),out hit,agrodDistance);
        if (active && hit.transform.CompareTag("Player"))
        {
            agent.speed = 4;
            agent.SetDestination(hit.transform.position);
            return true;
        }
        agent.speed = (float)3.5;
        return false;

    }

    
}
