using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 vHitLocation;
    public GameObject Target;

    private float scaleMax;
    void Start()
    {
        GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        prim2.transform.position = vHitLocation;

        if (Target)
        {
            scaleMax = Target.transform.localScale.x;
            Target.transform.localScale = new Vector3(scaleMax * 0.1f, scaleMax * 0.1f, scaleMax * 0.1f);
        }
     }
    void Update()
    {
        if (Target)
        {
            Target.transform.position = vHitLocation;
            Vector3 scale = Target.transform.localScale;
            scale.x = Mathf.Min(Target.transform.localScale.x * 1.01f, scaleMax);
            scale.y = Mathf.Min(Target.transform.localScale.y * 1.01f, scaleMax);
            scale.z = Mathf.Min(Target.transform.localScale.z * 1.01f, scaleMax);
            Target.transform.localScale = scale;
        }
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
