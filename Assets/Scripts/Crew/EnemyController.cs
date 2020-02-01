using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Objects.InteractableObject currentTarget;

    private NavMeshAgent NavMeshAgent;

    // Update is called once per frame
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
                    if (!currentTarget.GetPlacementPoint(gameObject, out vLocation))
                    {
                        NavMeshAgent.destination = vLocation;
                    }
                }
                else
                {
                    // Attack nearest NPC
                    // TODO
                }
            }
        }
    }
}
