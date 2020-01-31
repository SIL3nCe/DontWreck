using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crew
{
    public class CrewController : MonoBehaviour
    {
        [Tooltip("[Debug]If true the crew member can be directly moved by mouse click")]
        public bool autoAttachToWorldClicker;

        private NavMeshAgent    navMeshAgent;
        private Animator        animator;

        private int speedHash;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            speedHash = Animator.StringToHash("Speed");

            navMeshAgent.updateRotation = false;

            if (autoAttachToWorldClicker)
            {
                GameObject gameManager = GameObject.Find("GameManager");
                gameManager.GetComponent<WorldClickDestinationSetter>().AddOnClickedCallback(SetDestination);
            }
        }

        void Update()
        {
            animator.SetFloat(speedHash, navMeshAgent.velocity.normalized.magnitude);
        }

        void LateUpdate()
        {
            if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Vector3 lookPosition = navMeshAgent.velocity;
                lookPosition.y = 0.0f;

                transform.rotation = Quaternion.LookRotation(lookPosition.normalized);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }
    }
}
