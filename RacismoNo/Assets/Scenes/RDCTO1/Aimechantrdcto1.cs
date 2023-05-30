using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
//important

namespace Scenes.RDCTO1
{
    public class Aimechantrdcto1 : MonoBehaviour
    {
        // Start is called before the first frame update
        public NavMeshAgent agent;

         public string sceneName; 
        public AudioSource scream;
        public GameObject player;
        public GameObject ia;
        public float distanceThreshold = 0.001f;

        
        

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = (float)8.5;
            player = GameObject.FindWithTag("Player");
        }
    

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(player.transform.position);
            float distance = Vector3.Distance(player.transform.position, ia.transform.position);
            if ( distance < distanceThreshold)
            {
                
                scream.Play();
        
                SceneManager.LoadScene(sceneName); 
            }
           
        }
       
    }
}
