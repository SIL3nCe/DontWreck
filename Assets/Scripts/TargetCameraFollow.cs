using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TargetCameraFollow : MonoBehaviour
{
    public bool onCameraMove = false;
    public GameObject m_MainCamera;
    private BoxCollider m_RestrictedArea;

    private void Start()
    {
        this.m_MainCamera = GameObject.FindGameObjectWithTag("CMainCam");
        m_RestrictedArea = GameObject.FindGameObjectWithTag("RestrictedArea").GetComponent<BoxCollider>();
        m_RestrictedArea.transform.position = new Vector3(m_RestrictedArea.transform.position.x, transform.position.y, m_RestrictedArea.transform.position.z);
        

        Debug.Log(m_RestrictedArea.center);
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

        // IF CAMERA MOVE MODE
        if( onCameraMove ) {
            this.MoveObject();           
        }

        this.ZoomObject();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }


    private void MoveObject()
    {
        Vector2 mouse_delta = Mouse.current.delta.ReadValue();
        Transform ObjectTransform = transform;
        if (m_RestrictedArea.bounds.Contains(new Vector3( transform.position.x + mouse_delta.x, transform.position.y, transform.position.z + (-mouse_delta.y))))
        {
            Debug.Log("CONTAINS");
            ObjectTransform.Translate(mouse_delta.x, 0, -mouse_delta.y);
            transform.position = ObjectTransform.position;
        } else
        {
            Debug.Log("Not contains");
        }


    }

    private void ZoomObject()
    {
        Vector2 zoom_delta = Mouse.current.scroll.ReadValue();
        CinemachineVirtualCamera CMCamera = m_MainCamera.GetComponent<CinemachineVirtualCamera>();

        Vector3 currentFollowOffset = CMCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        currentFollowOffset.y += zoom_delta.y * Time.deltaTime;

        if ( currentFollowOffset.y > 2 )
        {
            CMCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = currentFollowOffset;
        }


    }
}
