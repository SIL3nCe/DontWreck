using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldClickDestinationSetter : MonoBehaviour
{
    public delegate void OnClicked(Vector3 clickPosition);

    OnClicked onClickedCallbacks;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                onClickedCallbacks?.Invoke(hitInfo.point);
            }
        }
    }

    public void AddOnClickedCallback(OnClicked onClicked)
    {
        onClickedCallbacks += onClicked;
    }

    public void RemoveOnClickedCallback(OnClicked onClicked)
    {
        onClickedCallbacks -= onClicked;
    }

    public void ClearOnClickedCallbacks()
    {
        onClickedCallbacks = null;
    }
}
