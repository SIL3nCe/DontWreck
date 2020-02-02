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
        Vector3 debug = new Vector3();
        foreach (Objects.InteractableObject interactableObject in InteractableObjectList)
        {
            Vector3 vPointLocation;
            if (interactableObject.HasFreePlacementPoint(out vPointLocation) && interactableObject.m_hp > 0)
            {
                NavMeshPath navPath = new NavMeshPath();
                NavMesh.CalculatePath(vLocation, vPointLocation, NavMesh.AllAreas, navPath);

                //GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //prim2.transform.position = vPointLocation;
                
                float fRes = GetPathLength(navPath);
                if (fRes < fCurrDist)
                {
                    debug = vLocation;
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
