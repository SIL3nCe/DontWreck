using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class InputTransitionScene : MonoBehaviour
{

    private GameObject m_Panel;
    private bool _updatePanel;

    private bool _dollyPositioned;

    public CinemachineDollyCart DollyCart;
    public GameObject FirstCameraPrefab;

    float duration = 5.0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        _dollyPositioned = false;
    }
    private void Awake()
    {
    }
    private void OnEnable()
    {
        m_Panel = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        _updatePanel = true;
        // Make a note of the time the script started.
        startTime = Time.time;

    }

    private void ActiveCamera()
    {
        FirstCameraPrefab.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if( DollyCart && FirstCameraPrefab )
        {
            if( DollyCart.m_Position >= 1167.00f && ! _dollyPositioned)
            {
                ActiveCamera();
                _dollyPositioned = true;
            }
        }

        if (_updatePanel)
        {
            float t = (Time.time - startTime) / duration;
            var lerp = Mathf.SmoothStep(1, 0, t);
            m_Panel.GetComponent<Image>().color = new Color(0, 0, 0, lerp);
            if( lerp <= 0)
            {
                _updatePanel = false;
            }
        } 
    }
}
