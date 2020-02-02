using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FallingExplosion : MonoBehaviour
{
    public GameObject FireFX;
    public GameObject ExplosionFX;
    public GameObject ThingToSpawn;
	public AudioClip[] m_explosionClips;

    public float ExplosionRadius;
    public float ExplosionForce;

    // Start is called before the first frame update
    public void Begin(Vector3 vOrigin)
    {
		//
		//
		if (m_explosionClips.Length > 0)
		{
			AudioSource.PlayClipAtPoint(m_explosionClips[Random.Range(0, m_explosionClips.Length)], Camera.main.transform.position, 0.6f);
		}

		Instantiate(ExplosionFX, vOrigin, Quaternion.identity);

        if (ThingToSpawn)
            Instantiate(ThingToSpawn, vOrigin, Quaternion.identity);

        if (FireFX)
            GenerateFire(vOrigin, 5.0f, 2);

        if (ExplosionForce != 0.0f)
        {
            // Get enemy+npcs
            GameObject[] Crew;
            Crew = GameObject.FindGameObjectsWithTag("Crew");
            ApplyImpulseOnCharacs(Crew, vOrigin);

            GameObject[] Enemy;
            Enemy = GameObject.FindGameObjectsWithTag("Enemy");
            ApplyImpulseOnCharacs(Enemy, vOrigin);
        }
    }

    private void ApplyImpulseOnCharacs(GameObject[] aCharacList, Vector3 vOrigin)
    {
        float fSquaredRadius = ExplosionRadius * ExplosionRadius;
        foreach (GameObject charac in aCharacList)
        {
            // Charac is in range of explosion
            if (Vector3.SqrMagnitude(vOrigin - charac.transform.position) < fSquaredRadius)
            {
				/* TODO
                //Rigidbody body = charac.AddComponent<Rigidbody>();
                Rigidbody body = charac.GetComponent<Rigidbody>();
                if (body)
                {
                    charac.GetComponent<NavMeshAgent>().enabled = false;
                    charac.layer = LayerMask.NameToLayer("EjectedCharac");
                    body.isKinematic = false;
                    //body.useGravity = false;
                    body.mass = 5.0f;
                    //body.velocity = charac.transform.forward * -3.0f;
                    body.AddForce(new Vector3(100.0f, 100.0f, 100.0f), ForceMode.Impulse);
                    //body.AddExplosionForce(-3.0f, vOrigin, 10000.0f, 0.0f, ForceMode.Acceleration);
                }
                //Rigidbody body = charac.GetComponent<Rigidbody>();
                //if (body)
                //    body.AddForce(new Vector3(100.0f, 100.0f, 100.0f));
                */
				if (charac != null)
				{
					if (charac.GetComponent<Unit>() != null)
					{
						charac.GetComponent<Unit>().PlayDeathSound();
						GameManager.m_instance.m_unitManager.UnpopUnit(charac.GetComponent<Unit>());
						Destroy(charac);
					}
					else if (charac.GetComponent<EnemyController>() != null)
					{
						charac.GetComponent<EnemyController>().PlayDeathSound();
						Destroy(charac);
					}
				}
            }
        }
    }

    public void GenerateFire(Vector3 vOrigin, float range, int fireNumber)
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
