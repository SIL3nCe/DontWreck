using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crew
{
    public class CrewController : MonoBehaviour
    {
        private NavMeshAgent    navMeshAgent;
        private Animator        animator;

        private int speedHash;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            speedHash = Animator.StringToHash("Speed");

            navMeshAgent.updateRotation = false;
        }

        void Update()
        {
            animator.SetFloat(speedHash, navMeshAgent.velocity.normalized.magnitude);
        }

        void LateUpdate()
        {
            if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }
    }
}
