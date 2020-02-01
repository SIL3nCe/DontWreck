using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Vector3 vHitLocation;
    public GameObject Target;

    void Start()
    {
        GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        prim2.transform.position = vHitLocation;
     }

    void Update()
    {
        if (Target)
        {
            Target.transform.position = vHitLocation;
            Vector3 scale = Target.transform.localScale;
            scale.x = Mathf.Min(Target.transform.localScale.x * 1.02f, 1.0f);
            scale.y = Mathf.Min(Target.transform.localScale.y * 1.02f, 1.0f);
            scale.z = Mathf.Min(Target.transform.localScale.z * 1.02f, 1.0f);
            Target.transform.localScale = scale;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        FallingExplosion explo = GetComponent<FallingExplosion>();
        if (explo)
        {
            explo.Begin(vHitLocation);
        }

        Destroy(gameObject);
    }
}
