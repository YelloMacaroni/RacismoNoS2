using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//important

namespace Scenes.RDCTO1
{
    public class Aimechantrdcto1 : MonoBehaviour
    {
        // Start is called before the first frame update
        public NavMeshAgent agent;


        public GameObject player;

        
        IEnumerator Waitforsec()
        {
            yield return new WaitForSeconds(5);

        }

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = (float)3.5;
            StartCoroutine("Waitforsec");
            player = GameObject.FindWithTag("Player");
        }
    

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
