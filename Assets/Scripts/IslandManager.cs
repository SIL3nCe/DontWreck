using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public Tide seawater;

    // Start is called before the first frame update
    void Start()
    {
        seawater.AddOnTideUpCallback(OnTideUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTideUp()
    {
        //GameManager.EndLevel();
    }
}
