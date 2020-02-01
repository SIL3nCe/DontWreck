using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CreditsMenu : MonoBehaviour
{

    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera CreditsCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void DisplayMainMenu()
    {
        GameObject.Find("MainMenu").GetComponent<Canvas>().enabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            CreditsCamera.gameObject.SetActive(false);
            MainCamera.gameObject.SetActive(true);

            Invoke("DisplayMainMenu", 3);
        }
    }
}
