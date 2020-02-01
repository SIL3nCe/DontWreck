using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 vHitLocation;
    public GameObject target;

    void Start()
    {
        GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        prim2.transform.position = vHitLocation;
     }
    void Update()
    {
        target.transform.position = vHitLocation;
        Vector3 scale = target.transform.localScale;
        scale.x = Mathf.Min(target.transform.localScale.x * 1.01f, 1.0f);
        scale.y = Mathf.Min(target.transform.localScale.y * 1.01f, 1.0f);
        target.transform.localScale = scale;
    }

    void OnCollisionEnter(Collision collision)
    {
        //TODO Generate FX

        FireGenerator fireGen = GetComponent<FireGenerator>();
        if (fireGen)
        {
            fireGen.Generate(vHitLocation, 5.0f, 2);
        }

        Destroy(gameObject);   
    }
}
