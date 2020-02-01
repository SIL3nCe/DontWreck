using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button CreditsButton;
    public Button QuitButton;

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



    // Play action
    void PlayAction()
    {
        Debug.Log("PLAY ACTION");
    }

    // Credits action
    void CreditsAction()
    {
        Debug.Log("CREDITS ACTION");
    }


    // Quit action
    void QuitAction()
    {
        Application.Quit();
    }

}
