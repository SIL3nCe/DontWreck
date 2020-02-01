﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldClickDestinationSetter : MonoBehaviour
{
    public delegate void OnClicked(Vector3 clickPosition);

    OnClicked m_onClickedCallbacks;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                m_onClickedCallbacks?.Invoke(hitInfo.point);
            }
        }
    }

    public void AddOnClickedCallback(OnClicked onClicked)
    {
        m_onClickedCallbacks += onClicked;
    }

    public void RemoveOnClickedCallback(OnClicked onClicked)
    {
        m_onClickedCallbacks -= onClicked;
    }

    public void ClearOnClickedCallbacks()
    {
        m_onClickedCallbacks = null;
    }
}