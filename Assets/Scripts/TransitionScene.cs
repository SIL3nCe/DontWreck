using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScene : MonoBehaviour
{

    private GameObject m_Panel;
    public string TargetScene;

    private bool _updateColor;

    float duration = 5.0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Panel = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        _updateColor = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (_updateColor)
        {
            float t = (Time.time - startTime) / duration;
            var lerp = Mathf.SmoothStep(0, 1, t);
            m_Panel.GetComponent<Image>().color = new Color(0, 0, 0, lerp);
        }
    }

    private void _loadScene()
    {
        SceneManager.LoadSceneAsync(TargetScene);
    }
    public void LoadScene(float tiemoutms)
    {
        Invoke("_loadScene", tiemoutms / 1000);
        //SceneManager.LoadSceneAsync(TargetScene);
        _updateColor = true;


        // Make a note of the time the script started.
        startTime = Time.time;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SCENE LOADED");
    }
}
