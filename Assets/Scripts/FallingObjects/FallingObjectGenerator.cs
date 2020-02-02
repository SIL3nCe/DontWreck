using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FallingObjectGenerator : MonoBehaviour
{
    public float Radius;
    
    [Range(1, 20)]
    public float Timer = 3.0f;

    public int ChancesToSpawnBall;

    public float SpawnHeight;

    public GameObject FallingCannonBall;
    public GameObject FallingEnemy;

    private void Start()
    {
        Invoke("Generate", Random.Range(Timer - 1.0f, Timer + 1.0f));
    }

    public void Generate()
    {
        GameObject objectToSpawn = FallingCannonBall;

        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy.Length < 10)
        {
            if (Random.Range(0, 100) >= ChancesToSpawnBall)
            {
                objectToSpawn = FallingEnemy;
            }
        }

        int remaining = Random.Range(2, 5);
        for (int i = 0; i < remaining; ++i)
        {
            NavMeshHit originHit;
            NavMesh.SamplePosition(gameObject.transform.position, out originHit, 3.0f, NavMesh.AllAreas);

            //GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //prim.transform.position = originHit.position;

            Vector3 randLocation = gameObject.transform.position + Random.insideUnitSphere * Radius;
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

                        GameObject fallingObject = Instantiate(objectToSpawn, new Vector3(finalPosition.x, SpawnHeight, finalPosition.z), Quaternion.identity);
                        fallingObject.GetComponent<FallingObject>().vHitLocation = finalPosition;
                    }
                }
            }
        }

        Invoke("Generate", Random.Range(Timer - 1.0f, Timer + 1.0f));
    }
}
