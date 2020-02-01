using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FireGenerator : MonoBehaviour
{
    public int FireNumber;
    public float Radius;

    public GameObject FireFX;

    // Start is called before the first frame update
    void Start()
    {
        int remaining = FireNumber;
        while (remaining != 0)
        {
            NavMeshHit originHit;
            NavMesh.SamplePosition(gameObject.transform.position, out originHit, 3.0f, NavMesh.AllAreas);
            GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            prim.transform.position = originHit.position;

            Vector3 randLocation = Random.insideUnitSphere * Radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randLocation, out hit, Radius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                prim2.transform.position = finalPosition;
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(originHit.position, finalPosition, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        for (int i = 0; i < path.corners.Length - 1; ++i)
                        {
                            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.white, 500.0f);
                        }
                        Instantiate(FireFX, finalPosition, Quaternion.identity);
                        remaining--;
                    }
                }
            }
        }
    }
}
