using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Objects.InteractableObject currentTarget;

    private NavMeshAgent m_navMeshAgent;
    private Animator m_animator;

    private int m_speedHash;

    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();

        m_speedHash = Animator.StringToHash("Speed");
    }

    void Update()
    {
        if (!currentTarget)
        {
            // Select target
            InteractableObjectManager manager = GameManager.m_instance.GetComponent<InteractableObjectManager>();
            if (manager)
            {
                Vector3 vLocation;
                currentTarget = manager.GetNearestAvailableOBject(gameObject.transform.position);
                if (currentTarget)
                {
                    // Attack object
                    if (currentTarget.GetPlacementPoint(gameObject, out vLocation))
                    {
                        GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        prim2.transform.position = vLocation;
                        m_navMeshAgent.destination = vLocation;
                    }
                }
                else
                {
                    // Attack nearest NPC
                    // TODO
                }
            }
        }
        else
        {
            if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
                float fVal = Mathf.Min(2.0f * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, fVal);
            }
        }

        m_animator.SetFloat(m_speedHash, m_navMeshAgent.velocity.normalized.magnitude);
    }
}
