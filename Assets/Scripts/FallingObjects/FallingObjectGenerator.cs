using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FallingObjectGenerator : MonoBehaviour
{
    public float Radius;

    public GameObject FallingObjectPrefab;

    void Start()
    {
        int remaining = 0;
        for (int i = 0; i < remaining; ++i)
        {
            NavMeshHit originHit;
            NavMesh.SamplePosition(gameObject.transform.position, out originHit, 3.0f, NavMesh.AllAreas);

            //GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //prim.transform.position = originHit.position;

            Vector3 randLocation = Random.insideUnitSphere * Radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randLocation, out hit, Radius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;

                //GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //prim2.transform.position = finalPosition;
                
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(originHit.position, finalPosition, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        //for (int j = 0; j < path.corners.Length - 1; ++j)
                        //{
                        //    Debug.DrawLine(path.corners[j], path.corners[j + 1], Color.white, 500.0f);
                        //}

                        GameObject fallingObject = Instantiate(FallingObjectPrefab, new Vector3(finalPosition.x, finalPosition.y + 40.0f, finalPosition.z), Quaternion.identity);
                        fallingObject.GetComponent<FallingObject>().vHitLocation = finalPosition;

                       // if (--remaining == 0)
                       //     break;
                    }
                }
            }
        }
    }
}
