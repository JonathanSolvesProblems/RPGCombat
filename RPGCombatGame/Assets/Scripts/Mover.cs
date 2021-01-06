using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit); // getting position info where the raycast hit. - Raycast method bool

        if (hasHit)
        {
            agent.destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        // Get the global velocity from Nav Mesh Agent
        Vector3 velocity = agent.velocity;

        // Convert this into a local value relative to the character
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // Set the Animator's blend value to be equal to our desired forward speed (on the Z axis)
        float speed = localVelocity.z;

        anim.SetFloat("forwardSpeed", speed);
        
    }
}
