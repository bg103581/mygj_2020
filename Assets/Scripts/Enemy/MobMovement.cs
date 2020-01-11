using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMovement : MonoBehaviour {
    private Transform target;//Drag&drop the object to reach
    private NavMeshAgent agent;
    // Use this for initialization
    void Start() {
        //Initializes the agent with the very same script that is attached to the game object.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    private void move() {
        if (target != null) { //Clean coding
            Debug.DrawLine(transform.position, target.transform.position, Color.blue);
            agent.SetDestination(target.transform.position);
        }

    }
}
