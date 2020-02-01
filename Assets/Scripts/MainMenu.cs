using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button CreditsButton;
    public Button QuitButton;

    public CinemachineVirtualCamera FirstCamera;
    public CinemachineVirtualCamera PlayCamera;
    public CinemachineVirtualCamera CreditsCamera;

    public TransitionScene transitionScene;

    public GameObject CreditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Add listener
        PlayButton.onClick.AddListener(PlayAction);
        CreditsButton.onClick.AddListener(CreditsAction);
        QuitButton.onClick.AddListener(QuitAction);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnHover()
    {

    }

    IEnumerator ShowCreditsMenu()
    {
        yield return new WaitForSeconds(5);
        CreditsMenu.SetActive(true);
    }

    // Play action
    void PlayAction()
    {
        Debug.Log("PLAY ACTION");

        // Hide menu, blend cameras
        GetComponent<Canvas>().enabled = false;
        FirstCamera.gameObject.SetActive(false);
        PlayCamera.gameObject.SetActive(true);

        // Lauch scene loading
        transitionScene.LoadScene(5000);

    }

    // Credits action
    void CreditsAction()
    {
        Debug.Log("CREDITS ACTION");
        StartCoroutine(ShowCreditsMenu());
        GetComponent<Canvas>().enabled = false;
        FirstCamera.gameObject.SetActive(false);
        PlayCamera.gameObject.SetActive(false);
        CreditsCamera.gameObject.SetActive(true);
        
        

    }


    // Quit action
    void QuitAction()
    {
        Application.Quit();
    }

}
