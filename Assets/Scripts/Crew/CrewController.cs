using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crew
{
    public class CrewController : MonoBehaviour
    {
        public enum AnimationType
        {
            E_INTERACTING,
            E_REPAIRING,
            E_PIPIYING,
            E_ATTACKING,
            E_NONE
        }

        [Tooltip("[Debug] If true the crew member can be directly moved by mouse click")]
        public bool m_autoAttachToWorldClicker;

        private NavMeshAgent    m_navMeshAgent;
        private Animator        m_animator;

        private int             m_speedHash;
        private int             m_interactHash;
        private int             m_repairHash;
        private int             m_pipiHash;
        private int             m_attackHash;

        private AnimationType   m_currentAnimation;

		private void Awake()
		{
			//
			//
			m_navMeshAgent = GetComponent<NavMeshAgent>();
			m_animator = GetComponent<Animator>();

            m_currentAnimation = AnimationType.E_NONE;
		}

		void Start()
        {
            m_speedHash = Animator.StringToHash("Speed");
            m_interactHash = Animator.StringToHash("Interact");
            m_repairHash = Animator.StringToHash("Repair");
            m_pipiHash = Animator.StringToHash("Extenguish");
            m_attackHash = Animator.StringToHash("Attack");

            m_navMeshAgent.updateRotation = false;

            if (m_autoAttachToWorldClicker)
            {
                GameObject gameManager = GameObject.Find("GameManager");
                gameManager.GetComponent<WorldClickDestinationSetter>().AddOnClickedCallback((Vector3 clickedPos, GameObject clickedObject) => SetDestination(clickedPos));
            }
        }

        void Update()
        {
            m_animator.SetFloat(m_speedHash, m_navMeshAgent.velocity.normalized.magnitude);
        }

        void LateUpdate()
        {
            // Instant rotate
            if (m_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Vector3 lookPosition = m_navMeshAgent.velocity;
                lookPosition.y = 0.0f;

                transform.rotation = Quaternion.LookRotation(lookPosition.normalized);
            }
        }

        public void ClearPath()
        {
            if (m_navMeshAgent.enabled)
            {
                m_navMeshAgent.ResetPath();
            }
        }

        public void SetDestination(Vector3 destination)
        {
            if (m_navMeshAgent.enabled)
            {
                m_navMeshAgent.destination = destination;
            }
        }

        public Vector3 GetDestination()
        {
            return m_navMeshAgent.destination;
        }

        public float GetDistanceToDestination()
        {
            return m_navMeshAgent.remainingDistance;
        }

        public float GetStoppingDistance()
        {
            return m_navMeshAgent.stoppingDistance;
        }

        public void SetAnimation(AnimationType animationType)
        {
            if (animationType != m_currentAnimation)
            {
                SetAnimation(m_currentAnimation, false);
            }

            SetAnimation(animationType, true);

            m_currentAnimation = animationType;
        }

        private void SetAnimation(AnimationType animationType, bool activate)
        {
            switch(animationType)
            {
                case AnimationType.E_INTERACTING:
                {
                    m_animator.SetBool(m_interactHash, activate);
                }
                break;

                case AnimationType.E_REPAIRING:
                {
                    m_animator.SetBool(m_repairHash, activate);
                }
                break;

                case AnimationType.E_PIPIYING:
                {
                    m_animator.SetBool(m_pipiHash, activate);
                }
                break;

                case AnimationType.E_ATTACKING:
                {
                    m_animator.SetBool(m_attackHash, activate);
                }
                break;
            }
        }

        public void OnDrawGizmos()
		{
			if (m_navMeshAgent != null)
			{
				if (m_navMeshAgent.destination != null)
				{
					Gizmos.DrawWireSphere(m_navMeshAgent.destination, 0.5f);
				}
			}
		}
        
    }
}
