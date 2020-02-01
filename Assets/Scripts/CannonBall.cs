using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
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
        Target.transform.position = vHitLocation;
        Vector3 scale = Target.transform.localScale;
        scale.x = Mathf.Min(Target.transform.localScale.x * 1.01f, 1.0f);
        scale.y = Mathf.Min(Target.transform.localScale.y * 1.01f, 1.0f);
        Target.transform.localScale = scale;
    }

    void OnCollisionEnter(Collision collision)
    {

        CannonExplosion explo = GetComponent<CannonExplosion>();
        if (explo)
        {
            explo.Begin(vHitLocation);
        }

        Destroy(gameObject);
    }
}
