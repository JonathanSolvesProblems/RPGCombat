using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;

        Fighter fighter;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
        }

        public void Stop()
        {
            agent.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            fighter.Cancel(); // cancelling fighting before even starting to move each time.
            MoveTo(destination);
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