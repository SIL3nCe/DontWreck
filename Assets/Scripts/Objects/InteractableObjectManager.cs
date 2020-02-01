using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObjectManager : MonoBehaviour
{
    public List<Objects.InteractableObject> InteractableObjectList = new List<Objects.InteractableObject>();

    public Objects.InteractableObject GetNearestAvailableOBject(Vector3 vLocation)
    {
        Objects.InteractableObject nearestObject = null;
        float fCurrDist = float.MaxValue;
        foreach (Objects.InteractableObject interactableObject in InteractableObjectList)
        {
            if (interactableObject.HasFreePlacementPoint())
            {
                Vector3 vObjectLocation = interactableObject.transform.position;

                NavMeshPath navPath = new NavMeshPath();
                NavMesh.CalculatePath(vLocation, vObjectLocation, NavMesh.AllAreas, navPath);

                float fRes = GetPathLength(navPath);
                if (fRes < fCurrDist)
                {
                    nearestObject = interactableObject;
                    fCurrDist = fRes;
                }
            }
        }

        return nearestObject;
    }

    private float GetPathLength(NavMeshPath path)
    {
        if ((path.status == NavMeshPathStatus.PathInvalid) || (path.corners.Length <= 1))
        {
            return float.MaxValue;
        }

        float lng = 0.0f;
        for (int i = 1; i < path.corners.Length; ++i)
        {
            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return lng;
    }
}
