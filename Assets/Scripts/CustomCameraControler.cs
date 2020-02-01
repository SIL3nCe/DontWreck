using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraControler : MonoBehaviour
{

    public bool onCameraMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            onCameraMove = true;
        }

        if( Input.GetMouseButtonUp(1) ) {
            onCameraMove = false;
        }
    }
}
