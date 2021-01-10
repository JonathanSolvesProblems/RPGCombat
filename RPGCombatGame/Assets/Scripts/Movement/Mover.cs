using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
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
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
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

}