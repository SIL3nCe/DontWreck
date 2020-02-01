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
            var lerp = Mathf.Lerp(0, 100, Time.time * 0.0020f);
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
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SCENE LOADED");
    }
}
