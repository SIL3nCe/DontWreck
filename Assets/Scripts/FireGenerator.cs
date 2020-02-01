using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FireGenerator : MonoBehaviour
{
    public GameObject FireFX;

    // Start is called before the first frame update
    public void Generate(Vector3 vOrigin, float range, int fireNumber)
    {
        int remaining = fireNumber;
        for (int i = 0; i < 50; ++i)
        {
            Vector3 randLocation = vOrigin + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randLocation, out hit, range, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                //GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //prim2.transform.position = finalPosition;
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(vOrigin, finalPosition, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        //for (int j = 0; j < path.corners.Length - 1; ++j)
                        //{
                        //    Debug.DrawLine(path.corners[j], path.corners[j + 1], Color.white, 500.0f);
                        //}
                        Instantiate(FireFX, finalPosition, Quaternion.identity);
                        if (--remaining == 0)
                            break;
                    }
                }
            }
        }
    }
}
